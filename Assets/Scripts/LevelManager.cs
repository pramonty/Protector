using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEngine.Advertisements;

public class LevelManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(gameObject);
		//Advertisement.Initialize("3165047",true);
		//print(Advertisement.isInitialized);
	}
	
	public void LoadScene (string name)
	{
		SceneManager.LoadScene(name);
	}

	public string GetCurrentLevel(){
		return SceneManager.GetActiveScene().name;
	}

	public void RestartLevel ()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	/*public void ShowAdNow(){
		StartCoroutine(ShowAdAfterReady());
	}

	IEnumerator ShowAdAfterReady(){
		while(!Advertisement.IsReady("levelComplete"))
			yield return null;

		Advertisement.Show("levelComplete");
	}*/
}
