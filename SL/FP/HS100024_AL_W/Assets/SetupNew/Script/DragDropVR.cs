using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;// Required when using Event data.

public class DragDropVR: MonoBehaviour    {

	public GameObject ParentObject;
	public bool CanRotate , CanTranslate = false;

	bool CanMove = true;

	float MoveSpeed = 10f;	
	float RotateSpeed = 8f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	// Use to zoom in and zoom out this particular object using joystick
		if(CanTranslate)
			transform.Translate (0, 0,- Input.GetAxis ("Vertical") *MoveSpeed);

	// Use to rotate this particular object using joystick
		if(CanRotate)
			transform.Rotate ( -Input.GetAxis ("Horizontal") * RotateSpeed , 0 , 
			0, Space.World);
		
	}

	#region IGvrGazeResponder implementation

	/// Called when the user is looking on a GameObject with this script,
	/// as long as it is set to an appropriate layer (see GvrGaze).
	public void OnGazeEnter() {
		Debug.Log ("OnGazeEnter");
	}

	/// Called when the user stops looking on the GameObject, after OnGazeEnter
	/// was already called.
	public void OnGazeExit() {
		Debug.Log ("OnGazeExit");
	}

	/// Called when the user stops looking on the GameObject, after OnGazeEnter
	/// was already called.
	public void OnGazeDown() {
		CanMove = false;

		if(transform.parent != Camera.main.transform)
			transform.parent = Camera.main.transform;

	}

	public void OnGazeDrag() {
	}

	public void OnGazeUp() {
		
		CanMove = true;
		if (ParentObject != null)
			transform.parent = ParentObject.transform;
		else
			transform.parent = null;
	}


	/// Called when the viewer's trigger is used, between OnGazeEnter and OnGazeExit.
	public void OnGazeTrigger() {
		Debug.Log ("OnGazeTrigger");
	}

	#endregion
}
