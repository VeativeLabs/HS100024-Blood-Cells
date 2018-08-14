using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ControllerButtonHandler : MonoBehaviour
{
	public static ControllerButtonHandler instance;

	void Awake ()
	{
		if (instance != null && instance != this) {
			Destroy (instance.gameObject);
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
		if (SceneManager.GetActiveScene ().buildIndex == 1) {//if (SceneManager.GetActiveScene ().name == "GamePlay") {
			if (GameObject.FindObjectOfType<ButtonsAction> () != null)
				GameObject.FindObjectOfType<ButtonsAction> ().enabled = false;
		}
	}

	void Update ()
	{
		if ( PlayerPrefs.GetInt ("Pause") == 0 ) {
				
			Debug.Log ( "Controller Is Working" );

			if (Input.GetKeyDown (KeyCode.Menu) || Input.GetKeyDown (KeyCode.F11)) {
				if (SceneManager.GetActiveScene ().buildIndex != 0) {//if (SceneManager.GetActiveScene ().name != "MainMenu") {
					if (SceneManager.GetActiveScene ().buildIndex != 1) {//if (SceneManager.GetActiveScene ().name != "GamePlay") {
						SceneManager.LoadScene (0);//SceneManager.LoadScene ("MainMenu");
					}
				}
			}

			if (Input.GetKeyDown (KeyCode.JoystickButton1) || Input.GetKeyDown (KeyCode.F12)) {
				if (SceneManager.GetActiveScene ().buildIndex == 0 && PlayerPrefs.GetInt ( "mode" ) == 0 ) {//if (SceneManager.GetActiveScene ().name == "MainMenu" && PlayerPrefs.GetInt ( "mode" ) == 0 ) {
					Debug.Log ( "Quitting" );
					Application.Quit ();

				}
				else if (SceneManager.GetActiveScene ().buildIndex == 1)//else if (SceneManager.GetActiveScene ().name == "GamePlay")
					SceneManager.LoadScene (0);//SceneManager.LoadScene ("MainMenu");
			}

		} else {

			Debug.Log ( "Controller Not Working" );

		}

	}

}