using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class PlayPauseSimulation : MonoBehaviour
{

    public static PlayPauseSimulation instance;

    public GameObject playPauseSimulationCanvas;
    public GameObject startMessageCanvas;
    public bool isPaused;

    [ HideInInspector]
    public AudioSource[ ] audioScript;

    [ HideInInspector]
    public List <bool> audioPlaying;

    public bool simualtionAlreadyPaused;

    void Awake()
    {

//        if (instance != null && instance != this)
//        {
//            Destroy(instance.gameObject);
//            instance = null;
//        } 
//
//        if (instance == null)
//        {
//            instance = this;
//            DontDestroyOnLoad(gameObject);	
//        }
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad(gameObject);	
		}
		else if(instance != this){
			Destroy(this.gameObject);
			return;
		}

        audioPlaying.Clear();

        PlayerPrefs.SetInt("Pause", 0);

        PlayerPrefs.SetInt("Toggle", 1);

    }

    public void ShowStartErrorMessage(string messageToBeShown)
    {

        startMessageCanvas.GetComponentInChildren < Text >().text = messageToBeShown;

        startMessageCanvas.transform.SetParent(Camera.main.transform);
        startMessageCanvas.transform.localPosition = new Vector3(0, 0, 0.6f);
        startMessageCanvas.transform.localRotation = Quaternion.identity;
        startMessageCanvas.SetActive(true);

        AudioListener.volume = 0;

        Time.timeScale = 0;

    }


    void Update()
	{
		#if UNITY_EDITOR
		if (SceneManager.GetActiveScene ().name != "LoadingScene") {
			if (isPaused && PlayerPrefs.GetInt ("Toggle") == 1) {

				PlayerPrefs.SetInt ("Pause", 1);
				PlayerPrefs.SetInt ("Toggle", 0);
				SetTimeScale (0);

			} else if (!isPaused && PlayerPrefs.GetInt ("Toggle") == 0) {

				PlayerPrefs.SetInt ("Pause", 0);
				PlayerPrefs.SetInt ("Toggle", 1);
				SetTimeScale (1);

			}
		}
		#endif
		if (SceneManager.GetActiveScene ().name != "LoadingScene") {
			if (PlayerPrefs.GetInt ("Pause") == 1 && PlayerPrefs.GetInt ("Toggle") == 1) {
				for (int i = 0; i < Camera.main.transform.parent.childCount; i++) {
					if (Camera.main.transform.parent.GetChild (i).GetComponent<Camera> () && Camera.main.transform.parent.GetChild (i).tag != "MainCamera") {
						Camera.main.transform.parent.GetChild (i).gameObject.SetActive (false);
					}
				}
				PlayerPrefs.SetInt ("Toggle", 0);
				SetTimeScale (0);


			} else if (PlayerPrefs.GetInt ("Pause") == 0 && PlayerPrefs.GetInt ("Toggle") == 0) {
				for (int i = 0; i < Camera.main.transform.parent.childCount; i++) {
					if (Camera.main.transform.parent.GetChild (i).GetComponent<Camera> () && Camera.main.transform.parent.GetChild (i).tag != "MainCamera") {
						Camera.main.transform.parent.GetChild (i).gameObject.SetActive (true);
					}
				}
				PlayerPrefs.SetInt ("Toggle", 1);
				SetTimeScale (1);
			}
		}
	}

    public void SetTimeScale(float scale)
    {
		if (scale == 1)
		{

			playPauseSimulationCanvas.SetActive(false);
			playPauseSimulationCanvas.transform.SetParent(transform);

			HandleAudio("Pause");
			AudioListener.volume = 1;
			isPaused = false;

			if (simualtionAlreadyPaused)
			{

				scale = 0;

			}

		}
		else if (scale == 0)
		{

			playPauseSimulationCanvas.transform.SetParent(Camera.main.transform);
			playPauseSimulationCanvas.transform.localPosition = new Vector3(0, 0, 0.6f);
			playPauseSimulationCanvas.transform.localRotation = Quaternion.identity;
			playPauseSimulationCanvas.SetActive(true);

			HandleAudio("Play");
			AudioListener.volume = 0;

			isPaused = true;

			if (Time.timeScale == 0)
			{

				simualtionAlreadyPaused = true;

			}
			else
			{

				simualtionAlreadyPaused = false;

			}

		}

		Time.timeScale = scale;


    }

    public void HandleAudio(string state)
    {

		audioScript = Resources.FindObjectsOfTypeAll < AudioSource >();

		if (audioScript.Length == 0)
			return;

		if (state == "Pause")
		{

			if (audioPlaying.Count == 0)
				return;

			for (int i = 0; i < audioScript.Length; i++)
			{

				if (audioPlaying[i])
				{

					audioScript[i].UnPause();

				}

			}


		}
		else if (state == "Play")
		{

			audioPlaying.Clear();

			for (int i = 0; i < audioScript.Length; i++)
			{


				if (audioScript[i].isPlaying)
				{

					audioPlaying.Add(true);
					audioScript[i].Pause();

				}
				else
					audioPlaying.Add(false);

			}

		}

    }

}