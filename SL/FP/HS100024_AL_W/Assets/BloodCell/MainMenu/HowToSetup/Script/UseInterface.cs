using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class UseInterface : MonoBehaviour {

    public float speed = 6.0f;
    private Vector3 moveDirection = Vector3.zero;
    CharacterController controller;
    private Transform vrHead;
    public Transform joystic;
    public Transform rotateObject;
    public bool checkFord, checkBack,rotate, Zoom,zoomF,zoomBK; 
    public GameObject fordUser,backUser,cube, cubeDrag;
    public Transform button;
    public Vector3 setPosition,setPostionR,setRot;
    Vector3 storePosition;
    public GameObject aRotate,other,mainRemote,last;
    public Text showText;
    public Transform[] bothButton;

    public GameObject zoom, zoomB;
	private float count=0;


    void Start()
    {
        setPostionR = mainRemote.transform.localPosition;
        setRot = mainRemote.transform.localEulerAngles;
        // GoToVrMode();
        storePosition = button.localPosition;
        controller = GetComponent<CharacterController>();
        vrHead = Camera.main.transform;
          callOn();
        Invoke("GoToVrMode",2f);
          }

    public void callOn()
    {
//       vrHead.transform.GetComponent<GvrHead>().trackRotation = false;
    }

    float speedAuto = 8f;
    public void Fordword()
    {

        Vector3 forwardA = vrHead.TransformDirection(Vector3.forward);
        float curSpeedA = speedAuto;
        controller.SimpleMove(forwardA * curSpeedA);
        joystic.localEulerAngles = new Vector3(Mathf.Lerp(0, 30, 1f), 0, 0);
     }
    float speedAutoA = -8f;
    public void BackWordword()
    {
        Vector3 forwardA = vrHead.TransformDirection(Vector3.back);
        float curSpeedA = speedAuto;
        controller.SimpleMove(forwardA * curSpeedA);
        joystic.localEulerAngles = new Vector3(Mathf.Lerp(0, -30, 1f), 0, 0);
     }
    float rotateB=-2;
    public void RotateObject()
    {

         float horizontal = rotateB;         
         joystic.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(0, -30, 1f));
        aRotate.transform.Rotate(0, horizontal * 1, 0);
        clickAndRotate();
     }
    float rotateC = 2;
    public void RotateObjectL()
    {
        float horizontal = rotateC;
        joystic.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(0, 30, 1f));
        aRotate.transform.Rotate(0, horizontal * 1, 0);
        clickAndRotate();
    }
    float rotateBZ = 0.2f;
    public void ZoomFObject()
    {
        float horizontal = rotateBZ;
        joystic.localEulerAngles = new Vector3(Mathf.Lerp(0, 30, 1f), 0, 0);
        aRotate.transform.localPosition = new Vector3(aRotate.transform.localPosition.x, aRotate.transform.localPosition.y, aRotate.transform.localPosition.z + horizontal);
        clickAndRotate();
    }
    float rotateCZB = -0.2f;
    public void ZoomBObject()
    {
        float horizontal = rotateCZB;
        joystic.localEulerAngles = new Vector3(Mathf.Lerp(0, -30, 1f), 0, 0);
        aRotate.transform.localPosition = new Vector3(aRotate.transform.localPosition.x, aRotate.transform.localPosition.y, aRotate.transform.localPosition.z + horizontal);
        clickAndRotate();
    }
    public void GoToVrMode()
    {
        showText.text= "Press @ + C for VR mode.";
        for (int i=0;i< bothButton.Length;i++)
        {
            bothButton[i].localPosition = new Vector3(bothButton[i].localPosition.x, 0.0055f, bothButton[i].localPosition.z);
        }
        StartCoroutine(bothButtonUp(5f));
    }
    public IEnumerator bothButtonUp(float ttr)
    {
        yield return new WaitForSeconds(ttr);
        bothButton[0].localPosition = new Vector3(-0.002119721f, 0.006116648f, 0.008226271f);
        bothButton[1].localPosition = new Vector3(0.003307478f, 0.00583f, 0.00175f);
         showText.text = "Push joystick to move forward.";
        InvokeRepeating("Fordword", 1f, 0.001f);
    }
    void Update()
    {


        if (last.transform.GetComponent<UserInterDrag>().CanMove==false) { 
            Vector3 forward = vrHead.TransformDirection(Vector3.forward);
            float curSpeed = Input.GetAxis("Vertical");
            if (curSpeed > 0)
            {
                joystic.localEulerAngles = new Vector3(Mathf.Lerp(0, 30, 1f), 0, 0);
            }
            else if (curSpeed < 0)
            {
                joystic.localEulerAngles = new Vector3(Mathf.Lerp(0, -30, 1f), 0, 0);
            }
            else
            joystic.localEulerAngles = new Vector3(0, 0, 0);
            controller.SimpleMove(forward * curSpeed * speed);
          }

        if (checkFord)
        {
             Vector3 forward = vrHead.TransformDirection(Vector3.forward);
            float curSpeed =Input.GetAxis("Vertical");
             if(curSpeed>0)
            controller.SimpleMove(forward * curSpeed*speed);
             if (curSpeed > 0)
            {
                joystic.localEulerAngles = new Vector3(Mathf.Lerp(0, 30, 1f), 0, 0);

            }             
            else
                joystic.localEulerAngles = new Vector3(0, 0, 0);
        }
        if (checkBack)
        {
            Vector3 forward = vrHead.TransformDirection(Vector3.forward);
            float curSpeed =Input.GetAxis("Vertical");
            if (curSpeed < 0)
                controller.SimpleMove(forward * curSpeed*speed);
             if (curSpeed < 0)
            {
                joystic.localEulerAngles = new Vector3(Mathf.Lerp(0, -30, 1f), 0, 0);
            }
            else
                joystic.localEulerAngles = new Vector3(0, 0, 0);
        }
        if (rotate)
        {
            if (other != null)
            {
                float horizontal = Input.GetAxis("Horizontal");
                if (horizontal > 0)
                {
                    joystic.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(0, -30, 1f));
                    other.transform.Rotate(0, -horizontal * 5, 0);
                }
                else if (horizontal < 0)
                {
                    joystic.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(0, 30, 1f));
                    other.transform.Rotate(0, -horizontal * 5, 0);
                }
                else
                {
                    joystic.localEulerAngles = new Vector3(0, 0, 0);
                }
            }
            else
            {
                joystic.localEulerAngles = new Vector3(0, 0, 0);
            }
        }
        if (Zoom)
		{ 
            if (other != null)
            {
                float horizontal = Input.GetAxis("Vertical");
                if (horizontal > 0)
                {
                    if (zoomF)
                    {
                        joystic.localEulerAngles = new Vector3(Mathf.Lerp(0, -30, 1f), 0, 0);
                        other.transform.localPosition = new Vector3(other.transform.localPosition.x, other.transform.localPosition.y, other.transform.localPosition.z + horizontal);

                    }
                }
                else if (horizontal < 0)
                {
                    if (zoomBK) {

                    joystic.localEulerAngles = new Vector3(Mathf.Lerp(0, 30, 1f), 0, 0);
                        other.transform.localPosition = new Vector3(other.transform.localPosition.x, other.transform.localPosition.y, other.transform.localPosition.z + horizontal);
                }
            }
            else
            {
                joystic.localEulerAngles = new Vector3(0, 0, 0);
            }
            }
            else
            {
                joystic.localEulerAngles = new Vector3(0, 0, 0);
            }
        }

