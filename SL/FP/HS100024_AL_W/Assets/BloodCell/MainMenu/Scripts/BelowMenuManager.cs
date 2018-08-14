using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class BelowMenuManager : MonoBehaviour {
	[ HideInInspector ]
	public List < GameObject > belowMenuButtons;

	[ HideInInspector ]
	public float distanceBetweenHomeAndOtherButtons;

	public bool mode;

	void Start ( ) {

		Image [ ] belowMenuImages = GetComponentsInChildren < Image > ( );

		for ( int i = 0 ; i < belowMenuImages.Length ; i++ )
			belowMenuButtons.Add ( belowMenuImages [ i ].gameObject ) ;

		distanceBetweenHomeAndOtherButtons = belowMenuButtons [ 1 ].transform.localPosition.x - belowMenuButtons [ 0 ].transform.localPosition.x;

		#if !UNITY_EDITOR
		mode = PlayerPrefs.GetInt ( "mode" ) != 0 ? true : false ;
		#endif

		if ( mode ) {

			belowMenuButtons [ 0 ].transform.localPosition = new Vector3 ( -distanceBetweenHomeAndOtherButtons/2 , belowMenuButtons [ 0 ].transform.localPosition.y , belowMenuButtons [ 0 ].transform.localPosition.z );
			belowMenuButtons [ 1 ].SetActive ( false );
			belowMenuButtons [ 2 ].transform.localPosition = new Vector3 ( distanceBetweenHomeAndOtherButtons/2 , belowMenuButtons [ 2 ].transform.localPosition.y , belowMenuButtons [ 2 ].transform.localPosition.z );

		} else {

			belowMenuButtons [ 0 ].transform.localPosition = new Vector3 ( -distanceBetweenHomeAndOtherButtons , belowMenuButtons [ 0 ].transform.localPosition.y , belowMenuButtons [ 0 ].transform.localPosition.z );
			belowMenuButtons [ 1 ].SetActive ( true );
			belowMenuButtons [ 2 ].transform.localPosition = new Vector3 ( distanceBetweenHomeAndOtherButtons , belowMenuButtons [ 2 ].transform.localPosition.y , belowMenuButtons [ 2 ].transform.localPosition.z );

		}

	}
	public void GoToMainMenu() {
		Time.timeScale = 1;
		SceneManager.LoadScene("MainMenu");
	}

	public void ResetLevel() {
		Time.timeScale = 1;
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void GOHome() {
		Application.Quit ();
		return;
	}
}
