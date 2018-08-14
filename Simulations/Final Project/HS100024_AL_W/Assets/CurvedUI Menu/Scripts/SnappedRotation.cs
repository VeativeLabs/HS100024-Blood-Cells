using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class SnappedRotation : MonoBehaviour
{
    public float rotateAngle = 20;

    public float stripOne = 1f;
    public float stripTwo = 0.3f;

    // DayDream Axis
    public static float GetAxis(string direction)
    {
        if (!GvrController.IsTouching)
            return 0;
        Vector2 input = (GvrController.TouchPos - new Vector2(0.5f, 0.5f)) * 2.0f;
        if (direction == "Horizontal")
        {
            return input.x;
        }
        else if (direction == "Vertical")
        {
            return-input.y;
        }
        else
        {
            return 0;
        }
    }

    void Update()
    {
        if (VRSettings.supportedDevices[0] == "daydream")
        {
            if (!(GetAxis("Horizontal") >= -0.4f && GetAxis("Horizontal") <= 0.4f && GetAxis("Vertical") >= -0.4f && GetAxis("Vertical") <= 0.4f))
            {
                if (GetAxis("Vertical") <= stripTwo && GetAxis("Vertical") >= -stripTwo && GetAxis("Horizontal") <= stripOne && GetAxis("Horizontal") >= -stripOne)
                {
                    //transform.Rotate(0, GetAxis("Horizontal") * rotateSpeed, 0);
                    if (GvrController.TouchDown)
                    {
                        if (GetAxis("Horizontal") > 0)
                        {
                            transform.Rotate(0, rotateAngle, 0);
                        }

                        if (GetAxis("Horizontal") < 0)
                        {
                            transform.Rotate(0, -rotateAngle, 0);
                        }
                    }
                }
            }
        }

//        else if (VRSettings.supportedDevices[0] == "Oculus")
//        {
//            forward = vrHead.TransformDirection(Vector3.forward);
//            Vector2 TouchAxis = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);
//        
//            if (TouchAxis.magnitude > 0.3f)
//            {
//                if (Mathf.Abs(TouchAxis.x) > Mathf.Abs(TouchAxis.y))
//                {
//                    if (OVRInput.GetDown(OVRInput.Button.PrimaryTouchpad))
//                    {
//                        if (TouchAxis.x > 0)
//                            transform.Rotate(0, rotateAngle, 0);
//        
//                        if (TouchAxis.x < 0)
//                            transform.Rotate(0, -rotateAngle, 0);
//                    }
//                }
//            }
//        
//            #if UNITY_EDITOR
//                    NormalController();
//            #endif
//        }

        else if (VRSettings.supportedDevices[0] == "cardboard")
        {
            NormalController();
        }
    }

    void NormalController()
    {
        if (Input.GetButtonDown("Horizontal"))
            transform.Rotate(0, rotateAngle * Input.GetAxisRaw("Horizontal"), 0);
    }
}
