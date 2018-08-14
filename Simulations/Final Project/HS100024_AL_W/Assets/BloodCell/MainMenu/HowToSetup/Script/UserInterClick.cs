using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;// Required when using Event data.

public class UserInterClick : MonoBehaviour    {

  	internal bool IsInsideCollider;

    // Use this for initialization

    public Transform button;
    public Vector3 setPosition;
    Vector3 storePosition;
    public bool checkClick;
    public int checkCouter;

    public GameObject playerm;
     void Start () {
        storePosition = button.localPosition;
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
        if (checkClick)
        {
            checkCouter = checkCouter + 1;
            if (checkCouter == 5)
            {
                button.localPosition = storePosition;
                checkCouter = 0;

                playerm.GetComponent<UseInterface>().callOnZoom();
                gameObject.SetActive(false);
            }
            gameObject.GetComponent<Renderer>().material.color = Color.red;
            button.localPosition = setPosition;
        }
    }
	public void OnGazeDrag() {
	}
    public void OnGazeUp()
    {
        if (checkClick)
        {
            button.localPosition = storePosition;
            gameObject.GetComponent<Renderer>().material.color = Color.green;
        }
    }
    /// Called when the viewer's trigger is used, between OnGazeEnter and OnGazeExit.
    public void OnGazeTrigger() {
		Debug.Log ("OnGazeTrigger");
	}
	#endregion
}
