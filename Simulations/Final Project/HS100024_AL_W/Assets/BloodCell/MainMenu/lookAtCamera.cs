using UnityEngine;
using System.Collections;

public class lookAtCamera : MonoBehaviour {
    public GameObject CameraUI;
    Vector3 offset;
	// Use this for initialization
	void Start () {
        offset = Camera.main.transform.position - transform.position;
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Camera.main.transform.eulerAngles.x < 25 || Camera.main.transform.eulerAngles.x > 330)
        {
            //  CameraUI.SetActive(true);
            CameraUI.transform.localScale = new Vector3(0.0001f, 0.0001f, 0.0001f);// Vector3.one;

        }
        else {
           // CameraUI.SetActive(false);
            CameraUI.transform.localScale = Vector3.zero;


        }
        // offset = Camera.main.transform.position - transform.position;
        //Camera.main.transform.position= transform.position  +offset;
        // transform.rotation = Camera.main.transform.rotation;

        // transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Camera.main.transform.localEulerAngles.y, transform.localEulerAngles.z);

        // transform.localEulerAngles = new Vector3(Camera.main.transform.localEulerAngles.x, transform.localEulerAngles.y, Camera.main.transform.localEulerAngles.z);
    }
}
