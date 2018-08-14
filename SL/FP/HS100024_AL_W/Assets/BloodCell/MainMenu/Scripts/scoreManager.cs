using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class scoreManager : MonoBehaviour {
    public Text scoreText;
	// Use this for initialization


	void Start () {
        PlayerPrefs.SetInt("score", 0);
        // how to save score ex==========

       // PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("score") + 5);
    }
	
	// Update is called once per frame
	void Update () {
//        scoreText.text = "Score - " + PlayerPrefs.GetInt("score");
      
    }


}
