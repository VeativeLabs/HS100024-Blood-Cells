using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using VeativeDatabase;
using ArabicSupport;
using SmartLocalization;

public class Level2Controller : MonoBehaviour {
	public static Level2Controller instance;

	public GameObject _canvas;
	public GameObject WellDONE;
	public GameObject C_Inst_RightOption;
	public GameObject Loadbutton;
	public Text scoreText,_Final_Score_Text;
	public GameObject C_Inst_Assesment_Complete;
	public GameObject Activity0_Start_Dialog;
	public GameObject Activity1_Start_Dialog;
	public GameObject Activity2_Start_Dialog;
	public GameObject Activity3_Start_Dialog;
	public GameObject Activity4_Start_Dialog;
	public GameObject Activity5_Start_Dialog;
	public GameObject Activity6_Start_Dialog;

	public GameObject EndOfLevel2_Dialog;
	public GameObject WelldoneEnd;
	public GameObject TryAgainEnd;
	public GameObject LOCanvas;

	public GameObject Activity0_End_Dialog;
	public GameObject Activity1_End_Dialog;
	public GameObject Activity2_End_Dialog;
	public GameObject Activity3_End_Dialog;
	public GameObject Activity4_End_Dialog;
	public GameObject Activity5_End_Dialog;
	public GameObject Activity6_End_Dialog;


	public GameObject[] BulletList_Hint;

	public GameObject infectedTunnal;

	public GameObject energyBar;
	public GameObject meshSwellig;
	private bool swelling = false;
	int Temp_cell;

	// Use this for initialization
	void Awake(){
		instance = this;	
	}

	void Start () {
		Resources.UnloadUnusedAssets ();
		FixVeriable.PartNumber = -1;
		FixVeriable.collectNumber = -1;
		FixVeriable.isPlayerStill = true;
		FixVeriable.isActivityStart = false;
		swelling = false;
		PlayerPrefs.SetInt ("scor", 0);

		if (LanguageHandler.instance.IsRightToLeft) {
			scoreText.text = PlayerPrefs.GetInt("scor")+" " + ArabicFixer.Fix("" + LanguageManager.Instance.GetTextValue("Score"),false,false);
		} else if (LanguageHandler.instance.Languages[LanguageHandler.instance.CurrentLanguageIndex].LanguageID== "zh-CN") {
			scoreText.text = "得分 : " + PlayerPrefs.GetInt ("scor");
		} else {
			scoreText.text = "Score : " + PlayerPrefs.GetInt ("scor");
		}
		PlayerPrefs.SetInt ("Attempt", 0);
	}
	
	// Update is called once per frame
	void Update () {
		if (swelling) {
			float temp = meshSwellig.GetComponent<SkinnedMeshRenderer> ().GetBlendShapeWeight (0);
			temp = Mathf.Lerp (temp, 25, 5);
			meshSwellig.GetComponent<SkinnedMeshRenderer> ().SetBlendShapeWeight (0, temp);
		} else {
			float temp = meshSwellig.GetComponent<SkinnedMeshRenderer> ().GetBlendShapeWeight (0);
			temp = Mathf.Lerp (temp, 0, 5);
			meshSwellig.GetComponent<SkinnedMeshRenderer> ().SetBlendShapeWeight (0, temp);
		}
	}

	public void startpart1(){
		FixVeriable.PartNumber = FixVeriable.discription;
		FixVeriable.isPlayerStill = false;
	}


	//Wwlldone Voice Waiting Time ********************

	void _Welldone(){
		Loadbutton.SetActive (true);
		hideOnLoad_hint ();
		WellDONE.SetActive (false);
	}

