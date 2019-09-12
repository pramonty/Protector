using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseCardNav : MonoBehaviour {

	private GameMaster lvlMstr;

	// Use this for initialization
	void Start () {
		lvlMstr=FindObjectOfType<GameMaster>();
	}
	
	public void OnClick (int direction)
	{
		if (direction == 0) {
			lvlMstr.RestartLevel ();
		} else if (direction == 1) {
			lvlMstr.LoadLevel(0);
		}
	}
}
