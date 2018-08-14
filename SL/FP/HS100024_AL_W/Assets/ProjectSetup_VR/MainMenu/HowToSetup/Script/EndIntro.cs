using UnityEngine;
using System.Collections;

public class EndIntro : MonoBehaviour
{
    public Animator anim;
    public HeadRotation HeadRot;

    void Start()
    {
        GetComponent<Animator>().enabled = false;
        anim.enabled = true;
        HeadRot.RotationAbility = false;
    }
}
