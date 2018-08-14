using UnityEngine;
using System.Collections;
using DG.Tweening;

public class zoomInOut : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetButton ("Fire1")) {
			float speed=Input.GetAxis ("Vertical")*Time.deltaTime;
			if (speed < 0)
				transform.Translate ((this.transform.position - GameObject.Find ("zoomingpoint").transform.position) * speed, Space.World);
			else {
				transform.Translate ((GameObject.Find ("PositionDiscription").transform.position-this.transform.position) * speed, Space.World);
			}
		}
	
	}
}