//to exit from how to play
		if (Input.GetButton ("Fire1")) {
			count += Time.deltaTime;
			if (count >4) {
				Application.LoadLevel ("MainScene_New");
			}
			if ((Input.GetAxis ("Horizontal") != 0 || Input.GetAxis ("Vertical") != 0)) {
				count = 0;
			}
		}
		if (Input.GetButtonUp("Fire1")) {
			count = 0;
		}
    }
    public void OnTriggerEnter(Collider collider)
    {
         if (collider.name == "Fowd")
        {
            showText.text = "Pull joystick to move backward.";
            CancelInvoke();
            joystic.localEulerAngles = new Vector3(0, 0, 0);
            InvokeRepeating("BackWordword", 2f, 0.001f);
            collider.gameObject.SetActive(false);
         } 
        if (collider.name == "Back")
        {
            CancelInvoke();
            joystic.localEulerAngles = new Vector3(0, 0, 0);
             collider.gameObject.SetActive(false);
            fordUser.gameObject.SetActive(true);
            checkFord = true;
            showText.text = "Now you try to move forward. ";
        }
        if (fordUser.name== collider.name)
        {
            checkFord = false;
            checkBack = true;
            backUser.gameObject.SetActive(true);
            joystic.localEulerAngles = new Vector3(0, 0, 0);
            showText.text = "Now you try to move backward. ";
        }
        if (backUser.name == collider.name)
        {
            joystic.localEulerAngles = new Vector3(0, 0, 0);
            checkBack = false;
            backUser.gameObject.SetActive(false);
             CancelInvoke();
            cube.SetActive(true);
            fordUser.gameObject.SetActive(false);
            StopAllCoroutines();
            showText.text = "Press trigger to click \non an object.";
            StartCoroutine(HoldTiming(2f));
         }
        if (zoom.name == collider.name)
        {
            zoomF = false;
            zoomBK = true;
            zoom.SetActive(false);
            zoomB.SetActive(true);
        }
        if (zoomB.name == collider.name)
        {
             callOnRotate();
            zoomF = false;
            zoomBK = false;
            zoomB.SetActive(false);
            Zoom = false;
        }
 
    }
    public IEnumerator HoldTiming(float timee)
    {
        yield return new WaitForSeconds(1);
        mainRemote.transform.localEulerAngles = new Vector3(-45f,-100f,20);
        yield return new WaitForSeconds(timee);
        InvokeRepeating("clickOb", 0.1f, 1f);
    }
    public void clickOb()
    {
        cube.GetComponent<Renderer>().material.color = Color.red;
        button.localPosition = setPosition;
        StartCoroutine(clickObUP());
    }
    public IEnumerator clickObUP()
    {
        yield return new WaitForSeconds(0.5f);
        button.localPosition = storePosition;
        cube.GetComponent<Renderer>().material.color = Color.green;
        StartCoroutine(Cancle());
    }
    public IEnumerator Cancle()
    {
        yield return new WaitForSeconds(5);
        StopAllCoroutines();
        CancelInvoke();
        cube.GetComponent<Renderer>().material.color = Color.green;
        cube.transform.GetComponent<UserInterClick>().checkClick=true;
        button.localPosition = storePosition;
        showText.text = "Why don't you try and click \non an object.";
     }
    public void clickAndRotate()
    {
         button.localPosition = setPosition;
     }
    public void clickAndRotateEnd()
    {
        button.localPosition = storePosition;
    }
    public void callOnZoom()
    {
         mainRemote.transform.localPosition = setPostionR;
        mainRemote.transform.localEulerAngles = setRot;
        button.localPosition = setPosition;
        showText.text = "Let's get conversant how to pick up an object \nand rotate it.";
        aRotate.SetActive(true);
        InvokeRepeating("ZoomFObject", 1f, 0.01f);
        StartCoroutine(AllZoom(2));
    }
    IEnumerator AllZoom(float tt)
    {
        yield return new WaitForSeconds(tt);
        CancelInvoke();
        clickAndRotateEnd();
        InvokeRepeating("ZoomBObject", 1f, 0.01f);
        joystic.localEulerAngles = new Vector3(0, 0, 0);
        StartCoroutine(AllZoomCalcle(2f));
    }
    IEnumerator AllZoomCalcle(float ttA)
    {
        yield return new WaitForSeconds(ttA);
        StopAllCoroutines();
        CancelInvoke();
        joystic.localEulerAngles = new Vector3(0, 0, 0);
        Zoom = true;
		zoomF = true;
        zoom.SetActive(true);
        button.localPosition = storePosition;
        other.transform.GetComponent<UserInterMove>().check = true;
     }
    public void callOnRotate()
    {
         Zoom = false;
        mainRemote.transform.localEulerAngles = new Vector3(-45f, -100f, 20);
        button.localPosition = setPosition;
		showText.text = "Let's get conversant how to pick up an object \nand rotate it.";
        aRotate.SetActive(true);
        InvokeRepeating("RotateObject", 1f, 0.01f);
        StartCoroutine(AllCancle(5));
    }
    IEnumerator AllCancle( float tt)
    {
        yield return new WaitForSeconds(tt);
        CancelInvoke();
        clickAndRotateEnd();
        InvokeRepeating("RotateObjectL", 1f, 0.01f);
        joystic.localEulerAngles = new Vector3(0, 0, 0);
        StartCoroutine(AllCancleR(5f));
    }
    IEnumerator AllCancleR(float ttA)
    {
        Zoom = false;
        yield return new WaitForSeconds(ttA);
        StopAllCoroutines();
        CancelInvoke();
        joystic.localEulerAngles = new Vector3(0, 0, 0);
        rotate = true;
        button.localPosition = storePosition;
		aRotate.transform.GetComponent<UserInterMove>().check = true;
        showText.text = "Try to pick and rotate an\nobject.";
        StartCoroutine(DrageAndDrop(10f));
     }

    IEnumerator DrageAndDrop(float ttA)
    {
        yield return new WaitForSeconds(ttA);
       // showText.text = "Now how to pick Object and Rotate Object S";
        yield return new WaitForSeconds(1);
        button.localPosition = storePosition;
        aRotate.transform.GetComponent<UserInterMove>().check = false;
        aRotate.SetActive(false);
        cubeDrag.SetActive(true);
        rotate = false;
        joystic.localEulerAngles = new Vector3(0, 0, 0);
        showText.text = "Learn how to drag an object.";
        yield return new WaitForSeconds(1);
        Camera.main.transform.GetComponent<Animation>().Play("DragAnimation");
    }
    }
