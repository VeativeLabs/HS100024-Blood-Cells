
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pool : MonoBehaviour {

	public GameObject pool_Object;
	public int TotalObject=20;
	public List<GameObject>	pool_Objects;
	public float count=0,delay=.5f;

	// Use this for initialization
	void Start () {
		pool_Objects = new List<GameObject> ();
		GameObject Temp = Instantiate (pool_Object, this.transform.position, Quaternion.identity) as GameObject;
		for(int i=1;i<TotalObject;i++){
			Temp = Instantiate (Temp, this.transform.position, Quaternion.identity) as GameObject;
			Temp.SetActive (false);
			pool_Objects.Add (Temp);
		}
		StartCoroutine (moveObj());
	}

//	void OnEnable(){
//		
//	}



	IEnumerator moveObj(){
		yield return new WaitForSecondsRealtime(delay);
		GameObject temp=getObject ();
		if(temp!=null){
			temp.transform.position = new Vector3 (Random.Range(-.05f, .05f) + this.transform.position.x, Random.Range(-.05f, .05f) + this.transform.position.y, this.transform.position.z);
			temp.SetActive (true);		
			if(Application.loadedLevelName.Equals("Blood_Cell")){
				temp.GetComponent<ConstantTranslate> ().startTranslatation ();
				temp.GetComponent<Level1Controller> ().enabled = true;
			}
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

	public void reset(){
		for(int i=0;i<pool_Objects.Count;i++){
			pool_Objects[i].GetComponent<ConstantTranslate> ().startTranslatation ();
			pool_Objects[i].GetComponent<ConstantTranslate> ().isPlayerStill = false;
			pool_Objects[i].GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezePositionZ;
		}
	}
}
