using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using VeativeDatabase;
public class shootingCell : MonoBehaviour {
	public static shootingCell instance;
	public List<GameObject> pool_Object;
	public int TotalObject=20;
	public List<GameObject>	pool_Objects;
	private float count=0,Delay=4;
	public int bulletcount = 0;

	public GameObject platelates;
	void Awake(){
		instance = this;
		bulletcount = 0;
	}
	// Use this for initialization
	void Start () {
		pool_Objects = new List<GameObject> ();
	}

	public void makeObj(){
		pool_Objects.Clear ();
		FixVeriable.isActivityStart = true;
		if (FixVeriable.selectedbloodcell >= 0) {
			for (int i = 0; i < TotalObject; i++) {
				GameObject Temp = Instantiate (pool_Object [FixVeriable.selectedbloodcell], this.transform.position, Quaternion.identity) as GameObject;
				Temp.SetActive (false);
				pool_Objects.Add (Temp);
			}
		}

		if (FixVeriable.collectNumber == 5) {
			Level2Controller.instance.energyBar.SetActive (true);
		}
	}
		
	// Update is called once per frame
	void Update () {
		if (FixVeriable.collectNumber >=0 && FixVeriable.isPlayerStill && FixVeriable.isActivityStart) {
//			if (Input.GetButtonDown ("Fire1")) {
			if((Input.GetButtonDown("Fire1") || GvrControllerInput.ClickButton || GvrControllerInput.ClickButtonDown || (Input.anyKeyDown && Input.inputString.Equals(System.Environment.NewLine)))&&!BackMenu.instance.IsBackMenuEnabled)
			{
				FireCell ();
				print ("After OK  Press");
				Level2Controller.instance.moreBullets_hint_hide ();
				count = 0;
				if (bulletcount >= pool_Objects.Count) {
					Level2Controller.instance.completeActivity ();
					bulletcount = 0;
					if (FixVeriable.collectNumber == 6) {
						int _percentage = ((PlayerPrefs.GetInt ("scor") * 100) / 35);
						if (LanguageHandler.instance.IsRightToLeft) {
							Level2Controller.instance._Final_Score_Text.text = "%"+ _percentage;
						} else {
							Level2Controller.instance._Final_Score_Text.text = "" + _percentage + "%";
						}
						Level2Controller.instance.C_Inst_Assesment_Complete.SetActive (true);
//						if(PlayerPrefs.GetInt ("scor")>=21){
//							Level2Controller.instance.WelldoneEnd.SetActive (true);
//						}else{
//							Level2Controller.instance.TryAgainEnd.SetActive (true);
//						}
//						DatabaseManager.dbm.InsertFinalDataToDatabase ();
//						Invoke ("showLOCanvas", 3f);
					}
				}
			}
			count += Time.deltaTime;
			if (count >= Delay && !Level2Controller.instance.BulletList_Hint[5].activeInHierarchy) {
				Level2Controller.instance.morebullets_hint ();
			}
		}
	}


	public void AfterComplete_Score(){
		if(PlayerPrefs.GetInt ("scor")>=21){
			Level2Controller.instance.WelldoneEnd.SetActive (true);
		}else{
			Level2Controller.instance.TryAgainEnd.SetActive (true);
		}
		DatabaseManager.dbm.InsertFinalDataToDatabase ();
	}

	public void showLOCanvas(){
		Level2Controller.instance.EndOfLevel2_Dialog.SetActive (false);
		Level2Controller.instance.LOCanvas.SetActive (true);
	}

	public void FireCell(){
		GameObject temp=getObject ();
		if (temp != null) {
			temp.transform.position = this.transform.position;
			temp.transform.localRotation = Random.rotation;

			temp.SetActive (true);
			if (FixVeriable.collectNumber == 6) {
				temp.GetComponent<ConstantTranslate> ().enabled = false;
				temp.transform.DOMove (platelates.transform.position, 3);
			} else {
				temp.GetComponent<Rigidbody> ().AddForce (Vector3.forward * -5 * Time.deltaTime, ForceMode.Impulse);
			}
			bulletcount += 1;
			if (bulletcount <= TotalObject) {
				Genratebacteria.instance.destroyAllObject (bulletcount);
			}
		}
	}

	public int getbulletcount(){
		return bulletcount;
	}
	public GameObject getObject(){
		for (int i = 0; i < pool_Objects.Count; i++) {
			if (!pool_Objects [i].activeInHierarchy) {
				return pool_Objects [i];
			}
		}
		return null;
	}

	public void destroyAllObject(){
		FixVeriable.isActivityStart = false;
		for(int i=0;i<pool_Objects.Count;i++) {
			Destroy (pool_Objects[i]);
		}
		pool_Objects.Clear ();

	}
}
