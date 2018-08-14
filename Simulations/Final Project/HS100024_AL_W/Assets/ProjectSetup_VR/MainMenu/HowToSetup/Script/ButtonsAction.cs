using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonsAction : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Menu))
        {
			SceneManager.LoadScene("MainMenu");
        }

        if (Input.GetKeyDown(KeyCode.JoystickButton1))
        {
			SceneManager.LoadScene("MainMenu");
        }
    }
}
