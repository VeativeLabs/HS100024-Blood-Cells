using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropDownForVR : MonoBehaviour 
{
    public Transform _BlockerParent;
	// Use this for initialization
	void Start () 
    {
        GetComponent<Canvas>().overrideSorting = false;
        //gameObject.AddComponent<OVRRaycaster>();
        gameObject.AddComponent<GvrPointerGraphicRaycaster>();
        Destroy(GetComponent<GraphicRaycaster>());
        Destroy(_BlockerParent.Find("Blocker").gameObject);
	}
}
