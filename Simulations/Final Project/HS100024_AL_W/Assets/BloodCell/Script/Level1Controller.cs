using UnityEngine;
using System.Collections;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine.UI;
using SmartLocalization;

public class Level1Controller : MonoBehaviour {
	private GameObject positionDiscription;
	private string ButtonName="";
	public string[] Tags;
	private int TotalcollectCell=2;
	// Use this for initialization
	void Awake(){
		positionDiscription = GameObject.Find ("PositionDiscription").gameObject;

	}

	void OnEnable () {
		this.DOKill (true);
		this.GetComponent<Collider> ().enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void Start()
	{
		
	}
	public void part1Start(){
		FixVeriable.PartNumber = FixVeriable.discription;
	}

	public void part2Start()
	{ 
//		string obj_name;
		FixVeriable.PartNumber = FixVeriable.collection;
		FixVeriable.collectNumber = 0;
		CanvasController.instance.showHint();

//		print (FixVeriable.collectNumber);
//		print (LanguageManager.Instance.GetTextValue (Tags [FixVeriable.collectNumber]));
		CanvasController.instance.setScore(Tags [FixVeriable.collectNumber],PlayerPrefs.GetInt (Tags [FixVeriable.collectNumber])+"/"+TotalcollectCell);
//		CanvasController.instance.setScore(LanguageManager.Instance.GetTextValue (Tags [FixVeriable.collectNumber])+":"+PlayerPrefs.GetInt (Tags [FixVeriable.collectNumber])+"/"+TotalcollectCell);

	}

	public void PointerEnter(Collider obj){
		SoundManager.sm.PlayClickSound ();
		if (!FixVeriable.isPlayerStill && FixVeriable.PartNumber==FixVeriable.discription) {
			obj.gameObject.transform.DOMove (positionDiscription.transform.position, 1f);
//			SoundManager.sm.PlayRightSound();
			Invoke ("disableScript",2);
		}
		if(FixVeriable.PartNumber==FixVeriable.collection){
			if (obj.gameObject.tag.Equals (Tags [FixVeriable.collectNumber])) {
				PlayerPrefs.SetInt (Tags [FixVeriable.collectNumber], PlayerPrefs.GetInt (Tags [FixVeriable.collectNumber]) + 1);
				PlayerPrefs.Save ();
				CanvasController.instance.setScore(Tags [FixVeriable.collectNumber],PlayerPrefs.GetInt (Tags [FixVeriable.collectNumber])+"/"+TotalcollectCell);
				//show welldone
//				SoundManager.sm.PlayRightSound();
				CanvasController.instance.showWelldone();
//				this.transform.DOMove (Camera.main.transform.GetChild(0).position, 1).OnComplete (destroythis);
				destroythis();
				if(PlayerPrefs.GetInt (Tags [FixVeriable.collectNumber])>=TotalcollectCell){
					PlayerPrefs.SetInt (Tags [FixVeriable.collectNumber],0);
					PlayerPrefs.Save ();
					FixVeriable.collectNumber += 1;
					if (FixVeriable.collectNumber >= Tags.Length) {
						FixVeriable.PartNumber = -1;
						CanvasController.instance.showEndLevel2dialog();

//						Camera.main.GetComponent<GvrPointerPhysicsRaycaster> ().enabled = false;
					}
					CanvasController.instance.setScore(Tags [FixVeriable.collectNumber],PlayerPrefs.GetInt (Tags [FixVeriable.collectNumber])+"/"+TotalcollectCell);

				}
			} else {
				//show tryAgain
				CanvasController.instance.showTryAgain(Tags [FixVeriable.collectNumber]);
//				SoundManager.sm.PlayWrongSound();

			}
		}
	}

	public void destroythis(){
		this.gameObject.SetActive (false);
	}
		
	private void disableScript(){
		this.GetComponent<Level1Controller> ().enabled = false;
		this.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY;

	}

	public void showButton(string gameObjectName){
		if (!FixVeriable.isPlayerStill && FixVeriable.PartNumber==FixVeriable.discription) {
			ButtonName = gameObjectName;
			if (!CanvasController.display.Contains (gameObjectName)) {
				CanvasController.display.Add (gameObjectName);
			}
			Invoke ("show", 2);
		}
	}

	public void show(){
		CanvasController.instance.showDiscription(ButtonName);
	}

	public void Restart(){
		Application.LoadLevel (Application.loadedLevel);
	}

//	public void Gotolevel2(){
//
//	}
}
