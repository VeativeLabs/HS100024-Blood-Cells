using UnityEngine;
using System.Collections;

public class showOst : MonoBehaviour {
	public GameObject ost;
	// Use this for initialization
	void Start () {
		
	}
		
	void OnEnable(){
		ost.SetActive (true);
	}
}
