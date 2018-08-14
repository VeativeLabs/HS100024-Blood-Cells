using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public enum MenuItems { HowToSetup , Objective , Level1 , Level2 , Level3};

public class MenuManager : MonoBehaviour {
	public static MenuManager Instance;

	public MenuItems currItem;

	GameObject currObj;

	// Use this for initialization
	void Start () {
		Instance = this;
	}

	


	public void OnMainMenuBtnClick(int order) {
		switch((MenuItems)order) { 
		case  MenuItems.HowToSetup:
			SceneManager.LoadScene("Setup");
			break;
		case  MenuItems.Objective:
			SceneManager.LoadScene("ObjectiveScene");
			break;
		case  MenuItems.Level1:
			SceneManager.LoadScene("Blood_Cell");
			break;
		case  MenuItems.Level2:
			SceneManager.LoadScene("Blood_Cell_level2");
			break;
		case  MenuItems.Level3:
			break;
		}
		currItem = (MenuItems)order;
	}

	public void GoToMainMenu() {
		SceneManager.LoadScene("Blood_Cell");
	}
    public void Quit() {
        Application.Quit();
		return;
    }


}
