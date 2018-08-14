using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AlertPanelManager : MonoBehaviour 
{
    public static AlertPanelManager apm;
    private Text alertMessageText;
    LocaliseTextAndVoiceOver _ltvo;
    AudioClip _clip;
    public float _defaultHideTime = 4;
	public LocalizeType _localizeType = LocalizeType.Only_Text;

    void Awake() 
    { 
        apm = this;
        alertMessageText = transform.GetChild(0).GetComponentInChildren<Text>();
    }

    void Start()
    {
        if (transform.GetChild(0).GetComponent<LocaliseTextAndVoiceOver>() == null)
            gameObject.transform.GetChild(0).gameObject.AddComponent<LocaliseTextAndVoiceOver>();
        _ltvo = transform.GetChild(0).GetComponent<LocaliseTextAndVoiceOver>();
		_ltvo.localizeType = LocalizeType.TextAndVoice;
    }

    public void ShowAlertPanel() 
    {
        VrSelector.instance._CardboardPointer.GetComponent<MeshRenderer>().enabled = false;
        transform.GetChild(0).gameObject.SetActive(true);

        _clip = Resources.Load < AudioClip >("VoiceOvers/" + LanguageHandler.instance.Languages[LanguageHandler.instance.CurrentLanguageIndex].LanguageID +"/"+transform.GetChild(0).name);
        if (_clip != null)
        {
			Invoke("HideAlertPanel", _clip.length+1);
        }
        else
        {
            Invoke("HideAlertPanel", _defaultHideTime);
        }
    }

    public void HideAlertPanel() 
    {
        VrSelector.instance.EnableUIRayCasters();
        VrSelector.instance.EnablePhysicsRayCasters();
        VrSelector.instance._CardboardPointer.GetComponent<MeshRenderer>().enabled = true;
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public void ShowAlertMessage (string alertMessage)
    {
        if (alertMessage == "Internal server error")
        {
            transform.GetChild(0).name = "C_Inst_ServerError";
        }
        else
        {
            transform.GetChild(0).name = "C_Inst_CheckNetworkConnection";
        }

        VrSelector.instance.DisableUIRayCasters();
        VrSelector.instance.DisablePhysicsRayCasters();

        Invoke ( "ShowAlertPanel" , 0.1f );
    }
}
