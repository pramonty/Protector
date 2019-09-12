using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L4StarShip : StarShip {

	protected bool lightningStarted;

	protected new void Start ()
	{
		base.Start();
		lightningStarted=false;
	}

	protected override void FireMissile ()
	{
		if (Time.timeSinceLevelLoad - lastSpawnTime >= 10 && !lightningStarted) {
			transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().Play();
			lightningStarted=true;
		}
	}

	protected override void SetSpawnTime(){
		lastSpawnTime=Time.timeSinceLevelLoad;
	}

	protected override void AdditionalUseCases(){
		transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().Stop();
	}

	protected override void OnTriggerEnter (Collider cldr)
	{
		if (cldr.gameObject.tag == "Arrow") {
			Destroy (cldr.gameObject);
			myHealth -= 8;
		}
		if (cldr.gameObject.tag == "TimeGrenade") {
			BroadcastMessage("HandleTimeGrenade");
			//Destroy(cldr.gameObject);
		}
	}
}
