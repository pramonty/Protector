using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstMessageBehaviour : MonoBehaviour {

	private bool persistentDisplay;

	void Start ()
	{
		if (PlayerPrefManager.GetFirstMessageShown ()) {
			gameObject.SetActive(false);
		}	
		persistentDisplay=false;

	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Mouse0) && !persistentDisplay) {
			PlayerPrefManager.SetFirstMessageShown(true);
			gameObject.SetActive(false);
		}
	}

	public void SetPersistentDisplay(bool val){
		persistentDisplay=val;
	}

}
