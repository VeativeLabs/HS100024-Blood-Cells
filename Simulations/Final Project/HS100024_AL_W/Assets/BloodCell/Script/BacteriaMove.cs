using UnityEngine;
using System.Collections;
using DG.Tweening;

public class BacteriaMove : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.gameObject.transform.DOShakeRotation (.5f, 60f, 3, 90, false).SetLoops (-1);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
