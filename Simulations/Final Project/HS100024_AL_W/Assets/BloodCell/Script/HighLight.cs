using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HighLight : MonoBehaviour {
	public static HighLight instance;
	public List<GameObject> bullets; 

	void Awake(){
		instance = this;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void highLight(int cell){
		bullets [cell].GetComponent<Animator>().enabled=true;
	}

	public void wrongAns(int cell){
		bullets [cell].GetComponentInChildren<Text> ().color = new Color32 (255, 0,32,255);
//		SoundManager.sm.PlayWrongSound ();
	}

	public void rightAns(int cell){
		bullets [cell].GetComponentInChildren<Text> ().color = new Color32 (102, 135,72,255);
	}

	public void highLightstop(int cell){
		bullets [cell].GetComponent<Animator>().enabled=false;
		bullets [cell].GetComponentInChildren<Text> ().color =  new Color32 (102, 135,72,255);
//		SoundManager.sm.PlayRightSound ();
	}

	public void restart(){
		for(int i=0;i<bullets.Count;i++) {
			bullets[i].GetComponent<Animator> ().enabled = false;
			bullets[i].GetComponentInChildren<Text> ().color = new Color (233f / 255f, 233f / 255f, 233f / 255f);
		}
	}

	public void colliderOff(){
		for(int i=0;i<bullets.Count;i++) {
			bullets[i].GetComponent<EventTrigger> ().enabled = false;
			bullets[i].GetComponent<Button> ().enabled = false;
			bullets [i].transform.GetChild (1).gameObject.SetActive (false);
		}
	}

	public void ColliderOn(){
		for(int i=0;i<bullets.Count;i++) {
			bullets[i].GetComponent<EventTrigger> ().enabled = true;
			bullets[i].GetComponent<Button> ().enabled = true;
		}
	}
}
