using UnityEngine;
using DG.Tweening;
using System.Collections;

public class DragDrop : MonoBehaviour
{
    public HeadRotation PlayerHeadRot;
    public Transform Book;
    public Transform BookParent;
    public Transform BookTarget;

    Vector3 InitPos;
    Quaternion InitRot;

    public GameObject PlayerObject;
    public Transform OkButton;
    Animator anim;
    float time;
    public float MsgInterval;

    bool end;


    void Start()
    {
        anim = PlayerObject.GetComponent<Animator>();
        anim.enabled = false;
        PlayerHeadRot.RotationAbility = true;
        time = 0;
        end = false;
        InitPos = Book.position;
        InitRot = Book.rotation;
        StartCoroutine(BlinkHalo());
    }

    IEnumerator BlinkHalo()
    {
        while (!end)
        {
            yield return new WaitForSeconds(0.1f);
            Book.GetChild(0).gameObject.SetActive(true);
            yield return new WaitForSeconds(0.25f);
            Book.GetChild(0).gameObject.SetActive(false);
        }
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

            if(Book.parent == BookParent)
            {
                if (Vector3.Distance(Book.position, BookTarget.position) < 0.6f && Book.position.y>BookTarget.position.y)
                {
                    Book.transform.DOMove(BookTarget.position,0.25f);
                    Book.transform.DORotateQuaternion(BookTarget.rotation,0.1f);
                    Invoke("WD",0.3f);
                    time = 0;
                    StopVoice();
                    end = true;
                }
                else
                {
                    Book.position = InitPos;
                    Book.rotation = InitRot;
                }
            }
        }

        if(Book.position != InitPos)
        {
            Book.GetChild(0).GetChild(0).gameObject.SetActive(false);
        }
        else
            Book.GetChild(0).GetChild(0).gameObject.SetActive(true);
    }

    public void OnPointerDown()
    {
        OkButton.localPosition = new Vector3(0.0265f,OkButton.localPosition.y,OkButton.localPosition.z);
    }

    public void OnPointerUp()
    {
        OkButton.localPosition = new Vector3(0.02875616f,OkButton.localPosition.y,OkButton.localPosition.z);
    }

    void WD()
    {
        PlayerHeadRot.RotationAbility = false;
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
