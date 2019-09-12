using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

	private float playerMaxHealth;
	private Slider mySlider;
	private Image sliderImage;
	private Tower myParentTower;

	// Use this for initialization
	void Start () {
		myParentTower=GetComponentInParent<Tower>();
		playerMaxHealth=myParentTower.GetMyHealth();
		mySlider=GetComponent<Slider>();
		mySlider.value=myParentTower.GetMyHealth()/playerMaxHealth;
		sliderImage=transform.Find("Fill Area").Find("Fill").gameObject.GetComponent<Image>();
		sliderImage.color=Color.green;
	}
	
	// Update is called once per frame
	void Update ()
	{
		mySlider.value = myParentTower.GetMyHealth () / playerMaxHealth;
		if (mySlider.value <= 0.5 && mySlider.value > 0.25) {
			sliderImage.color = Color.yellow;
		} else if (mySlider.value <= 0.25) {
			sliderImage.color = Color.red;
		} else {
			sliderImage.color=Color.green;
		}
	}
}
