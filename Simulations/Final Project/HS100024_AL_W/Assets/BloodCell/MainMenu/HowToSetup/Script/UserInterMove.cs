using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;// Required when using Event data.

public class UserInterMove : MonoBehaviour   {

  	internal bool IsInsideCollider;

    // Use this for initialization

    public Transform button;
    public Vector3 setPosition;
    Vector3 storePosition;
    UseInterface player;
    public bool check;

     void Start () {
        player = GameObject.FindObjectOfType(typeof(UseInterface)) as UseInterface;
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
    public void OnGazeDown()
    {
        if (check)
        {
            if (player != null)
                player.other = gameObject;
                button.localPosition = setPosition;
        }
    }
	public void OnGazeDrag() {
	}
    public void OnGazeUp()
    {
        if (check)
        {
            player.other = null;

            button.localPosition = storePosition;
        }
     }
    /// Called when the viewer's trigger is used, between OnGazeEnter and OnGazeExit.
    public void OnGazeTrigger() {
		Debug.Log ("OnGazeTrigger");
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (player.zoom.name == collider.name)
        {
            player.zoomF = false;
            player.zoomBK = true;
            player.zoom.SetActive(false);
            player.zoomB.SetActive(true);
        }
        if (player.zoomB.name == collider.name)
        {
            player.callOnRotate();
            player.zoomF = false;
            player.zoomBK = false;
            player.zoomB.SetActive(false);
            player.Zoom = false;
        }
    }
	#endregion
}
