using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class Genrate_Tunnal : MonoBehaviour {

	public GameObject genrateObject;
	public float gap = -1.176f;
	public Material mat;
	public Texture healedTextur;
	public List<Texture> infectedTexture;
	// Use this for initialization
	void Start () {
		if (mat != null) {
			mat.mainTexture = infectedTexture[0];
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {
		if(other.gameObject.CompareTag("Player")){
			genrateObject.transform.position = new Vector3(this.transform.position.x,this.transform.position.y,this.transform.position.z + gap);
			print("genrate");
			if (this.tag.Equals ("Enfected")) {
				Invoke ("startActivity", .5f);
			} else {
				if(SceneManager.GetActiveScene().name.Equals("Blood_Cell_level2"))
					Invoke ("MakeLevelforNext", .1f);
			}
		}

	}

	void OnTriggerExit(Collider other){
		if(other.gameObject.CompareTag("Player")){
//			if(this.tag.Equals("Enfected")){
//				Invoke ("MakeLevelforNext", 1f);
//			}
		}

	}

	void genrateArea(int partNumber){
		mat.mainTexture = infectedTexture[partNumber];
	}

	public void startActivity(){
		FixVeriable.isPlayerStill = true;
		switch (FixVeriable.collectNumber) {
		case -1:
			Level2Controller.instance.startActivity0 ();
			break;
		case 0:
			Level2Controller.instance.startActivity1 ();
			break;
		case 1:
			Level2Controller.instance.startActivity2 ();
			break;
		case 2:
			Level2Controller.instance.startActivity3 ();
			break;
		case 3:
			Level2Controller.instance.startActivity4();
			break;
		case 4:
			Level2Controller.instance.startActivity5 ();
			break;
		case 5:
			Level2Controller.instance.startActivity6 ();
			break;
		default:
			break;
		}
	}

	public void MakeLevelforNext(){
		switch (FixVeriable.collectNumber) {
		case 0:
			Genratebacteria.instance.makeBacteria (0);
			mat.mainTexture = infectedTexture [1];
			break;
		case 1:
			Genratebacteria.instance.makeBacteria (1);
			mat.mainTexture = infectedTexture [2];
			break;
		case 2:
			Genratebacteria.instance.makeBacteria (2);
			mat.mainTexture = infectedTexture [3];
			break;
		case 3:
			Genratebacteria.instance.makeBacteria (3);
			mat.mainTexture = infectedTexture [4];
			break;
		case 4:
			mat.mainTexture = infectedTexture [5];
			break;
		case 5:
			mat.mainTexture = infectedTexture [6];
			break;
		default:
			break;
		}
	}

	public void EndActivity0(){
		StartCoroutine (changemat());
		GameObject.Find ("Gun").GetComponent<shootingCell> ().destroyAllObject ();
	}

	IEnumerator changemat(){
		yield return new WaitForEndOfFrame();
		if (mat != null) {
			mat.mainTexture = healedTextur;
		}
	}

}
