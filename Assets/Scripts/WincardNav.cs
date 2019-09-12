using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WincardNav : MonoBehaviour {

	[Tooltip("0:Start Page; 1:Next Level")]
	public int direction;

	private GameMaster thisLevelMaster;
	// Use this for initialization
	void Start () {
		thisLevelMaster=FindObjectOfType<GameMaster>();
	}
	

	public void onClick ()
	{
		thisLevelMaster.LoadLevel(direction);
	}
}
