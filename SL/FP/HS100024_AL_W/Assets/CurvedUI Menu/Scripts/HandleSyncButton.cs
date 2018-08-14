using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using VeativeDatabase;

public class HandleSyncButton : MonoBehaviour
{
    public static HandleSyncButton Instance;

    public GameObject syncButton;
    public GameObject crossButton;

    public bool mode;
    private Vector3 crossButtonPosition;

    Vector3 _intial;

    void Awake()
    {
        Instance = this;
        _intial = crossButton.transform.position;
    }

    void Start()
    {
        if (CanSendDataToServer())
            syncButton.SetActive(true);
        else
            syncButton.SetActive(false);
    }

    public void Swap_Btns()
    {
        if (LanguageHandler.instance.IsLeftToRight)
        {			
            crossButton.transform.position = _intial;
        }
        else
        {
            crossButton.transform.position = new Vector3(-1*_intial.x, _intial.y, _intial.z);
        }

        #if !UNITY_EDITOR
        mode = PlayerPrefs.GetInt ( "mode" ) != 0 ? true : false ;
        #endif

        if (mode)
        {
            crossButton.SetActive(false);
        }
        else
        {
            crossButton.SetActive(true);
        }
    }

    public bool CanSendDataToServer()
    {
        List < MyDataEntry > myDataEntries = new List < MyDataEntry >();
        myDataEntries = DatabaseManager.dbm.ReadFromDatabase();
        string json = DatabaseManager.dbm.ReadDBInJson(myDataEntries);

        if (json != "")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SendData2Server()
    {
        SoundManager.sm.PlayClickSound();
        DatabaseManager.dbm.SendDataToServer();
    }
}