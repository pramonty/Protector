using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundToggleController : MonoBehaviour {

	private Toggle myToggle;

	// Use this for initialization
	void Start () {
		myToggle=GetComponent<Toggle>();
		myToggle.isOn=PlayerPrefManager.GetSoundOff();
	}
	
	public void ChangeValue(){
		PlayerPrefManager.SetSoundOff(myToggle.isOn);
	}
}
