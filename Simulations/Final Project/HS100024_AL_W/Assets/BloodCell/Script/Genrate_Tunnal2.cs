using UnityEngine;
using System.Collections;

public class Genrate_Tunnal2 : MonoBehaviour {
	public GameObject genrateObject;
//	public float gap=1.176f;
	public float gap=2.94f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		if(other.gameObject.CompareTag("Player")){
			genrateObject.transform.position = new Vector3(this.transform.position.x,this.transform.position.y,this.transform.position.z + gap);
		}

	}

}
