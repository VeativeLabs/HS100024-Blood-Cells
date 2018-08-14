using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ControllerBtns : MonoBehaviour
{
	public static ControllerBtns instance;

	void Awake ()
	{
		if (instance != null && instance != this) {
			Destroy (instance);
			instance = null;
		} 

		if (instance == null) {
			instance = this;
			DontDestroyOnLoad (gameObject);	
		}
	}

	void OnEnable ()
	{
		SceneManager.sceneLoaded += OnSceneLoad;
	}

	void OnSceneLoad (Scene scene, LoadSceneMode load)
	{
		if (SceneManager.GetActiveScene ().name == "SetupNew") {
			if (GameObject.FindObjectOfType<ButtonsAction> () != null)
				GameObject.FindObjectOfType<ButtonsAction> ().enabled = false;
		}
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Menu)) {
			if (SceneManager.GetActiveScene ().name != "MainMenu") {
				if (SceneManager.GetActiveScene ().name != "SetupNew") {
					SceneManager.LoadScene ("MainMenu");
				}
			}
		}

		if (Input.GetKeyDown (KeyCode.JoystickButton1)) {
			if (SceneManager.GetActiveScene ().name == "MainMenu")
				Application.Quit ();
			else if (SceneManager.GetActiveScene ().name == "SetupNew")
				SceneManager.LoadScene ("MainMenu");
		}
	}
}