	public void selectCell(int cell){
		
		if(!Loadbutton.activeInHierarchy){
			if (cell == FixVeriable.collectNumber) {
				WellDONE.SetActive (true);
				Invoke ("_Welldone", GlobalAudioSrc.Instance.audioSrc.clip.length + .4f);
//				Loadbutton.SetActive (true);
				HighLight.instance.colliderOff();
				FixVeriable.selectedbloodcell = cell;
//				hideOnLoad_hint ();
//				HighLight.instance.rightAns (cell);
				if(PlayerPrefs.GetInt ("Attempt")==0){
					PlayerPrefs.SetInt ("scor", PlayerPrefs.GetInt ("scor") + 5);
				}
				if(PlayerPrefs.GetInt ("Attempt")==1){
					PlayerPrefs.SetInt ("scor", PlayerPrefs.GetInt ("scor") + 3);
				}
				if(PlayerPrefs.GetInt ("Attempt")==2){
					PlayerPrefs.SetInt ("scor", PlayerPrefs.GetInt ("scor") + 2);
				}
				HighLight.instance.highLightstop (FixVeriable.collectNumber);
				DatabaseManager.dbm.AddEntry("L2","Q"+(cell+1),(PlayerPrefs.GetInt ("Attempt")+1)<3?(PlayerPrefs.GetInt ("Attempt")+1):3,(PlayerPrefs.GetInt ("Attempt") == 0 ? 5 : (PlayerPrefs.GetInt ("Attempt") == 1 ? 3 : (PlayerPrefs.GetInt ("Attempt") == 2) ? 2 : 0)),PlayerPrefs.GetInt ("Attempt")<3,PlayerPrefs.GetInt ("Attempt")>2);
				PlayerPrefs.SetInt ("Attempt", 0);
			} else {
				Loadbutton.SetActive (false);
				//show message choose other cell for shoot
//				showTryagain();
				FixVeriable.selectedbloodcell = -1;
				PlayerPrefs.SetInt ("Attempt",PlayerPrefs.GetInt ("Attempt")+1);
				HighLight.instance.wrongAns (cell);
				if (PlayerPrefs.GetInt ("Attempt") > 2) {
//					HighLight.instance.highLight (FixVeriable.collectNumber);
					HighLight.instance.colliderOff();
					for(int i=0;i<BulletList_Hint.Length;i++) {
						BulletList_Hint[i].SetActive (false);
					}
					BulletList_Hint[2].SetActive (true);
					Temp_cell = FixVeriable.collectNumber;
					Invoke ("_correctResponse", GlobalAudioSrc.Instance.audioSrc.clip.length + .5f);

				} else {
					showTryagain();
					_canvas.GetComponent<GvrPointerGraphicRaycaster> ().enabled = false;
				}
			}
		}
	}

	void _correctResponse(){
		BulletList_Hint[2].SetActive (false);
		C_Inst_RightOption.SetActive (true);
		HighLight.instance.highLight (FixVeriable.collectNumber);
		Invoke ("_CorrectResponseDone", GlobalAudioSrc.Instance.audioSrc.clip.length + .5f);
	}
	void _CorrectResponseDone(){
		Loadbutton.SetActive (true);
		HighLight.instance.colliderOff();
		FixVeriable.selectedbloodcell = FixVeriable.collectNumber;
			hideOnLoad_hint ();
		if(PlayerPrefs.GetInt ("Attempt")==0){
			PlayerPrefs.SetInt ("scor", PlayerPrefs.GetInt ("scor") + 5);
		}
		if(PlayerPrefs.GetInt ("Attempt")==1){
			PlayerPrefs.SetInt ("scor", PlayerPrefs.GetInt ("scor") + 3);
		}
		if(PlayerPrefs.GetInt ("Attempt")==2){
			PlayerPrefs.SetInt ("scor", PlayerPrefs.GetInt ("scor") + 2);
		}
		HighLight.instance.highLightstop (FixVeriable.collectNumber);
		DatabaseManager.dbm.AddEntry("L2","Q"+(Temp_cell+1),(PlayerPrefs.GetInt ("Attempt")+1)<3?(PlayerPrefs.GetInt ("Attempt")+1):3,(PlayerPrefs.GetInt ("Attempt") == 0 ? 5 : (PlayerPrefs.GetInt ("Attempt") == 1 ? 3 : (PlayerPrefs.GetInt ("Attempt") == 2) ? 2 : 0)),PlayerPrefs.GetInt ("Attempt")<3,PlayerPrefs.GetInt ("Attempt")>2);
		PlayerPrefs.SetInt ("Attempt", 0);
		C_Inst_RightOption.SetActive (false);
	}



	public void setScore(Text text){
		if (LanguageHandler.instance.IsRightToLeft) {
			text.text = PlayerPrefs.GetInt("scor")+" " + ArabicFixer.Fix("" + LanguageManager.Instance.GetTextValue("Score"),false,false);
		} else if (LanguageHandler.instance.Languages[LanguageHandler.instance.CurrentLanguageIndex].LanguageID == "zh-CN") {
			text.text = "得分 : " + PlayerPrefs.GetInt ("scor");
		} else {
			text.text = "Score : " + PlayerPrefs.GetInt ("scor");
		}

	}

