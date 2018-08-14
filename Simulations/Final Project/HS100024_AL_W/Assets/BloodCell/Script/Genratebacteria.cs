using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class Genratebacteria : MonoBehaviour {
	public static Genratebacteria instance;
	public List<GameObject> pool_Object;
	public int TotalObject=20;
	public List<GameObject>	pool_Objects;
	public float count=0,delay=.01f;
	public bool isStart=false;
	public GameObject Tunnal;
	public GameObject initial, final;
	void Awake(){
		instance=this;
	}
	// Use this for initialization
	void Start () {
		pool_Objects = new List<GameObject> ();
	}

	public void makeBacteria(int bacteria){
		print("makeBacteria");
		for(int i=0;i<TotalObject;i++){
			if (bacteria == 3) { 
				GameObject Temp = Instantiate (pool_Object[bacteria], this.transform.position, Quaternion.identity) as GameObject;
				Temp.SetActive (false);
				pool_Objects.Add (Temp);
				i++;
				Temp = Instantiate (pool_Object[0], this.transform.position, Quaternion.identity) as GameObject;
				Temp.SetActive (false);
				pool_Objects.Add (Temp);
			} else {
				GameObject Temp = Instantiate (pool_Object[bacteria], this.transform.position, Quaternion.identity) as GameObject;
				Temp.SetActive (false);
				pool_Objects.Add (Temp);
			}

		}
		this.transform.DOKill (true);
		this.transform.localPosition = initial.transform.localPosition;
		this.transform.DOLocalMove (final.transform.localPosition,40);

		StartCoroutine (moveObj());
	}

	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator moveObj(){
		yield return new WaitForSecondsRealtime(delay);
		GameObject temp = getObject ();
		if (temp != null) {
			temp.transform.position = this.transform.position;
			temp.SetActive (true);	
			float angle = Random.Range (0f, 360f)*Mathf.PI*2;
			temp.transform.DOLocalMove(new Vector3 ((Mathf.Cos(angle)*.07f)+this.transform.localPosition.x,(Mathf.Sin(angle)*.07f)+this.transform.localPosition.y, this.transform.localPosition.z),0f);
			temp.transform.parent = Tunnal.transform;
			temp.isStatic = true;
		}
		StartCoroutine (moveObj());
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
		StopAllCoroutines ();
		foreach (GameObject obj in pool_Objects) {
			if(obj!=null)
			Destroy (obj,5f);
		}
		pool_Objects.Clear ();

	}

	public void destroyAllObject(int Bullets){
		if(pool_Objects.Count!=0){
			StopAllCoroutines ();
			pool_Objects[(Bullets-1)*(TotalObject/shootingCell.instance.TotalObject)].GetComponent<doChildrenOn> ().childActive ();
			pool_Objects[(Bullets-1)*(TotalObject/shootingCell.instance.TotalObject)].GetComponentInChildren<Animation> ().enabled = true;
			pool_Objects [(Bullets-1)*(TotalObject/shootingCell.instance.TotalObject)].GetComponentInChildren<Animation> ().Play ();

			for(int i=((Bullets-1)*(TotalObject/shootingCell.instance.TotalObject));i<((Bullets-1)*(TotalObject/shootingCell.instance.TotalObject))+(TotalObject/shootingCell.instance.TotalObject);i++) {
				Destroy (pool_Objects[i],8f);
			}
		}
	}

}
