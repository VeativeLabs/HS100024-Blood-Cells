using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class AfterLOHandler : MonoBehaviour {

	[ HideInInspector ]
	public List < GameObject > afterLOMenuButtons;

	[ HideInInspector ]
	public float distanceBetweenMenuAndOtherButtons;

	public bool mode;

	void Start ( ) {

		Button [ ] afterLOMenuImages = GetComponentsInChildren < Button > ( );

		for ( int i = 0 ; i < afterLOMenuImages.Length ; i++ )
			afterLOMenuButtons.Add ( afterLOMenuImages [ i ].gameObject ) ;

		distanceBetweenMenuAndOtherButtons = afterLOMenuButtons [ 1 ].transform.localPosition.y - afterLOMenuButtons [ 0 ].transform.localPosition.y;

		#if !UNITY_EDITOR
		mode = PlayerPrefs.GetInt ( "mode" ) != 0 ? true : false ;
		#endif

		if ( mode ) {

			afterLOMenuButtons [ 0 ].transform.localPosition = new Vector3 ( afterLOMenuButtons [ 0 ].transform.localPosition.x , -( distanceBetweenMenuAndOtherButtons - ( distanceBetweenMenuAndOtherButtons *1/3 ) ) , afterLOMenuButtons [ 0 ].transform.localPosition.z );
			afterLOMenuButtons [ 1 ].transform.localPosition = new Vector3 ( afterLOMenuButtons [ 2 ].transform.localPosition.x , distanceBetweenMenuAndOtherButtons/2 , afterLOMenuButtons [ 2 ].transform.localPosition.z );
			afterLOMenuButtons [ 2 ].SetActive ( false );

		} else {

			afterLOMenuButtons [ 0 ].transform.localPosition = new Vector3 ( afterLOMenuButtons [ 0 ].transform.localPosition.x , -distanceBetweenMenuAndOtherButtons , afterLOMenuButtons [ 0 ].transform.localPosition.z );
			afterLOMenuButtons [ 1 ].transform.localPosition = new Vector3 ( afterLOMenuButtons [ 2 ].transform.localPosition.x , 0 , afterLOMenuButtons [ 2 ].transform.localPosition.z );
			afterLOMenuButtons [ 2 ].transform.localPosition = new Vector3 ( afterLOMenuButtons [ 2 ].transform.localPosition.x , distanceBetweenMenuAndOtherButtons , afterLOMenuButtons [ 2 ].transform.localPosition.z );
			afterLOMenuButtons [ 2 ].SetActive ( true );

		}

	}

}
