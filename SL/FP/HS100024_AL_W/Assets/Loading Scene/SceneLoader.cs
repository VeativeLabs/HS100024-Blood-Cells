using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour 
{
    public void LoadScene(int BuildIndex)
    {
        SoundManager.sm.PlayClickSound();
        LoadingScene.LoadingSceneIndex = BuildIndex;
    }

    public void Quit()
    {
        SoundManager.sm.PlayClickSound();
        Application.Quit();
        Debug.Log("Quit");
    }
}
