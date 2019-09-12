using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour {

	private LevelManager lvlMngr;
	private GameObject startText;

	// Use this for initialization
	void Start ()
	{
		lvlMngr = FindObjectOfType<LevelManager> ();
		startText = GameObject.Find ("StartText");
		if (startText) {
			StartTextHandler();		
		}
	}


	void StartTextHandler ()
	{
		string nextLevelToLoad=PlayerPrefManager.GetNextLevel();
		startText.GetComponent<Text>().text=nextLevelToLoad==""?"Start":"Continue";
	}


	public void LoadScene(string name){
		lvlMngr.LoadScene(name);
	}

	public void LoadGame ()
	{
		lvlMngr.LoadScene(PlayerPrefManager.GetNextLevel()==""?"Level1":PlayerPrefManager.GetNextLevel());
	}

	public void LoadSceneConditionally (string levelToLoad)
	{
		
			if (PlayerPrefManager.GetLevelUnlock(levelToLoad)) {
				lvlMngr.LoadScene(levelToLoad);
			}
	}

	public void QuitGame ()
	{
		Application.Quit();
	}

	public void ClearPlayerRef ()
	{
		PlayerPrefManager.ClearAllMYPreferences();
		StartTextHandler();
	}
}
