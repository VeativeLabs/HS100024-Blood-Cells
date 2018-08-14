using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems; 

public class Select :  MonoBehaviour
{
    public Transform OkButton;
    public Color hl;
    public Image img;

    public GameObject PlayerObject;
    Animator anim;
    float time;
    public float MsgInterval;

  
    void Start()
    {
        anim = PlayerObject.GetComponent<Animator>();
        anim.enabled = false;
        time = 0;
    }
	

    void Update()
    {
        if (!GlobalAudioSrc.Instance.audioSrc.isPlaying)
        {
            time += Time.deltaTime;
            if (time >= MsgInterval)
                PlayVoice();
        }
        else
        {
            time = 0;
        }
    }


    public void OnPointerClick()
    {
        OkButton.localPosition = new Vector3(0.0265f,OkButton.localPosition.y,OkButton.localPosition.z);
        img.color = hl;
        time = 0;
        StopVoice();
        Invoke("ResetButton",0.25f);
    }

    void ResetButton()
    {
        OkButton.localPosition = new Vector3(0.02875616f,OkButton.localPosition.y,OkButton.localPosition.z);
        img.color = Color.white;
        time = 0;
        anim.enabled = true;
        anim.SetTrigger("WellDone");
    }

    void PlayVoice()
    {
        if(!transform.GetChild(0).gameObject.activeSelf)
            transform.GetChild(0).gameObject.SetActive(true);
        else
            GlobalAudioSrc.Instance.audioSrc.Play();
    }

    void StopVoice()
    {
        GlobalAudioSrc.Instance.audioSrc.Stop();
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
