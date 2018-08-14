using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AnAnimation : MonoBehaviour {

    public GameObject drag,other, dragA;
    Transform storeP;
    public Transform butto;
    public Text showText;
    Vector3 store;

    public Transform joystic;
    void Start()
    { 
        store = butto.transform.localPosition;
    }
      public void CallOnDrag () {
        storeP = drag.transform.parent;
//        other.transform.GetComponent<GvrHead>().target = Camera.main.transform;
//        other.transform.GetComponent<GvrHead>().trackRotation = false;
        drag.transform.parent = Camera.main.transform;
        butto.localPosition = new Vector3(0.0001211286f, -0.007745712f, 0.037f); 
    }
    public void CallOnDrop()
    {
//        other.transform.GetComponent<GvrHead>().target = null;
        drag.transform.parent= storeP;
        drag.transform.GetComponent<UserInterDrag>().enabled = true;
        drag.transform.GetComponent<Collider>().enabled = true;
        drag.transform.GetChild(0).gameObject.SetActive(false);
        butto.localPosition = store;
//        other.transform.GetComponent<GvrHead>().trackRotation = true;
		showText.text = "Now pick the object and move your head.";
        joystic.localEulerAngles = new Vector3(0, 0, 0);
        StartCoroutine(CallOnafteranimation());
    }
    public void CallOnDragA()
    {
        storeP = dragA.transform.parent;
//        other.transform.GetComponent<GvrHead>().trackRotation = false;
        dragA.transform.parent = Camera.main.transform;
        butto.localPosition = new Vector3(0.0001211286f, -0.007745712f, 0.037f);
    }
    public void CallOnDropA()
    {
//        other.transform.GetComponent<GvrHead>().target = null;
        dragA.transform.parent = storeP;
        dragA.transform.GetComponent<UserInterDrag>().enabled = true;
        dragA.transform.GetComponent<Collider>().enabled = true;
        dragA.transform.GetChild(0).gameObject.SetActive(false);
        butto.localPosition = store;
		showText.text = "Now pick the object and move your head.";
        joystic.localEulerAngles = new Vector3(0, 0, 0);
    }
    public IEnumerator CallOnafteranimation()
    {
         yield return new WaitForSeconds(20);
        drag.SetActive(false);
        dragA.SetActive(true);
        
        yield return new WaitForSeconds(1);
//        other.transform.GetComponent<GvrHead>().target = null;
//        other.transform.GetComponent<GvrHead>().trackRotation = false;
        Camera.main.transform.eulerAngles = new Vector3(0, 0, 0);
        Camera.main.transform.localPosition = new Vector3(0, 2.73f, 0);
        Camera.main.transform.GetComponent<Animation>().Play("NewAnimationother");
    }
}
