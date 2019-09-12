using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//Attached to MainCanvas\LowerMessagePanel\MessageBox
public class MessageHandler : MonoBehaviour {


	private Text myText;
	//private string message;
	// Use this for initialization
	void Start () {
		myText=GetComponent<Text>();
		myText.text="Destroy the Aliens ship before they destroy your tower";
		Invoke("WipeOutMessage",10f);
	}

	public void SetMessage (string message)
	{
		myText.text=message;
		Invoke("WipeOutMessage",5f);
	}

	void WipeOutMessage ()
	{	
		myText.text="";
	}
	
	// Update is called once per frame
	void Update () {
	}
}
