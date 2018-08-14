using UnityEngine;
using System.Collections;

public class BelowUiEnableDisable : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Camera.main.transform.localEulerAngles.y, transform.localEulerAngles.z);

		transform.position = Camera.main.transform.position + Camera.main.transform.forward * 0.06f;
		transform.position = new Vector3(transform.position.x,Camera.main.transform.position.y - 0.05f, transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
        //if (Camera.main.transform.eulerAngles.x < 25 || Camera.main.transform.eulerAngles.x > 330)
        //{
//      Debug.Log(Camera.main.GetComponent<GvrHead>().trackRotation);
        if (Input.GetAxis("Vertical") == 0)
        {
            //  CameraUI.SetActive(true);
            transform.localScale = Vector3.one;//new Vector3 ( 0.1f , 0.1f , 0.1f );//Vector3.one;
            //transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Camera.main.transform.localEulerAngles.y-5, transform.localEulerAngles.z);
            //transform.position = Camera.main.transform.position + Camera.main.transform.forward * 0.4f;


        }
        else
        {
            // CameraUI.SetActive(false);
        transform.localScale = Vector3.zero;
           // transform.rotation = Camera.main.transform.rotation;
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Camera.main.transform.localEulerAngles.y, transform.localEulerAngles.z);

            transform.position = Camera.main.transform.position + Camera.main.transform.forward * 0.06f;
            transform.position = new Vector3(transform.position.x,Camera.main.transform.position.y - 0.05f, transform.position.z);
          



        }

    }
}
