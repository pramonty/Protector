using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experience : MonoBehaviour {

	private int playerExperiencePoints;
	private Banker bnkr;
	private int previousCoins;
	private bool frngRtChckStrt;
	private float chckStrtTime;
	private int arrowsPresent;
	private int previousFiringRate;

	// Use this for initialization
	void Start () {
		playerExperiencePoints=0;
		bnkr=GameObject.FindObjectOfType<Banker>();
		previousCoins=-1;
		frngRtChckStrt=false;
		chckStrtTime=-1;
		arrowsPresent=0;
		previousFiringRate=0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (bnkr.GetMoney () - previousCoins >= 100 && bnkr.GetMoney () <= 600) {
			playerExperiencePoints++;
			previousCoins = bnkr.GetMoney ();
		}

		if (Random.Range (0, 1.1f) > 0.5f && !frngRtChckStrt) {
			frngRtChckStrt = true;
			chckStrtTime = Time.timeSinceLevelLoad;
			arrowsPresent = GameObject.FindGameObjectsWithTag ("Arrow").Length;
		}
		if (chckStrtTime != -1 && Time.timeSinceLevelLoad - chckStrtTime >= 1) {
			int presentFiringRate = GameObject.FindGameObjectsWithTag ("Arrow").Length - arrowsPresent;
			if (presentFiringRate - previousFiringRate >= 2) {
				previousFiringRate=presentFiringRate;
				playerExperiencePoints+=2;
			}
			frngRtChckStrt=false;
			chckStrtTime=-1;
		}
	}

	public int GetExperience ()
	{
		return playerExperiencePoints;
	}
}
