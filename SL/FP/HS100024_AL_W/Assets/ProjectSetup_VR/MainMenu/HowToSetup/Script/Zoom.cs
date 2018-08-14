using UnityEngine;
using System.Collections;

public class Zoom : MonoBehaviour
{
    public GameObject Vr_Player;
    public Transform Rocker;

    public GameObject PlayerObject;
    Animator anim;
    AudioSource aud;
    float time;
    public float MsgInterval;

    bool FrontPoint,BackPoint;
    bool end;
    // Use this for initialization
    void Start()
    {
        anim = PlayerObject.GetComponent<Animator>();
        anim.enabled = false;
        FrontPoint = false;
        BackPoint = false;
        end = false;
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!end)
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

            if (Vr_Player.transform.localPosition.x < -1  && !FrontPoint)
            {
                FrontPoint = true;
                time = 0;
            }
            if (FrontPoint && !BackPoint && Vr_Player.transform.localPosition.x == 0)
            {
                BackPoint = true;
                time = 0;
            }



            if (Input.GetAxisRaw("Vertical") > 0)
            {
                time = 0;
                StopVoice();
                Rocker.localRotation = Quaternion.Euler(new Vector3(0, 0, -15));
                Vr_Player.transform.localPosition -= new Vector3(Time.deltaTime*2,0,0);
                Vr_Player.transform.localPosition = new Vector3(Mathf.Clamp(Vr_Player.transform.localPosition.x,-3,0),Vr_Player.transform.localPosition.y,Vr_Player.transform.localPosition.z);
            }
            else if (Input.GetAxisRaw("Vertical") < 0)
            {
                time = 0;
                StopVoice();
                Rocker.localRotation = Quaternion.Euler(new Vector3(0, 0, 15));
                Vr_Player.transform.localPosition += new Vector3(Time.deltaTime*2,0,0);
                Vr_Player.transform.localPosition = new Vector3(Mathf.Clamp(Vr_Player.transform.localPosition.x,-3,0),Vr_Player.transform.localPosition.y,Vr_Player.transform.localPosition.z);
            }
            else
                Rocker.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));

            if (FrontPoint == true && BackPoint == true)
            {
                time = 0;
                Rocker.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                anim.enabled = true;
                anim.SetTrigger("WellDone");
                end = true;
            }

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
