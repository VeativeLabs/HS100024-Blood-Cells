using Mono.Data.Sqlite;
using System.Data;
using UnityEngine;
using System.IO;


/// <summary>
/// Database connection.
///	Dont make any changes in this file.
/// </summary>
namespace VeativeDatabase {
	
	public class DatabaseConnection : MonoBehaviour {

		public static string dbpath;
		public static IDbConnection dbconn;
		public static IDbCommand dbcmd;
		public static string dbname = "UserActivity.db";

		/// <summary>
		/// Connect to the database on priority
		/// </summary>
		void Awake ( ) {
			
			ConnectDatabase ( );

		}

		/// <summary>
		/// Connects to database. Gets called from Awake.
		/// </summary>
		void ConnectDatabase ( ) {

			string filepath;

			if ( Application.platform != RuntimePlatform.Android ) {
				
				dbpath = "URI=file:" + Application.streamingAssetsPath + "/UserActivity.db";

			} else {
				
				filepath = Application.persistentDataPath + "/" + dbname;
				if ( !File.Exists ( filepath ) ) {

					WWW loadDB = new WWW ( "jar:file://" + Application.dataPath + "!/assets/" + dbname );

					while ( !loadDB.isDone ) { }

					File.WriteAllBytes ( filepath, loadDB.bytes );
				}
				dbpath = "URI=file:" + filepath;
			}

			dbconn = new SqliteConnection ( dbpath );
			dbconn.Open ( );
			dbcmd = dbconn.CreateCommand ( );

			dbconn.Close ( );

			if ( PlayerPrefs.GetInt ( "FirstRun" ) == 0 ) {

				DropTable ( );
				Invoke ( "ClearDataFiles" , 0.01f );
				PlayerPrefs.SetInt ( "FirstRun" , 1 );

			}

			CreateTable ( );

			#if UNITY_EDITOR

				DropTable ( );
				Invoke ( "ClearDataFiles" , 0.01f );

			#endif

		}

		void ClearDataFiles ( ) {

			DatabaseManager.dbm.DeleteDataFiles ( );

		}

		/// <summary>
		/// To open the database connection
		/// </summary>
		public static void OpenConnection ( ) {
			
			try {
				
				dbconn = ( IDbConnection )new SqliteConnection ( dbpath );
				dbconn.Open ( ); //Open connection to the database.
				//Debug.Log ("Database Opened " + dbconn);
				dbcmd = dbconn.CreateCommand ( );
				//Debug.Log ("Creating Command " + dbcmd);

			} catch ( SqliteException ex ) {

				Debug.LogError ( ex.Message );

			}

		}

		/// <summary>
		/// To close the database connection
		/// </summary>
		public static void CloseConnection ( ) {
			
			dbcmd.Dispose ( );
			dbcmd = null;
			dbconn.Close ( );
			dbconn = null;

		}

		/// <summary>
		/// To create a new table in the database
		/// </summary>
		public static void CreateTable ( ) {
			
			OpenConnection ( );

			try {
				
				string sqlQuery = "CREATE TABLE T_UserActivity ( ActivityID INTEGER PRIMARY KEY AUTOINCREMENT, UserID INT, UserName VARCHAR, PackageID VARCHAR, PackageName VARCHAR, " +
					"LevelID VARCHAR, QuestionID VARCHAR, Correctness VARCHAR, HintUsed VARCHAR , UserAttempts INT, UserScore INT, Language VARCHAR, AttemptedOn VARCHAR , Mode INT )";
				
				dbcmd.CommandText = sqlQuery;
				dbcmd.ExecuteNonQuery ( );
			
			} catch ( SqliteException ex ) {

				if ( ex.Message.Contains ( "table" ) && ex.Message.Contains ("already exists" ) ) {

					Debug.Log ( "Table Already Exists" );

				} else {

					//DropTable ();
					Debug.Log ( ex.Message );

				}

			} finally {
				
				CloseConnection ( );

			}

		}

		/// <summary>
		/// To delete the whole table from the database
		/// </summary>
		public static void DropTable ( ) {
			
			OpenConnection ( );

			string sqlQuery = "DROP TABLE T_UserActivity";

			dbcmd.CommandText = sqlQuery;
			dbcmd.ExecuteNonQuery ( );

			CloseConnection ( );
			CreateTable ( );

		}

	}

}