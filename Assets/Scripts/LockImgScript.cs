using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockImgScript : MonoBehaviour {

	public string levelToCheck;

	private string parentName;

	// Use this for initialization
	void Start ()
	{
		parentName = transform.parent.gameObject.name;
		if (parentName != "LVL1") {
			if (PlayerPrefManager.GetLevelUnlock(levelToCheck)) {
				Destroy(gameObject);
			}
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
