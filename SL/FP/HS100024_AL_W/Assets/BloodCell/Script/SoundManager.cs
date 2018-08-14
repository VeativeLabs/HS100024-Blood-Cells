using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	public static SoundManager sm;

	public AudioClip ClickSound;
	public AudioClip ScrollSound;
	public AudioClip RightSound;
	public AudioClip WrongSound;
	AudioSource asc;

	// Use this for initialization
	void Start () {
		sm = this;
		asc = GetComponent<AudioSource> ();
	}

	public void PlayClickSound(){

//		asc.clip = ClickSound;
		asc.PlayOneShot (ClickSound);

	}

	public void PlayScrollSound(){

//		asc.clip = ScrollSound;
		asc.PlayOneShot (ScrollSound);

	}

	public void PlayRightSound(){

//		asc.clip = RightSound;
		asc.PlayOneShot (RightSound);

	}

	public void PlayWrongSound(){

//		asc.clip = WrongSound;
		asc.PlayOneShot (WrongSound);

	}

}
