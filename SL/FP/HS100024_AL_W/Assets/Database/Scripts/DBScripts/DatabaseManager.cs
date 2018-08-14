using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Mono.Data.Sqlite;
using System.Data;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace VeativeDatabase
{

    public class DatabaseManager : MonoBehaviour
    {
		
        // Instance for accessing variables of this class
        public static DatabaseManager dbm;

        public bool makingAndroidProject;

        // Enter your topic ID here very carefully as only according to this your data will be saved and displayed when required
        // ( DO CROSS VERIFY AT LEAST TWO TIMES )
        public string topicID;

        public int totalNumberOfQuestions;

        // These are made public and hide in inspector so as to check these entries at any point of your game by removing HideInInspector
        // and also for making their access possible outside script through instance
        //		[HideInInspector]
        public int userID;
        //		[HideInInspector]
        public string userName;
        //		[HideInInspector]
        public string language;
        //		[HideInInspector]
        public int mode;
        [HideInInspector]
        public string packageName;

        public List < LevelData > levelsData;

        //		[HideInInspector]
        public List < MyDataEntry > dataEntries;

        // This is for the reading of the database when required internally
        private IDataReader reader;

        // These variables are for saving firstDataEntry's ActivityId and lastDataEntry's ActivityId to delete those entries when success is received from server
        private int activityId_FirstDataEntry;
        private int activityId_LastDataEntry;

        void Awake()
        {

            Debug.Log("Final Database Package");

            if (dbm != null && dbm != this)
            {

                Destroy(dbm.gameObject);
                dbm = null;
            }


            if (dbm == null)
            {
                dbm = this;
                DontDestroyOnLoad(gameObject);
            }

            packageName = Application.identifier;
            topicID = packageName.Substring(packageName.LastIndexOfAny(".".ToCharArray()) + 1, 
                (packageName.Length - packageName.LastIndexOfAny(".".ToCharArray()) - 1)).ToUpper();

            // These lines are for testing purpose, remember to comment these to get these from the wrapper when making it's final build
            #region TempForTesting

            #if UNITY_EDITOR

            PlayerPrefs.SetInt ("userId", 647);
            PlayerPrefs.SetString ("userName", "user");

            #endif

            #endregion

            userID = PlayerPrefs.GetInt("userId");
            userName = PlayerPrefs.GetString("userName");
            language = PlayerPrefs.GetString("currentLanguage");
            mode = PlayerPrefs.GetInt("mode");
        }

        void Start()
        {
            #if !UNITY_EDITOR

            if ( makingAndroidProject ) {

                if (userID == 0) {

                    PlayPauseSimulation.instance.ShowStartErrorMessage (":(\n\nContact Support");

                } else if (string.IsNullOrEmpty(userName)) { 

                    PlayPauseSimulation.instance.ShowStartErrorMessage (":(\n\nContact Support");

                } else if ( string.IsNullOrEmpty(language) || !LanguageHandler.instance.CheckIfLanguageExist ( language ) ) {

                    PlayerPrefs.SetString ("currentLanguage", "en-US");

                }

            }
            #endif

        }

        public void InsertFinalDataToDatabase()
        {

            if (dataEntries.Count != totalNumberOfQuestions)
            {

                Debug.LogError("Entries will be done only when entries will be equal to total number of questions");
                Debug.LogError("Data Not Saved To Database");

                LevelData currentLevelData = null;
                string currentLevelName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

                for (int i = 0; i < levelsData.Count; i++)
                {

                    if (currentLevelName == levelsData[i].levelSceneName)
                    {

                        currentLevelData = levelsData[i];

                    }

                }

                if (currentLevelData == null)
                {

                    Debug.LogError("This : ( " + currentLevelName + " ) scene does not exist in the Database Manager's LevelsData");
                    return;

                }

                if (currentLevelData.noOfQuestions == dataEntries.Count)
                {

                    if (LoadData(currentLevelName) == null)
                    {

                        Debug.Log("Saving Level ( " + currentLevelName + " ) Data to file");
                        SaveData(currentLevelName, dataEntries);
                        ClearDataEntries();

                    }
                    else
                    {

                        Debug.Log("Overriding Level ( " + currentLevelName + " ) Data to file");
                        List < MyDataEntry > currentLevelDataEntries = LoadData(currentLevelName);
                        currentLevelDataEntries = dataEntries;
                        SaveData(currentLevelName, currentLevelDataEntries);
                        ClearDataEntries();

                    }

                    if (CheckIfAllLevelsDataExist())
                    {

                        Debug.Log("All Levels Data Exist. Collect Data Entries");
                        CollectDataFromFilesToDataEntries();
                        InsertFinalDataToDatabase();
                        DeleteDataFiles();
                        Debug.Log("Deleted Data File");

                    }
                    else
                    {

                        Debug.LogError("All Levels Data has not been saved till time");
                        return;

                    }

                }
                else
                {

                    Debug.LogError("Level ( " + currentLevelName + " ) : Entries will be done only when entries will be equal to number of questions");
                    Debug.LogError("Level Data Not Even Saved To File");

                }

                return;

            }

            Debug.Log("Inserting Data Entries into Database");

            while (dataEntries.Count > 0)
            {
				
                try
                {

                    DatabaseConnection.OpenConnection();

                    string sqlQuery = "INSERT INTO T_UserActivity ( UserID, UserName, PackageID, PackageName, LevelID , QuestionID," +
                       " Correctness, HintUsed , UserAttempts, UserScore, Language, AttemptedOn , Mode )" +
                       "values ("
                       + userID + ", '" + userName + "', '" + topicID + "', '" + packageName + "', '" + dataEntries[0].LevelID + "', '" + dataEntries[0].QuestionID +
                       "', '" + dataEntries[0].Correctness + "', '" + dataEntries[0].hintUsed + "', " + dataEntries[0].UserAttempts + ", " + dataEntries[0].UserScore + ", '" + language + "', '" + dataEntries[0].AttemptedOn + "', " + mode + " )";

                    DatabaseConnection.dbcmd.CommandText = sqlQuery;
                    DatabaseConnection.dbcmd.ExecuteNonQuery();
                    Debug.Log("SuccessFully Submitted " + sqlQuery);
                    dataEntries.Remove(dataEntries[0]);

                }
                catch (SqliteException ex)
                {
					
                    Debug.LogError("Database error " + ex.Message);

                }
                finally
                {

                    DatabaseConnection.CloseConnection();

                }

            }

            Debug.Log("Data Saved To Database");
            Debug.Log("Sending data to Server");
            SendDataToServer();

        }

        //		string CheckForApostrophe ( string columnvalue ) {
        //
        //			if (columnvalue.Contains ("'")) {
        //				columnvalue = columnvalue.Replace ("'", "^");
        //			}
        //
        //			return columnvalue;
        //		}

        //		public string CheckForSpecialSymbols ( string columnvalue ) {
        //
        //			char [ ] myStringCharacters = columnvalue.ToArray ( );
        //
        //			string [ ] myStringArray = new string [ myStringCharacters.Length ];
        //
        //			for ( int i = 0 ; i < myStringCharacters.Length ; i++ )
        //				myStringArray [ i ] = myStringCharacters [ i ].ToString ( );
        //
        //			for ( int i = 0 ; i < myStringCharacters.Length ; i++ ) {
        //
        //				if ( ( int ) myStringCharacters [ i ] == 36 || ( int ) myStringCharacters [ i ] == 39 || ( int ) myStringCharacters [ i ] == 64 || ( int ) myStringCharacters [ i ] == 126 || ( int ) myStringCharacters [ i ] == 128 || ( ( int ) myStringCharacters [ i ] >= 130 && ( int ) myStringCharacters [ i ] <= 140 )
        //					|| ( int ) myStringCharacters [ i ] == 142 || ( ( int ) myStringCharacters [ i ] >= 145 && ( int ) myStringCharacters [ i ] <= 156 ) || ( int ) myStringCharacters [ i ] == 158 || ( int ) myStringCharacters [ i ] == 159
        //					|| ( ( int ) myStringCharacters [ i ] >= 161 && ( int ) myStringCharacters [ i ] <= 172 ) || ( ( int ) myStringCharacters [ i ] >= 174 && ( int ) myStringCharacters [ i ] <= 255 ) ) {
        //
        //					//Debug.Log ( "My string is now &#" + ( ( int ) myStringCharacters [ i ] ).ToString ( ) + ";" );
        //					myStringArray [ i ] = "&#" + ( ( int ) myStringCharacters [ i ] ).ToString ( ) + ";";
        //
        //				}
        //
        //			}
        //
        //			string finalMyString="";
        //			for ( int i = 0 ; i < myStringArray.Length ; i++ )
        //				finalMyString += myStringArray [ i ];
        //
        //			//Debug.Log ( "Final string is " + finalMyString );
        //
        //			return finalMyString;
        //
        //		}

        public List < MyDataEntry > ReadFromDatabase()
        {

            Debug.Log("Reading Table...");
            DatabaseConnection.OpenConnection();

            string sqlQuery = "SELECT ActivityID, UserID, UserName, PackageID , PackageName , LevelID , QuestionID, " +
                     "Correctness, HintUsed, UserAttempts, UserScore , Language, AttemptedOn , Mode " + "FROM T_UserActivity";
			
            DatabaseConnection.dbcmd.CommandText = sqlQuery;
            reader = DatabaseConnection.dbcmd.ExecuteReader();

            List < MyDataEntry > myDataEntries = new List < MyDataEntry >();

            while (reader.Read())
            {
				
                myDataEntries.Add(new MyDataEntry(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6),
                        reader.GetString(7), reader.GetString(8), reader.GetInt32(9), reader.GetInt32(10), reader.GetString(11), reader.GetString(12), reader.GetInt32(13)));
				
            }

            reader.Close();
            reader = null;
            DatabaseConnection.CloseConnection();

            return myDataEntries;

        }

        public string ReadDBInJson(List < MyDataEntry > myDataEntries)
        {

            if (myDataEntries.Count != 0)
            {

                int maxIndexOfEntriesThatWouldBeSent;

                if (myDataEntries.Count > 100)
                {

                    int dummy = (int)100 / totalNumberOfQuestions;

                    maxIndexOfEntriesThatWouldBeSent = myDataEntries.Count - (myDataEntries.Count - (dummy * totalNumberOfQuestions));

                }
                else
                {

                    maxIndexOfEntriesThatWouldBeSent = myDataEntries.Count;

                }

                if (myDataEntries.Count > maxIndexOfEntriesThatWouldBeSent)
                {

                    for (int i = myDataEntries.Count - 1; i > maxIndexOfEntriesThatWouldBeSent - 1; i--)
                    {

                        myDataEntries.RemoveAt(i);

                    }

                }

                Debug.Log(myDataEntries.Count);

                activityId_FirstDataEntry = myDataEntries[0].ActivityID;
                activityId_LastDataEntry = myDataEntries[myDataEntries.Count - 1].ActivityID;

                MyDatabase mdb = new MyDatabase();
                mdb.myDataEntries = myDataEntries;
                string json = JsonUtility.ToJson(mdb);
                Debug.Log(json);
                return json;

            }
            else
            {

                Debug.Log("There are no entries in the database to be converted to JSON");
                return "";

            }

        }


        #region Temporary

        string str = "";
        //
        //		void OnGUI()
        //		{
        //			GUI.Label (new Rect(100,100,100,100),str);
        //		}

        #endregion


        public void PostDataToServer(string json)
        {

            string url = "http://veative.com/learn/public/api/app-activity";


            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            Hashtable ht = new Hashtable();
            ht.Add("Content-Type", "application/json");
            ht.Add("Content-Length", json.Length.ToString());

            WWW www = new WWW(url, encoding.GetBytes(json), HashtableToDictionary < string , string >(ht));
            StartCoroutine(ShowResponse(www));

        }

        IEnumerator ShowResponse(WWW www)
        {

            Debug.Log("Processing Request");

            if (ProgressPanelManager.ppm != null)
                ProgressPanelManager.ppm.ShowProgressPanel();
            //Show panel for sending data entries to the server ( Catch for not sending redundant data )
            yield return www;

            if (ProgressPanelManager.ppm != null)
                ProgressPanelManager.ppm.HideProgressPanel();

            if (www.error == null)
            {

                Debug.Log("WWW Ok : " + www.text);

                DeleteEntriesFromDatabase();
                Debug.Log("Data Successfully sent to Server");
                //AlertPanelManager.apm.ShowAlertMessage ("Data successfully Synced with server");
                // Show panel for success of data entries sent to server and deletion of entries from local DB

//				string s = www.data.ToString ( );
//				Debug.Log ( s.IndexOf ( "]" ) );
//				s = s.Remove ( s.IndexOf ( "]" ) );
//				Debug.Log (s);
//				char [ ] c = new char [ s.Length ];
//				c = s.ToCharArray ();
//				List <char> cc = c.ToList < char > ( );
//				s = "";
//				Debug.Log ( cc.Count );
//				Debug.Log (cc.IndexOf ('['));
//				for ( int i = 0 ; i < cc.IndexOf ( '[' ) ; i++ ) {
//
//					cc.RemoveAt ( i );
//
//				}
//
//				Debug.Log ( cc.Count );
//
//				for ( int i = 0 ; i < cc.Count ; i++ ) {
//
//					s += cc[i].ToString ( );
//
//				}
//				s = cc.ToString ( );
//				Debug.Log ( s );
//				Debug.Log ( JsonUtility.FromJson < Response > ( www.data ) );

            }
            else
            {
                str = www.text;
                str += "-------" + www.error;
                Debug.Log("WWW Error : " + www.error);
                Debug.LogError("Data not sent to Server");
                if (AlertPanelManager.apm != null)
                    AlertPanelManager.apm.ShowAlertMessage("Internal server error");
                // Show panel for server error with server error

            }

        }

        public static Dictionary < K , V > HashtableToDictionary < K, V >(Hashtable table)
        {
			
            return table.Cast < DictionaryEntry >().ToDictionary(kvp => (K)kvp.Key, kvp => (V)kvp.Value);

        }

        public void SendDataToServer()
        {

            if (Application.internetReachability == NetworkReachability.NotReachable)
            {

                Debug.Log("There is no internet connection");
                if (AlertPanelManager.apm != null)
                    AlertPanelManager.apm.ShowAlertMessage("Please check your network connection or try again later");
                //Show panel for no internet connectivity

            }
            else
            {

                List < MyDataEntry > myDataEntries = new List < MyDataEntry >();
                myDataEntries = ReadFromDatabase();
                Debug.Log(myDataEntries.Count);

                string json = "";

                json = ReadDBInJson(myDataEntries);

                if (json != "")
                {

                    if (json.Contains("^"))
                    {

                        json = json.Replace("^", "'");

                    }

                    PostDataToServer(json);

                }
                else
                {

                    Debug.Log("There are no entries in the database to be sent to the server");
                    if (ProgressPanelManager.ppm != null)
                        ProgressPanelManager.ppm.ShowProgressPanelForSometime();
//					AlertPanelManager.apm.ShowAlertMessage ("No Data Entries");
                    //Show panel for no data entries ( Please play the simulation at least once )

                }

            }

        }

        public void DeleteEntriesFromDatabase()
        {

            Debug.Log("Deleteing Entries From Table...");
            DatabaseConnection.OpenConnection();

            for (int i = activityId_FirstDataEntry; i <= activityId_LastDataEntry; i++)
            {
				
                string sqlQuery = "DELETE FROM T_UserActivity WHERE ActivityID = " + i.ToString();
                DatabaseConnection.dbcmd.CommandText = sqlQuery;
                DatabaseConnection.dbcmd.ExecuteNonQuery();
            }

            DatabaseConnection.CloseConnection();

        }

        public string CurrentDateTime()
        {

            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:") + DateTime.Now.Millisecond.ToString();

        }

        public void ClearDataEntries()
        {
			
            Debug.Log("Cleared the data entries");
            dataEntries = new List < MyDataEntry >();

        }

        public List < MyDataEntry > LoadData(string levelSceneName)
        {

            if (File.Exists(Application.persistentDataPath + "/" + levelSceneName + ".dat"))
            {

                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/" + levelSceneName + ".dat", FileMode.Open);

                LevelDataEntries currentLevelDataEntries = (LevelDataEntries)bf.Deserialize(file);
                file.Close();

                return currentLevelDataEntries.dataEntries;

            }
            else
            {

                return null;

            }

        }

        public void SaveData(string levelSceneName, List < MyDataEntry > dataEntries)
        {

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/" + levelSceneName + ".dat");

            LevelDataEntries currentLevelDataEntries = new LevelDataEntries();
            currentLevelDataEntries.dataEntries = dataEntries;

            bf.Serialize(file, currentLevelDataEntries);
            file.Close();

        }

        public bool CheckIfAllLevelsDataExist()
        {

            for (int i = 0; i < levelsData.Count; i++)
            {

                if (!File.Exists(Application.persistentDataPath + "/" + levelsData[i].levelSceneName + ".dat"))
                    return false;

            }

            return true;

        }

        public void CollectDataFromFilesToDataEntries()
        {

            for (int i = 0; i < levelsData.Count; i++)
            {
				
                List < MyDataEntry > levelDataEntries = LoadData(levelsData[i].levelSceneName);

                for (int j = 0; j < levelDataEntries.Count; j++)
                    dataEntries.Add(levelDataEntries[j]);

            }

        }

        public void DeleteDataFiles()
        {

            for (int i = 0; i < levelsData.Count; i++)
            {

                if (File.Exists(Application.persistentDataPath + "/" + levelsData[i].levelSceneName + ".dat"))
                    File.Delete(Application.persistentDataPath + "/" + levelsData[i].levelSceneName + ".dat");

            }

        }

        public int CheckForRedundancy(string questionId, string levelId)
        {

            for (int i = 0; i < dataEntries.Count; i++)
            {

                if (dataEntries[i].QuestionID == questionId && dataEntries[i].LevelID == levelId)
                {

                    int dataEntryIndex = i;
                    return dataEntryIndex;

                }

            }

            return -1;

        }

        //		public void AddEntry ( string QuestionID, string QuestionName, string Option_1, string Option_2, string Option_3, string Option_4, string UserResponse, string CorrectResponse, int UserAttempts, int UserScore, bool Correctness , bool hintUsed ) {
        //
        //			if ( QuestionID == null || QuestionID == "" ) {
        //
        //				Debug.LogError ( "You have not the Question Id in the data entry" );
        //				return;
        //
        //			}
        //
        //			if ( QuestionName == null || QuestionName == "" ) {
        //
        //				Debug.LogError ( "You have not the Question Name in the data entry" );
        //				return;
        //
        //			}
        //
        //			if ( Option_1 == null || Option_1 == "" ) {
        //
        //				Debug.LogError ( "You have not the Option_1 in the data entry" );
        //				return;
        //
        //			}
        //
        //			if ( Option_2 == null || Option_2 == "" ) {
        //
        //				Debug.LogError ( "You have not the Option_2 in the data entry" );
        //				return;
        //
        //			}
        //
        //			if ( Option_3 == null || Option_3 == "" ) {
        //
        //				Debug.LogError ( "You have not the Option_3 in the data entry" );
        //				return;
        //
        //			}
        //
        //			if ( Option_4 == null || Option_4 == "" ) {
        //
        //				Debug.LogError ( "You have not the Option_4 in the data entry" );
        //				return;
        //
        //			}
        //
        //			if ( UserResponse == null || UserResponse == "" ) {
        //
        //				Debug.LogError ( "You have not the UserResponse in the data entry" );
        //				return;
        //
        //			}
        //
        //			if ( CorrectResponse == null || CorrectResponse == "" ) {
        //
        //				Debug.LogError ( "You have not the CorrectResponse in the data entry" );
        //				return;
        //
        //			}
        //
        //			if ( UserAttempts == 0 ) {
        //
        //				Debug.LogError ( "You have not the UserAttempts in the data entry" );
        //				return;
        //
        //			}
        //
        //			if ( CheckForRedundancy ( QuestionID ) == - 1 ) {
        //
        //				dataEntries.Add ( new MyDataEntry (QuestionID, QuestionName, Option_1, Option_2, Option_3, Option_4, UserResponse, CorrectResponse, UserAttempts, UserScore, Correctness, hintUsed ) );
        //
        //			} else {
        //
        //				ReplaceDataEntry ( QuestionID, QuestionName, Option_1, Option_2, Option_3, Option_4, UserResponse, CorrectResponse, UserAttempts, UserScore, Correctness , hintUsed );
        //
        //			}
        //
        //		}

        public void AddEntry(string LevelID, string QuestionID, int UserAttempts, int UserScore, bool Correctness, bool hintUsed)
        {

            if (LevelID == null || LevelID == "")
            {

                Debug.LogError("You have not entered the Level Id in the data entry");
                return;

            }

            if (QuestionID == null || QuestionID == "")
            {

                Debug.LogError("You have not entered the Question Id in the data entry");
                return;

            }

            if (UserAttempts == 0)
            {

                Debug.LogError("You have not entered the User Attempts in the data entry");
                return;

            }

            if (CheckForRedundancy(QuestionID, LevelID) == -1)
            {

                dataEntries.Add(new MyDataEntry(LevelID, QuestionID, UserAttempts, UserScore, Correctness, hintUsed));

            }
            else
            {

                ReplaceDataEntry(LevelID, QuestionID, UserAttempts, UserScore, Correctness, hintUsed);

            }

        }

        //		public void ReplaceDataEntry ( string QuestionID, string QuestionName, string Option_1, string Option_2, string Option_3, string Option_4, string UserResponse, string CorrectResponse, int UserAttempts, int UserScore, bool Correctness , bool hintUsed ) {
        //
        //			int dataEntryIndex = CheckForRedundancy ( QuestionID );
        //
        //			dataEntries[dataEntryIndex].QuestionID = QuestionID;
        //			dataEntries[dataEntryIndex].QuestionName = QuestionName;
        //			dataEntries[dataEntryIndex].Option_1 = Option_1;
        //			dataEntries[dataEntryIndex].Option_2 = Option_2;
        //			dataEntries[dataEntryIndex].Option_3 = Option_3;
        //			dataEntries[dataEntryIndex].Option_4 = Option_4;
        //			dataEntries[dataEntryIndex].UserResponse = UserResponse;
        //			dataEntries[dataEntryIndex].CorrectResponse = CorrectResponse;
        //			dataEntries[dataEntryIndex].UserAttempts = UserAttempts;
        //			dataEntries[dataEntryIndex].UserScore = UserScore;
        //			dataEntries[dataEntryIndex].AttemptedOn = CurrentDateTime ( );
        //			dataEntries[dataEntryIndex].Correctness = Correctness.ToString ();
        //			dataEntries[dataEntryIndex].hintUsed = hintUsed.ToString ();
        //
        //		}

        public void ReplaceDataEntry(string LevelID, string QuestionID, int UserAttempts, int UserScore, bool Correctness, bool hintUsed)
        {

            int dataEntryIndex = CheckForRedundancy(QuestionID, LevelID);

            dataEntries[dataEntryIndex].LevelID = LevelID;
            dataEntries[dataEntryIndex].QuestionID = QuestionID;
            dataEntries[dataEntryIndex].UserAttempts = UserAttempts;
            dataEntries[dataEntryIndex].UserScore = UserScore;
            dataEntries[dataEntryIndex].AttemptedOn = CurrentDateTime();
            dataEntries[dataEntryIndex].Correctness = Correctness.ToString();
            dataEntries[dataEntryIndex].hintUsed = hintUsed.ToString();

        }

    }

    [ System.Serializable]
    public class LevelData
    {

        public string levelSceneName;
        public int noOfQuestions;

    }

    [ System.Serializable]
    public class LevelDataEntries
    {

        public List < MyDataEntry > dataEntries;

    }

}