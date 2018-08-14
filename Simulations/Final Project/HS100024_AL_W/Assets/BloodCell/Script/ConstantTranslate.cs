using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ConstantTranslate : MonoBehaviour {

	public float Speed = 30f;
	public bool isPlayerStill = false;
	private float initialSpeed = 1f;
	// Use this for initialization
	void Start () {
		isPlayerStill = false;
		initialSpeed = Speed;
	}
	
	// Update is called once per frame
	void Update () {
		if (this.gameObject.CompareTag("Player")) {
			if(!FixVeriable.isPlayerStill){
//				if (Application.loadedLevelName.Equals ("Blood_Cell_level2")) {
					if (Input.GetAxis ("Vertical") > 0) {
						Speed = -.7f;
					} else {
						Speed = initialSpeed;
					}
//				}
				this.transform.Translate(Vector3.forward *Speed * Time.deltaTime,Space.World);
			}
		} else {
			if(!isPlayerStill){
				this.transform.Translate(Vector3.forward *Speed * Time.deltaTime,Space.World);
			}
		}

	}

	public void stopTranslatation(){
		
		if (!FixVeriable.isPlayerStill  && FixVeriable.PartNumber==FixVeriable.discription) {
			isPlayerStill = true;
			Camera.main.GetComponent<GvrPointerPhysicsRaycaster> ().enabled = false;
//			Camera.main.GetComponent<PhysicsRaycaster> ().enabled = false;
			this.GetComponent<Collider> ().isTrigger = true;
			FixVeriable.isPlayerStill = true;
		}
		if (!FixVeriable.isPlayerStill  && FixVeriable.PartNumber==FixVeriable.collection) {
			isPlayerStill = true;
		}
	}

	public void startTranslatation(){
		isPlayerStill = false;
		this.GetComponent<Collider> ().isTrigger = false;
	}


}
