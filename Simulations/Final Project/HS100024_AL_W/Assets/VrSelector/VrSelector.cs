using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;
using UnityEngine.EventSystems;

public class VrSelector : MonoBehaviour
{
    public static VrSelector instance;
    public GameObject _EditorEmulator, _CardboardPointer, _DaydreamController, _OvrController;
    Canvas[] _canvas;

    void Awake()
    {
        instance = this;
        _EditorEmulator.SetActive(false);
        _CardboardPointer.SetActive(false);
        _DaydreamController.SetActive(false);
        _OvrController.SetActive(false);
        _canvas = FindObjectsOfType<Canvas>();
    }

    void Start()
    {
        #if UNITY_EDITOR
        _EditorEmulator.SetActive(true);
        _EditorEmulator.GetComponent<GvrEditorEmulator>().Recenter();
        #endif

        InputTracking.Recenter();

        switch (VRSettings.supportedDevices[0])
        {
            case "cardboard":
                _CardboardPointer.transform.SetParent(Camera.main.transform);
                _CardboardPointer.transform.localPosition = Vector3.zero;
                _CardboardPointer.transform.localRotation = Quaternion.identity;
                _CardboardPointer.SetActive(true);
                break;

            case "daydream":
                _DaydreamController.transform.SetParent(Camera.main.transform.parent);
                _DaydreamController.transform.position = Camera.main.transform.position;
                _DaydreamController.transform.rotation = Camera.main.transform.rotation;
                _DaydreamController.SetActive(true);
                break;

            case "Oculus":
                _OvrController.transform.SetParent(Camera.main.transform.parent);
                _OvrController.transform.position = Camera.main.transform.position;
                _OvrController.transform.rotation = Camera.main.transform.rotation;
                _OvrController.SetActive(true);
                break;
        }
    }

    public void EnablePhysicsRayCasters()
    {
        if(Camera.main.GetComponent<GvrPointerPhysicsRaycaster>()!=null)
            Camera.main.GetComponent<GvrPointerPhysicsRaycaster>().enabled = true;

//        if(_OvrController.GetComponent<OVRPhysicsRaycaster>()!=null)
//            _OvrController.GetComponent<OVRPhysicsRaycaster>().enabled = true;

    }

    public void DisablePhysicsRayCasters()
    {
        if(Camera.main.GetComponent<GvrPointerPhysicsRaycaster>()!=null)
            Camera.main.GetComponent<GvrPointerPhysicsRaycaster>().enabled = false;

//        if(_OvrController.GetComponent<OVRPhysicsRaycaster>()!=null)
//            _OvrController.GetComponent<OVRPhysicsRaycaster>().enabled = false;
    }

    public void EnableUIRayCasters()
    {
        for (int i = 0; i < _canvas.Length; i++)
        {
            if (_canvas[i].GetComponent<GvrPointerGraphicRaycaster>() != null)
                _canvas[i].GetComponent<GvrPointerGraphicRaycaster>().enabled = true;

            if (_canvas[i].GetComponent<CurvedUI.CurvedUIRaycaster>() != null)
                _canvas[i].GetComponent<CurvedUI.CurvedUIRaycaster>().enabled = true;
            
//            if (_canvas[i].GetComponent<OVRRaycaster>() != null)
//                _canvas[i].GetComponent<OVRRaycaster>().enabled = true;
        }

    }

    public void DisableUIRayCasters()
    {
        for (int i = 0; i < _canvas.Length; i++)
        {
            if (_canvas[i].GetComponent<GvrPointerGraphicRaycaster>() != null)
                _canvas[i].GetComponent<GvrPointerGraphicRaycaster>().enabled = false;

            if (_canvas[i].GetComponent<CurvedUI.CurvedUIRaycaster>() != null)
                _canvas[i].GetComponent<CurvedUI.CurvedUIRaycaster>().enabled = false;

//            if (_canvas[i].GetComponent<OVRRaycaster>() != null)
//                _canvas[i].GetComponent<OVRRaycaster>().enabled = false;
        }
    }
}
