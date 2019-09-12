using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarShipHB : MonoBehaviour {

	private static float myMaxHealth=0;
	private Slider mySlider;
	private Image sliderImage;
	private LevelBoss strShp;

	// Use this for initialization
	void Start () {
		strShp=GameObject.FindGameObjectWithTag("StarShip").GetComponent<LevelBoss>();
		if(myMaxHealth==0)myMaxHealth=strShp.GetMyHealth();
		mySlider=GetComponent<Slider>();
		mySlider.value=strShp.GetMyHealth()/myMaxHealth;
		sliderImage=transform.Find("Fill Area").Find("Fill").gameObject.GetComponent<Image>();
		sliderImage.color=Color.green;
	}
	
	// Update is called once per frame
	void Update ()
	{
		mySlider.value = strShp.GetMyHealth() / myMaxHealth;
		if (mySlider.value <= 0.5 && mySlider.value>0.25) {
			sliderImage.color=Color.yellow;
		}else if(mySlider.value<=0.25){
			sliderImage.color=Color.red;
		}
	}
}