	public void startActivity0(){
		FixVeriable.collectNumber = 0;
		Activity0_Start_Dialog.SetActive (true);
	}
	public void startActivity1(){
		FixVeriable.collectNumber = 1;
		Activity1_Start_Dialog.SetActive (true);
	}
	public void startActivity2(){
		FixVeriable.collectNumber = 2;
		Activity2_Start_Dialog.SetActive (true);
	}
	public void startActivity3(){
		FixVeriable.collectNumber = 3;
		Activity3_Start_Dialog.SetActive (true);
	}
	public void startActivity4(){
		FixVeriable.collectNumber = 4;
		Activity4_Start_Dialog.SetActive (true);
	}
	public void startActivity5(){
		FixVeriable.collectNumber = 5;
		Activity5_Start_Dialog.SetActive (true);
	}
	public void startActivity6(){
		FixVeriable.collectNumber = 6;
		Activity6_Start_Dialog.SetActive (true);
	}

	public void showTryagain(){
		for(int i=0;i<BulletList_Hint.Length;i++) {
			BulletList_Hint[i].SetActive (false);
		}
		BulletList_Hint[2].SetActive (true);

//		Invoke ("hideTryagain", 1);
		Invoke("hideTryagain",GlobalAudioSrc.Instance.audioSrc.clip.length +.4f);
	}

	public void hideTryagain(){
		for(int i=0;i<BulletList_Hint.Length;i++) {
			BulletList_Hint[i].SetActive (false);
		}
		if(!BulletList_Hint[3].activeInHierarchy){
			BulletList_Hint[1].SetActive (true);
		}
		_canvas.GetComponent<GvrPointerGraphicRaycaster> ().enabled = true;
	}

	public void hideOnLoad_hint(){
		CancelInvoke ();
		for(int i=0;i<BulletList_Hint.Length;i++) {
			BulletList_Hint[i].SetActive (false);
		}
		BulletList_Hint[3].SetActive (true);
	}

	public void morebullets_hint(){
		for(int i=0;i<BulletList_Hint.Length;i++) {
			BulletList_Hint[i].SetActive (false);
		}
		BulletList_Hint[5].SetActive (true);
	}
		
	public void moreBullets_hint_hide(){
		for(int i=0;i<BulletList_Hint.Length;i++) {
			BulletList_Hint[i].SetActive (false);
		}
	}

	public void Trigger_hint(){
		for(int i=0;i<BulletList_Hint.Length;i++) {
			BulletList_Hint[i].SetActive (false);
		}
		BulletList_Hint[4].SetActive (true);
	}

	public void completeActivity(){
		for(int i=0;i<BulletList_Hint.Length;i++) {
			BulletList_Hint[i].SetActive (false);
		}
		switch (FixVeriable.collectNumber) {
		case 0:
			infectedTunnal.GetComponent<Genrate_Tunnal> ().EndActivity0 ();
			Activity0_End_Dialog.SetActive (true);
			break;
		case 1:
//			Genratebacteria.instance.destroyAllObject ();
			infectedTunnal.GetComponent<Genrate_Tunnal> ().EndActivity0 ();
			Activity1_End_Dialog.SetActive (true);
			break;
		case 2:
//			Genratebacteria.instance.destroyAllObject ();
			infectedTunnal.GetComponent<Genrate_Tunnal> ().EndActivity0 ();
			Activity2_End_Dialog.SetActive (true);
			break;
		case 3:
//			Genratebacteria.instance.destroyAllObject ();
			infectedTunnal.GetComponent<Genrate_Tunnal> ().EndActivity0 ();
			Activity3_End_Dialog.SetActive (true);
			break;
		case 4:
//			Genratebacteria.instance.destroyAllObject ();
			infectedTunnal.GetComponent<Genrate_Tunnal> ().EndActivity0 ();
			Activity4_End_Dialog.SetActive (true);
			swelling = true;
			break;
		case 5:
//			Genratebacteria.instance.destroyAllObject ();
			infectedTunnal.GetComponent<Genrate_Tunnal> ().EndActivity0 ();
			Activity5_End_Dialog.SetActive (true);
			break;
		case 6:
//			Genratebacteria.instance.destroyAllObject ();
			infectedTunnal.GetComponent<Genrate_Tunnal> ().EndActivity0 ();

			break;
		default:
			break;
		}

	}

	public void MovePlayer(){
		FixVeriable.isPlayerStill = false;
		swelling = false;
	}

	public void Restart(){
		Application.LoadLevel (Application.loadedLevel);
	}

	public void Gotolevel1(){
		Application.LoadLevel ("Blood_Cell");
	}

	public void hideBar(){
		energyBar.SetActive (false);
	}
}
