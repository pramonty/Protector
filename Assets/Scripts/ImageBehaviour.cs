using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageBehaviour : CustomParentClass {

	public GameObject myPrefab;
	public int minimumMoneyNeeded;
	public  bool imageActive;

	private  Image myImage;
	private  Color tempColor;
	private GameObject[] otherImages;
	private bool messageShown;
	private Banker bnkr;
	private Text requiredCoinText;



	// Use this for initialization
	void Start ()
	{	
		otherImages = new GameObject[GameObject.FindGameObjectsWithTag ("Arsenal").Length - 1];
		myImage = GetComponent<Image> ();
		tempColor = myImage.color;
		messageShown=false;
		bnkr=FindObjectOfType<Banker>();
		requiredCoinText=transform.GetChild(0).gameObject.GetComponent<Text>();
		requiredCoinText.text=minimumMoneyNeeded.ToString();
		int i = 0;
		foreach (GameObject go in GameObject.FindGameObjectsWithTag("Arsenal")) {
			if (go.name != gameObject.name) {
				otherImages[i]=go;
				i++;
			}
		}
		if (imageActive && SatisfiesMinimumMoneyCriteria()) {	
			tempColor.a = 1;
			myImage.color=tempColor;
		} else {
			tempColor.a = 0.5f;
			myImage.color=tempColor;	
		}
	}

	bool SatisfiesMinimumMoneyCriteria ()
	{
		return (bnkr.GetMoney()>=minimumMoneyNeeded);
	}

	void SwapOtherImages ()
	{
		foreach (GameObject go in otherImages) {
			go.GetComponent<ImageBehaviour>().DeactivateImage();
		}
	}

	void DeactivateImage ()
	{
		tempColor.a = 0.5f;
		myImage.color=tempColor;	
		imageActive=false;
	}


	void Update ()
	{
		if (bnkr.GetMoney () >= minimumMoneyNeeded) {
			requiredCoinText.color = Color.green;
		} else {
			requiredCoinText.color=Color.red;
		}
	}

	public  void ImageClicked ()
	{
		if (imageActive) {	
			tempColor.a = 0.5f;
			myImage.color=tempColor;	
			imageActive=false;
		} else if(SatisfiesMinimumMoneyCriteria()){
			tempColor.a = 1;
			myImage.color=tempColor;	
			SwapOtherImages();
			imageActive=true;
		}
	}

	public bool GetImageActive(){
		return imageActive;
	}

	public bool GetMessageShown ()
	{
		return messageShown;
	}
	public void SetMessageShown (bool val)
	{
		messageShown=val;
	}
}
