using UnityEngine;
using System.Collections;

public class OnrotationTableDisable : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Camera.main.transform.eulerAngles.y < 359 && Camera.main.transform.eulerAngles.y > 130) {
			this.gameObject.SetActive (false);
		}

		if (Input.GetButtonDown ("Fire1") && (Camera.main.transform.eulerAngles.x > 9 && Camera.main.transform.eulerAngles.x < 345)) {
			this.gameObject.SetActive (false);
		}


		if (Input.GetButtonDown ("Fire1") && (Camera.main.transform.eulerAngles.y> 50 || Camera.main.transform.eulerAngles.y < 21)) {
			this.gameObject.SetActive (false);
		}
	}
}
