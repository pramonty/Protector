using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseHandler : MonoBehaviour {

	private bool gamePaused;
	private MonoBehaviour[] allScripts;

	void Start ()
	{
		gamePaused = false;
		Time.timeScale=1;
		allScripts = FindObjectsOfType<CustomParentClass> ();
	}

	void Update(){
		allScripts = FindObjectsOfType<CustomParentClass> ();
	}

	public void OnClick ()
	{
		if (gamePaused) {
			Time.timeScale = 1;
			gamePaused = false;
			foreach (CustomParentClass cust in allScripts) {
				cust.enabled=true;
			}
		} else {
			Time.timeScale=0;
			gamePaused=true;
			foreach (CustomParentClass cust in allScripts) {
				cust.enabled=false;
			}
		}
	}

	public bool GetGamePaused(){
		return gamePaused;
	}
}
