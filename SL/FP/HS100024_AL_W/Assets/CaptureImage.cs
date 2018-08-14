using System.IO;
using UnityEngine;
using System.Collections;

public class CaptureImage : MonoBehaviour{
	
	void Update (){
		if (Input.GetKey (KeyCode.S)) {
			ScreenShot ();
		}
	}

	public void ScreenShot (){
		Application.CaptureScreenshot (Application.dataPath + "/RenderImage.jpg");
	}
}