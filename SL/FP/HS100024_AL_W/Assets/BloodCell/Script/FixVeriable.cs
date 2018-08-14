using UnityEngine;
using System.Collections;

public class FixVeriable : MonoBehaviour {
	public static bool isPlayerStill=false;
	public readonly static int discription=0,collection=1;
	public static int PartNumber = -1;
	public static int collectNumber = 0;
	public static int selectedbloodcell=-1;
	public static bool isActivityStart = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static void Restart(){
		collectNumber = 0;
	}
}
