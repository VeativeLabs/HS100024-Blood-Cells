using UnityEngine;
using System.Collections;
using DG.Tweening;

public class movePools : MonoBehaviour {

	public GameObject initialposition;
	public GameObject finalposition;
	// Use this for initialization
	void Start () {
//		this.transform.position = initialposition.transform.position;
//		this.transform.DOMove (finalposition.transform.position, 4);
	}

	void OnEnable(){
		this.transform.position = initialposition.transform.position;
		this.transform.DOMove (finalposition.transform.position, 4);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
