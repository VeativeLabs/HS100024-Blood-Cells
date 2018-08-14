using UnityEngine;
using UnityEngine.UI;
using SmartLocalization;
using ArabicSupport;

public enum LocalizeType
{
    Only_Text,
    Only_Voice,
    TextAndVoice,
    Only_Font
};

public class LocaliseTextAndVoiceOver : MonoBehaviour
{
    public LocalizeType localizeType;
    public bool StopVOOnDisable;
    public bool DoNotAlign;
    bool IsFirstTime = true;
	public bool DoNotChangeFont = false;

    static string[] AbsoluteVOKeys = {"completed _simulation.", "Title"};

    void CallLanguageChangeListener()
    {		
        if (GetComponentInChildren < TextMesh >() == null)
        {
            OnLanguageChangeListener();
        }
        else
        {			
            OnLanguageChangeListener_textMesh();
        }
    }

    void Start()
    {
        if (localizeType == LocalizeType.Only_Text)
        {
            CallLanguageChangeListener();
            IsFirstTime = false;
        }

        if (localizeType == LocalizeType.TextAndVoice)
        {
            CallLanguageChangeListener();
            PlayVoiceOver();
            IsFirstTime = false;
        }

        if (localizeType == LocalizeType.Only_Voice)
        {
            PlayVoiceOver();
            IsFirstTime = false;
        }

        if (localizeType == LocalizeType.Only_Font)
        {
            ChangeFont();
            IsFirstTime = false;
        }
    }

    void OnEnable()
    {
        if (localizeType == LocalizeType.Only_Text || localizeType == LocalizeType.TextAndVoice)
        {			
            if (GetComponentInChildren < TextMesh >() == null)
            {				
                LanguageHandler.LanguageChangeEventFire += OnLanguageChangeListener;
            }
            else
            {
                LanguageHandler.LanguageChangeEventFire += OnLanguageChangeListener_textMesh;			
            }
        }

        if (!IsFirstTime)
        {
            if (localizeType == LocalizeType.Only_Text)
            {
                CallLanguageChangeListener();
            }

            if (localizeType == LocalizeType.TextAndVoice)
            {
                CallLanguageChangeListener();
                PlayVoiceOver();
            }

            if (localizeType == LocalizeType.Only_Voice)
            {
                PlayVoiceOver();
            }
            if (localizeType == LocalizeType.Only_Font)
            {              
                ChangeFont();
            }
        }
    }

    void OnDisable()
    {		
        if (localizeType == LocalizeType.Only_Text || localizeType == LocalizeType.TextAndVoice)
        {			
            if (GetComponentInChildren < TextMesh >() == null)
            {
                LanguageHandler.LanguageChangeEventFire -= OnLanguageChangeListener;
            }
            else
            {				
                LanguageHandler.LanguageChangeEventFire -= OnLanguageChangeListener_textMesh;
            }
        }

        if (StopVOOnDisable)
        {
            if ((localizeType == LocalizeType.Only_Voice || localizeType == LocalizeType.TextAndVoice))
            {
                StopVoiceOver();
            }
        }
    }

    public void OnLanguageChangeListener()
    {
        if (LanguageHandler.instance.Languages[LanguageHandler.instance.CurrentLanguageIndex]._Alignment == LanguageData.LanguageTextAlignment.LeftToRight)
        {
            if (!DoNotAlign)
            {
                switch (GetComponentInChildren < Text >().alignment)
                {
                    case TextAnchor.UpperRight:
                        GetComponentInChildren < Text >().alignment = TextAnchor.UpperLeft;
                        break;
                    case TextAnchor.MiddleRight:
                        GetComponentInChildren < Text >().alignment = TextAnchor.MiddleLeft;
                        break;
                    case TextAnchor.LowerRight:
                        GetComponentInChildren < Text >().alignment = TextAnchor.LowerLeft;
                        break;
                }
            }

            if (!DoNotChangeFont)
            {
                GetComponentInChildren < Text >().font = Resources.Load<Font>("Fonts/VeativeEnglish");
            }
            GetComponentInChildren < Text >().text = "" + LanguageManager.Instance.GetTextValue(gameObject.name);

        }
        else
        {
            if (!DoNotAlign)
            {
                switch (GetComponentInChildren < Text >().alignment)
                {
                    case TextAnchor.UpperLeft:
                        GetComponentInChildren < Text >().alignment = TextAnchor.UpperRight;
                        break;
                    case TextAnchor.MiddleLeft:
                        GetComponentInChildren < Text >().alignment = TextAnchor.MiddleRight;
                        break;
                    case TextAnchor.LowerLeft:
                        GetComponentInChildren < Text >().alignment = TextAnchor.LowerRight;
                        break;
                }
            }

            if (!DoNotChangeFont)
            {
                GetComponentInChildren < Text >().font = Resources.Load<Font>("Fonts/VeativeArabic");
            }
            GetComponentInChildren < Text >().text = ArabicFixer.Fix("" + LanguageManager.Instance.GetTextValue(gameObject.name), false, false);
        }
    }

