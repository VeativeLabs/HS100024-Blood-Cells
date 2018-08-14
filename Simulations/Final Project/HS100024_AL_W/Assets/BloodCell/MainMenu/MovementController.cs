using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class MovementController : MonoBehaviour {
	// how to fast move
	public float speed = 3.0F;
    // how to fast rotation

    public float rotateSpeed = 2.0F;

	// should i move forward or no
	public bool moveForward;
	//chanrctercontroller script
	private CharacterController controllor;
	// GvrViewer Script
//	private GvrViewer gurViewer;
    // VR Head
    public static float curSpeed;

    private Transform vrHead;

    public GameObject text;
	void Start(){

		controllor = GetComponent<CharacterController>();
//		gurViewer = transform.GetChild (0).GetComponent<GvrViewer> ();
		vrHead = Camera.main.transform;
 	}
	void Update() {
        //if (Input.GetAxis("Vertical") != 0)
        //{
        //    text.SetActive(false);
        //}
        //else
        //{
        //    text.SetActive(true);


        //}
        // Rotate Object
        transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);
		Vector3 forward = vrHead.TransformDirection(Vector3.forward);
		 curSpeed = speed * Input.GetAxis("Vertical");

		controllor.SimpleMove(forward * curSpeed);

 


	}
}