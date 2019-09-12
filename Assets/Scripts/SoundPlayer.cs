using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour {

	public AudioClip clip;

	private LevelManager lvlMngr;

	// Use this for initialization
	void Start ()
	{
		AudioSource audSrc = gameObject.AddComponent<AudioSource> ();
		audSrc.clip = clip;
		if (!PlayerPrefManager.GetSoundOff ()) {
			audSrc.Play ();
		}
		lvlMngr=FindObjectOfType<LevelManager>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Time.timeSinceLevelLoad >= clip.length)	 {
			lvlMngr.LoadScene("Start");
		}
	}
}
