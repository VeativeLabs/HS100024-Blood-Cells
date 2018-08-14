using UnityEngine;
using System.Collections;
using SmartLocalization;
using UnityEngine.UI;
using ArabicSupport;
using System.Collections.Generic;

[System.Serializable]
public class LanguageData
{
    public enum LanguageTextAlignment{LeftToRight, RightToLeft};

    public string DisplayName,LanguageID;
    public LanguageTextAlignment _Alignment;
}

public class LanguageHandler : MonoBehaviour
{
    public static LanguageHandler instance;

    public delegate void LanguageChanged();
    public static event LanguageChanged LanguageChangeEventFire;

    public List<LanguageData> Languages;
    [HideInInspector]
    public int CurrentLanguageIndex;

    public const int _DefaultLanguageIndex = 1;

    public bool IsLeftToRight
    {
        get
        { 
            return (LanguageHandler.instance.Languages[LanguageHandler.instance.CurrentLanguageIndex]._Alignment == LanguageData.LanguageTextAlignment.LeftToRight);
        }
    }

    public bool IsRightToLeft
    {
        get
        { 
            return (LanguageHandler.instance.Languages[LanguageHandler.instance.CurrentLanguageIndex]._Alignment == LanguageData.LanguageTextAlignment.RightToLeft);
        }
    }

    void Awake()
    {
        instance = this;
        setCurrentLanguage();
        changeLanguageUpdate();
    }

    internal void setCurrentLanguage()
    {    
        if (!PlayerPrefs.HasKey("currentLanguage"))
        {
            CurrentLanguageIndex = _DefaultLanguageIndex;
            PlayerPrefs.SetString("currentLanguage", Languages[CurrentLanguageIndex].LanguageID);
        }
        else
        {
            for (int i = 0; i < Languages.Count; i++)
            {
                if (PlayerPrefs.GetString("currentLanguage").Equals(Languages[i].LanguageID))
                {
                    CurrentLanguageIndex = i;
                    PlayerPrefs.SetString("currentLanguage", Languages[CurrentLanguageIndex].LanguageID);
                    return;
                }
            }
        }
    }

    internal void changeLanguageUpdate()
    {
        LanguageManager.Instance.ChangeLanguage(Languages[CurrentLanguageIndex].LanguageID);
        if (LanguageChangeEventFire != null)
            LanguageChangeEventFire();
    }   

    public void OnLanguageChangeListener(Text TextBox, string CurrKey)
    {
        if (LanguageHandler.instance.Languages[LanguageHandler.instance.CurrentLanguageIndex]._Alignment==LanguageData.LanguageTextAlignment.RightToLeft)
        {
            switch (TextBox.alignment)
            {
                case TextAnchor.UpperLeft:
                    TextBox.alignment = TextAnchor.UpperRight;
                    break;
                case TextAnchor.MiddleLeft:
                    TextBox.alignment = TextAnchor.MiddleRight;
                    break;
                case TextAnchor.LowerLeft:
                    TextBox.alignment = TextAnchor.LowerRight;
                    break;
            }
            TextBox.text = ArabicFixer.Fix("" + LanguageManager.Instance.GetTextValue(CurrKey));          
        }
        else
        {
            switch (TextBox.alignment)
            {
                case TextAnchor.UpperRight:
                    TextBox.alignment = TextAnchor.UpperLeft;
                    break;
                case TextAnchor.MiddleRight:
                    TextBox.alignment = TextAnchor.MiddleLeft;
                    break;
                case TextAnchor.LowerRight:
                    TextBox.alignment = TextAnchor.LowerLeft;
                    break;
            }
            TextBox.text = "" + LanguageManager.Instance.GetTextValue(CurrKey);
        }
    }

    public void OnLanguageChangeListener(TextMesh TextBox, string CurrKey)
    {
        if (LanguageHandler.instance.Languages[LanguageHandler.instance.CurrentLanguageIndex]._Alignment==LanguageData.LanguageTextAlignment.RightToLeft)
        {
            TextBox.alignment = TextAlignment.Right;

            TextBox.text = ArabicFixer.Fix("" + LanguageManager.Instance.GetTextValue(CurrKey));
        }
        else
        {         
            TextBox.alignment = TextAlignment.Left;
         
            TextBox.text = "" + LanguageManager.Instance.GetTextValue(CurrKey);
        }
    }

    public void PlayVoiceOver(string key)
    {
        if (GlobalAudioSrc.Instance.audioSrc.isPlaying && GlobalAudioSrc.Instance.audioSrc.clip.name == key)
            return;
        
        GlobalAudioSrc.Instance.audioSrc.clip = null;
        AudioClip _Clip = Resources.Load<AudioClip>("VoiceOvers/" + LanguageHandler.instance.Languages[LanguageHandler.instance.CurrentLanguageIndex].LanguageID + "/" + key);
        GlobalAudioSrc.Instance.audioSrc.clip = _Clip;
        GlobalAudioSrc.Instance.audioSrc.PlayOneShot(_Clip);
    }

    public void StopVoiceOver()
    {
        if (GlobalAudioSrc.Instance == null)
            return;
        
        GlobalAudioSrc.Instance.audioSrc.Stop();
    }

    public void PlayBackMenuVoiceOver(string key)
    {
        if (GlobalAudioSrc.Instance.SecondAudioSrc.isPlaying && GlobalAudioSrc.Instance.SecondAudioSrc.clip.name == key)
            return;
        GlobalAudioSrc.Instance.SecondAudioSrc.clip = null;
        AudioClip _Clip = Resources.Load<AudioClip>("VoiceOvers/" + LanguageHandler.instance.Languages[LanguageHandler.instance.CurrentLanguageIndex].LanguageID + "/" + key);
        GlobalAudioSrc.Instance.SecondAudioSrc.clip = _Clip;
        GlobalAudioSrc.Instance.SecondAudioSrc.PlayOneShot(_Clip);
    }

    public void StopBackMenuVoiceOver()
    {
        if (GlobalAudioSrc.Instance == null)
            return;

        GlobalAudioSrc.Instance.SecondAudioSrc.Stop();
    }

    public bool CheckIfLanguageExist(string _currentLanguage)
    {
        List < SmartCultureInfo > languages = SmartLocalization.LanguageManager.Instance.GetSupportedLanguages();

        for (int i = 0; i < languages.Count; i++)
        {
            if (languages[i].languageCode == _currentLanguage)
            {
                return true;
            }
        }
        return false;
    }
}
