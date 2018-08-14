using UnityEngine;
using DG.Tweening;
using System.Collections;

public class HeadRotation : MonoBehaviour
{
    public bool RotationAbility = true;
//    GvrHead GHead;
//    public GvrViewer GViewer;
    bool CheckBool;

    void Start()
    {
        CheckBool = true;
//        GHead = Camera.main.transform.GetComponent<GvrHead>();
//        GHead.trackRotation = true;
    }


    void Update()
    {
        

        if (CheckBool != RotationAbility)
        {
            CheckBool = RotationAbility;
            if (RotationAbility)
            {
//                GViewer.Recenter();
                Camera.main.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
//                GHead.trackRotation = true;

            }
            else
            {
//                GHead.trackRotation = false;
                Camera.main.transform.DOLocalRotateQuaternion(Quaternion.identity, 0.5f);
            }
        }
    }

}