    public void OnLanguageChangeListener_textMesh()
    {        
        if (LanguageHandler.instance.Languages[LanguageHandler.instance.CurrentLanguageIndex]._Alignment == LanguageData.LanguageTextAlignment.LeftToRight)
        {
            if (GetComponentInChildren < TextMesh >().alignment != TextAlignment.Center && !DoNotAlign)
            {				
                GetComponentInChildren < TextMesh >().alignment = TextAlignment.Left;
            }

            if (!DoNotChangeFont)
            {
                GetComponentInChildren < TextMesh >().font = Resources.Load<Font>("Fonts/VeativeEnglish");
                GetComponentInChildren < MeshRenderer >().material = Resources.Load<Font>("Fonts/VeativeEnglish").material;
            }

            GetComponentInChildren<TextMesh>().text = "" + LanguageManager.Instance.GetTextValue(gameObject.name);

        }
        else
        {
            if (GetComponentInChildren < TextMesh >().alignment != TextAlignment.Center && !DoNotAlign)
            {			
                GetComponentInChildren < TextMesh >().alignment = TextAlignment.Right;			
            }

            if (!DoNotChangeFont)
            {
                GetComponentInChildren < TextMesh >().font = Resources.Load<Font>("Fonts/VeativeArabic");
                GetComponentInChildren < MeshRenderer >().material = Resources.Load<Font>("Fonts/VeativeArabic").material;
            }

            GetComponentInChildren < TextMesh >().text = ArabicFixer.Fix("" + LanguageManager.Instance.GetTextValue(gameObject.name), false, false);		
        }
    }

    public void PlayVoiceOver()
    {
        if (GlobalAudioSrc.Instance == null)
            return;
        
        GlobalAudioSrc.Instance.audioSrc.clip = null;
        AudioClip _Clip = Resources.Load < AudioClip >("VoiceOvers/" + LanguageHandler.instance.Languages[LanguageHandler.instance.CurrentLanguageIndex].LanguageID + "/" + gameObject.name);
        GlobalAudioSrc.Instance.audioSrc.clip = _Clip;
        GlobalAudioSrc.Instance.audioSrc.PlayOneShot(_Clip);
    }

    public void PlayVoiceOverForGaze()
    {
        if (GlobalAudioSrc.Instance == null)
            return;
        
        if (IsPlayingAbsoluteVoice())
            return;
        
        PlayVoiceOver();
    }

    public void StopVoiceOver()
    {
        if (GlobalAudioSrc.Instance == null)
            return;

        if (IsPlayingAbsoluteVoice())
            return;
        GlobalAudioSrc.Instance.audioSrc.Stop();
    }

    public bool IsPlayingAbsoluteVoice()
    {
        if (GlobalAudioSrc.Instance.audioSrc.clip != null && GlobalAudioSrc.Instance.audioSrc.isPlaying)
        {
            for (int i = 0; i < AbsoluteVOKeys.Length; i++)
            {
                if (GlobalAudioSrc.Instance.audioSrc.clip.name.Equals(AbsoluteVOKeys[i]))
                    return true;
            }
            return false;
        }
        else
            return false;
    }

    void ChangeFont(){
        if (LanguageHandler.instance.Languages[LanguageHandler.instance.CurrentLanguageIndex]._Alignment == LanguageData.LanguageTextAlignment.LeftToRight)
        {
            if (GetComponentInChildren < TextMesh >() == null)
            {
                GetComponentInChildren < Text >().font = Resources.Load<Font>("Fonts/VeativeEnglish");
            }
            else
            {           
                GetComponentInChildren < TextMesh >().font = Resources.Load<Font>("Fonts/VeativeEnglish");
                GetComponentInChildren < MeshRenderer > ().material = Resources.Load<Font>("Fonts/VeativeEnglish").material;
            }
        }
        else
        {
            if (GetComponentInChildren < TextMesh >() == null)
            {
                GetComponentInChildren < Text >().font = Resources.Load<Font>("Fonts/VeativeArabic");
            }
            else
            {           
                GetComponentInChildren < TextMesh >().font = Resources.Load<Font>("Fonts/VeativeArabic");
                GetComponentInChildren < MeshRenderer > ().material = Resources.Load<Font>("Fonts/VeativeArabic").material;
            }
        }
    }
}