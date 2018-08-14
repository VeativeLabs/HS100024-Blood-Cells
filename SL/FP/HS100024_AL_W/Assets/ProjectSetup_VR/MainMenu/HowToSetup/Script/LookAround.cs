using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LookAround : MonoBehaviour
{
    public HeadRotation PlayerHeadRot;
    public Transform MainCamera;

    public GameObject LeftArrow, RightArrow;

    public GameObject PlayerObject;
    Animator anim;
    float time;
    public float MsgInterval;

    bool LeftLooked,RightLooked;

    void Start()
    {
        anim = PlayerObject.GetComponent<Animator>();
        PlayerHeadRot.RotationAbility = true;
        LeftLooked = false;
        RightLooked = false;
        LeftArrow.SetActive(true);
        RightArrow.SetActive(true);
        time = 0;
    }

    void Update()
    {
		if (Time.timeScale == 0) {
			return;
		}

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

        if (MainCamera.localRotation.y < -0.2 && !LeftLooked)
        {
            LeftLooked = true;
            time = 0;
            StopVoice();
            LeftArrow.SetActive(false);
        }
        if (MainCamera.localRotation.y > 0.2 && !RightLooked)
        {
            RightLooked = true;
            time = 0;
            StopVoice();
            RightArrow.SetActive(false);
        }

        if (LeftLooked == true && RightLooked == true)
        {
            time = 0;
            PlayerHeadRot.RotationAbility = false;
            anim.SetTrigger("WellDone");
        }
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
