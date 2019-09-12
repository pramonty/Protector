using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

	private Text myText;
	private Banker bnkr;
	// Use this for initialization
	void Start () {
		myText=GetComponent<Text>();
		myText.text="Coins: 0";
		bnkr=FindObjectOfType<Banker>();
	}
	
	// Update is called once per frame
	void Update () {
		myText.text="Coins: "+bnkr.GetMoney().ToString();
	}


}
