using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LanguageSelectionManager : MonoBehaviour
{
    public static LanguageSelectionManager Instance;  
    public GameObject Languagebutton;
    List<GameObject> buttons = new List<GameObject>();

    void Start()
    {
        Instance = this;

        Languagebutton.name = LanguageHandler.instance.Languages[0].DisplayName;
        Languagebutton.GetComponentInChildren<Text>().text = LanguageHandler.instance.Languages[0].DisplayName;
        buttons.Add(Languagebutton);

        for (int i = 1; i < LanguageHandler.instance.Languages.Count; i++)
        {
            GameObject temp = Instantiate(Languagebutton, Languagebutton.transform.parent) as GameObject;
            temp.name = LanguageHandler.instance.Languages[i].DisplayName;
            temp.GetComponentInChildren<Text>().text = LanguageHandler.instance.Languages[i].DisplayName;
            buttons.Add(temp);
        }

        Debug.Log((PlayerPrefs.GetString("currentLanguage")));

        buttons[LanguageHandler.instance.CurrentLanguageIndex].GetComponentInChildren<Text>().color = new Color32(105, 223, 0, 255);
        buttons[LanguageHandler.instance.CurrentLanguageIndex].GetComponent<EventTrigger>().enabled = false;
    }

    public void OnClickLanguageButton(GameObject Button)
    {
        makeDefaultcolor();

        PlayerPrefs.SetString("currentLanguage", LanguageHandler.instance.Languages[Button.transform.GetSiblingIndex()].LanguageID);

        Button.transform.GetChild(0).GetComponentInChildren<Text>().color = new Color32(105, 223, 0, 255);
        Button.GetComponent<EventTrigger>().enabled = false;

        LanguageHandler.instance.setCurrentLanguage();
        LanguageHandler.instance.changeLanguageUpdate();
       
        HandleSyncButton.Instance.Swap_Btns();
        ReverseMenu.Instance.Reverse();
        SoundManager.sm.PlayClickSound();
    }

    void makeDefaultcolor()
    {
        if (buttons.Count <= 0)
            return;

        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].GetComponentInChildren<Text>().color = new Color32(233, 233, 233, 255);
            buttons[i].GetComponent<EventTrigger>().enabled = true;
        }
    }

    public void OnOffTable(GameObject Table)
    {
        Table.SetActive(!Table.activeInHierarchy);
        SoundManager.sm.PlayClickSound();
    }

    public void TableDisable(GameObject obj)
    {
        obj.SetActive(false);
        SoundManager.sm.PlayClickSound();
    }
}
