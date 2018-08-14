using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;// Required when using Event data.

public class UserInterDrag : MonoBehaviour    {

	public delegate void OnDragCompletion(GameObject Object);
	public static event OnDragCompletion DragCompletion;
  	internal bool IsInsideCollider;
	public   bool CanMove = true;
     public Transform button;
    public Vector3 setPosition;
    Vector3 storePosition;

    Transform parent;
     // Use this for initialization
    void Start () {
        parent = transform.parent;
        storePosition = button.transform.localPosition;
        if (!gameObject.GetComponent<Rigidbody>())
        {
            gameObject.GetComponent<Collider>().isTrigger = true;
            gameObject.AddComponent<Rigidbody>();
            gameObject.transform.GetComponent<Rigidbody>().useGravity = false;
        }
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
             button.localPosition = setPosition;
            CanMove = false;
            if (transform.parent != Camera.main.transform)
                transform.parent = Camera.main.transform;
     }
	public void OnGazeDrag() {
        
    }    
    public void OnGazeUp()
    {

        transform.parent = parent;

        button.localPosition = storePosition;
            CanMove = true;          
            if (IsInsideCollider)
            {
                DragCompletion(gameObject);
            }
           
        }
 	/// Called when the viewer's trigger is used, between OnGazeEnter and OnGazeExit.
	public void OnGazeTrigger() {
		Debug.Log ("OnGazeTrigger");
	}
	#endregion
}
