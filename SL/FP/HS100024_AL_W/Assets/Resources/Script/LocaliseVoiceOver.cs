using UnityEngine;
using UnityEngine.UI;
using SmartLocalization;
using ArabicSupport;

public class LocaliseVoiceOver : MonoBehaviour {

    bool IsFirstTime = true;

    void Start() {
        PlayVoiceOver();
        IsFirstTime = false;
    }

	void OnEnable () {

        if (!IsFirstTime) {
            PlayVoiceOver();
        }
    }

    void Update() {
        

    }


    public void PlayVoiceOver() {
		GlobalAudioSrc.Instance.audioSrc.clip = Resources.Load<AudioClip>("VoiceOvers/" + LanguageHandler.instance.Languages[LanguageHandler.instance.CurrentLanguageIndex] + "/" + gameObject.name);
        GlobalAudioSrc.Instance.audioSrc.PlayOneShot(GlobalAudioSrc.Instance.audioSrc.clip , 1);
    }

}
