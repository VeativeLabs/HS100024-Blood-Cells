using UnityEngine;
using System.Collections;

public class VRSteeringCamera : MonoBehaviour {
	public float StartingRotation = 90;
	Transform cam;

	// Use this for initialization
	void Start () {
		cam = Camera.main.transform;
	}
	
	// Update is called once per frame
	void Update () {
		transform.localEulerAngles = new Vector3 (0 ,cam.transform.localEulerAngles.y, cam.transform.localEulerAngles.x - StartingRotation);
	}




}
