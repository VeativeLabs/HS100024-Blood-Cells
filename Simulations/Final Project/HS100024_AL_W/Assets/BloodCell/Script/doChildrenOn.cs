using UnityEngine;
using System.Collections;

public class doChildrenOn : MonoBehaviour {
	public GameObject obj;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void childActive(){
		obj.SetActive (true);
	}
}
