using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class EnergyBar : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (shootingCell.instance.bulletcount > 0) {
			float temp = this.GetComponent<Image> ().fillAmount;
			temp= Mathf.Lerp (temp,(shootingCell.instance.getbulletcount()*1f) /(1f*shootingCell.instance.TotalObject),.1f);
			this.GetComponent<Image> ().fillAmount = temp;

			float r = (1 - (shootingCell.instance.getbulletcount()*1f) /(1f*shootingCell.instance.TotalObject)*1);
			float g = ((shootingCell.instance.getbulletcount()*1f) /(1f*shootingCell.instance.TotalObject))*(163f/255f);
			this.GetComponent<Image> ().DOColor(new Color(r,g,0,1),.1f);
		}
	}

}
