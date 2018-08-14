using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


public class ChangeUiImage : MonoBehaviour {

	public List<Sprite> images;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (FixVeriable.PartNumber == FixVeriable.collection) {
			this.GetComponent<Image> ().sprite = images [FixVeriable.collectNumber];
		}
	}
}
