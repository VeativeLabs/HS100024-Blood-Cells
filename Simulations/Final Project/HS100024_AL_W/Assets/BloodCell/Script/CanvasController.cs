using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using SmartLocalization;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour {

	public static CanvasController instance;

	public GameObject MainCamera;
	public GameObject Main_Player;
	public GameObject rbcButton;
	public GameObject pletelet;
	public GameObject neutrophil;
	public GameObject eosinophil;
	public GameObject basophil;
	public GameObject lymphocyte;
	public GameObject monocyte;

// dialogBox
	public GameObject Part2dialog;
	public GameObject EndLevel2dialog;


// instructions(hint)
	public GameObject welldone;
	public GameObject tryagain;
	public GameObject collectRBC;
	public GameObject collectPletellet;
	public GameObject collectNeutrophil;
	public GameObject collectEosinophil;
	public GameObject collectBasophil;
	public GameObject collectLymphocyte;
	public GameObject collectMonocyte;
	public GameObject[] NumberofCells;

	public GameObject score;

//new added panel anfter new changes
	public GameObject Inst_LevelComplete;
	public static List<string> display=new List<string>();


//Different Tryagain panel for each Blood cells..
	public GameObject NotRBCs,NotPlatellets,NotNeutrophil,NotEosinophil,NotBasophil,NotLymphocyte,NotMonocyte;

	void Awake(){
		instance = this;

	}



	// Use this for initialization
	void Start () {
		display.Clear ();
		FixVeriable.PartNumber = -1;
		FixVeriable.collectNumber = -1;
		FixVeriable.isPlayerStill = false;
		Resources.UnloadUnusedAssets ();
	}
	
	// Update is called once per frame
	void Update () {
		if(!score.activeInHierarchy && FixVeriable.PartNumber== FixVeriable.collection){
			score.SetActive (true);
		}

	}

	public void setScore(string code,string str)
	{
		LanguageHandler.instance.OnLanguageChangeListener (score.GetComponentInChildren<Text> (), code);

		if (LanguageHandler.instance.IsRightToLeft) 
		{
			score.GetComponentInChildren<Text> ().text =  ArabicSupport.ArabicFixer.Fix(str) + ":" + score.GetComponentInChildren<Text> ().text;
		} 
		else 
		{
			score.GetComponentInChildren<Text> ().text =  score.GetComponentInChildren<Text>().text+" : "+ str;
		}

	}

	public void showDiscription(GameObject obj){
		obj.SetActive (true);
	}

	public void showDiscription(string str){
		for(int i=0;i<NumberofCells.Length;i++){
			NumberofCells[i].SetActive (false);
		}
		if(str.Equals("RBC_Button")){
			rbcButton.SetActive (true);
		}
		else if(str.Equals("Platellet_Button")){
			pletelet.SetActive (true);
		}
		else if(str.Equals("neutrophil _Button")){
			neutrophil.SetActive (true);
		}
		else if(str.Equals("eosinophil_Button")){
			eosinophil.SetActive (true);
		}
		else if(str.Equals("basophil_Button")){
			basophil.SetActive (true);
		}
		else if(str.Equals("lymphocyte_Button")){
			lymphocyte.SetActive (true);
		}
		else if(str.Equals("monocyte_Button")){
			monocyte.SetActive (true);
		}
	}

	public void RemoveObject(GameObject obj){
		obj.SetActive (false);
	}

	public void CountinueGame(){
		FixVeriable.isPlayerStill = false;

		if (CanvasController.display.Count >= 7) {
			FixVeriable.PartNumber = -1;
			// display collection part display
			Invoke("Camera_Raycaster_OFF",.5f);
			Main_Player.GetComponent<ConstantTranslate>().enabled = false;
			Part2dialog.SetActive (true);


		} else {
			NumberofCells[display.Count].SetActive (true);
		}
	}

	void Camera_Raycaster_OFF(){
		MainCamera.GetComponent<GvrPointerPhysicsRaycaster>().enabled = false;
	}

	public void showHint(){
		welldone.SetActive (false);
		tryagain.SetActive (false);
		NotRBCs.SetActive (false);
		NotPlatellets.SetActive (false);
		NotNeutrophil.SetActive (false);
		NotMonocyte.SetActive (false);
		NotLymphocyte.SetActive (false);
		NotEosinophil.SetActive (false);
		NotBasophil.SetActive (false);

		if(FixVeriable.PartNumber == FixVeriable.discription){
			
		}
		if(FixVeriable.PartNumber== FixVeriable.collection){
			if(FixVeriable.collectNumber==0){
				collectRBC.SetActive(true);
			}
			if(FixVeriable.collectNumber==1){
				collectPletellet.SetActive(true);
			}	
			if(FixVeriable.collectNumber==2){
				collectNeutrophil.SetActive(true);
			}	
			if(FixVeriable.collectNumber==3){
				collectEosinophil.SetActive(true);
			}	
			if(FixVeriable.collectNumber==4){
				collectBasophil.SetActive(true);
			}	
			if(FixVeriable.collectNumber==5){
				collectLymphocyte.SetActive(true);
			}	
			if(FixVeriable.collectNumber==6){
				collectMonocyte.SetActive(true);
			}
		}
		Camera.main.GetComponent<GvrPointerPhysicsRaycaster> ().enabled = true;
	}

	public void showEndLevel2dialog(){
		
//		Application.LoadLevel ("Blood_Cell_level2");

		Invoke ("LastPAnelLevelOne", GlobalAudioSrc.Instance.audioSrc.clip.length + .5f);
		score.SetActive (false);
//		EndLevel2dialog.SetActive (true);
	}

	void LastPAnelLevelOne(){
		MainCamera.GetComponent<GvrPointerPhysicsRaycaster> ().enabled = false;
		Main_Player.GetComponent<ConstantTranslate> ().enabled = false;
		Inst_LevelComplete.SetActive (true);
	}

	public void hideHint(){
		if(FixVeriable.PartNumber == FixVeriable.discription){

		}
		if(FixVeriable.PartNumber== FixVeriable.collection){
			if(FixVeriable.collectNumber==0){
				collectRBC.SetActive(false);
			}
			if(FixVeriable.collectNumber==1){
				collectPletellet.SetActive(false);
			}	
			if(FixVeriable.collectNumber==2){
				collectNeutrophil.SetActive(false);
			}	
			if(FixVeriable.collectNumber==3){
				collectEosinophil.SetActive(false);
			}	
			if(FixVeriable.collectNumber==4){
				collectBasophil.SetActive(false);
			}	
			if(FixVeriable.collectNumber==5){
				collectLymphocyte.SetActive(false);
			}	
			if(FixVeriable.collectNumber==6){
				collectMonocyte.SetActive(false);
			}	
		}
	}

	public void showWelldone(){
		CancelInvoke ();
		hideHint ();
		hideTryAgain ();
		welldone.SetActive (true);
//		Invoke ("showHint", 3);
		Camera.main.GetComponent<GvrPointerPhysicsRaycaster> ().enabled = false;
		Invoke("showHint",GlobalAudioSrc.Instance.audioSrc.clip.length +.2f);
	}
	public void showTryAgain(string str){
		CancelInvoke ();
		hideHint ();
		welldone.SetActive (false);
		Camera.main.GetComponent<GvrPointerPhysicsRaycaster> ().enabled = false;
//		tryagain.SetActive (true);
		switch (str) {
		case "RBC":
			NotRBCs.SetActive (true);
			break;
		case "Platelet":
			NotPlatellets.SetActive (true);
			break;
		case "Neutrophil":
			NotNeutrophil.SetActive (true);
			break;
		case "Eosinophil":
			NotEosinophil.SetActive (true);
			break;
		case "Basophil":
			NotBasophil.SetActive (true);
			break;
		case "Lymphocyte":
			NotLymphocyte.SetActive (true);
			break;
		case "Monocyte":
			NotMonocyte.SetActive (true);
			break;
		default:print("String not found");
			break;
		}

//		tryagain.GetComponentInChildren<Text> ().text = string.Format("This is not {0}\nTry again.",str);
//		tryagain.GetComponentInChildren<Text> ().text = string.Format("This is not {0}\nTry again.",LanguageManager.Instance.GetTextValue (str));

//		tryagain.GetComponentInChildren<Text> ().text = string.Format("{0} {1}\n{2}",LanguageManager.Instance.GetTextValue("notThis"),LanguageManager.Instance.GetTextValue (str),LanguageManager.Instance.GetTextValue("Tryagain"));
		Invoke("showHint",GlobalAudioSrc.Instance.audioSrc.clip.length+.4f);
//		Invoke ("showHint", 3);
	}

	public void hideTryAgain(){
		tryagain.SetActive (false);
	}

	public void Level1_complete(){
//		SceneManager.LoadScene ("MainMenu");
		LoadingScene.LoadingSceneIndex = 0;
	}

}
