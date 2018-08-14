using UnityEngine;
using System.Collections;

public class RandomRotation : MonoBehaviour {
	public float speed = .6f;
	// Use this for initialization
	void Start () {
		
	}

	void OnEnable(){
		GetComponent<Rigidbody> ().AddRelativeTorque (Vector3.one * Random.Range (1f, .5f), ForceMode.VelocityChange);

	}

	// Update is called once per frame
	void Update () {
//		this.transform.Rotate(Vector3.one * speed * Time.deltaTime,Space.World);
		if (Camera.main.transform.position.z-this.transform.position.z<-.45f) {
			this.gameObject.SetActive (false);
		}
	}
		
}
