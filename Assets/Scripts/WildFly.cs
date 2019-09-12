using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildFly : Minions {

	
	public int hitThreshold;

	private Animator myAnim;
	private float lastFiredTime;

	protected override void Start(){
		base.Start();
		myAnim=GetComponent<Animator>();
		lastFiredTime=0;
		shouldDance=true;
	}

	protected override void Update ()
	{
		if (Time.timeSinceLevelLoad - lastFiredTime >= 5 && WithinBounds ()) {
			myAnim.SetTrigger ("Fire");
			lastFiredTime = Time.timeSinceLevelLoad;
		}
		if (shouldDance) {
			Dance ();
		}

		if (!towerObjective) {
			myRigidBody.velocity=new Vector3(3,0,0);
			shouldDance=false;
		}
	}

	protected override void Fire(){
		GameObject firedShot=(GameObject)Instantiate(laserPrefab,transform.position+new Vector3(2.6f,0.8f,0),Quaternion.identity);
		firedShot.GetComponent<Rigidbody>().velocity=new Vector3(10,0,0);
	}

	protected override void OnTriggerEnter (Collider cldr)
	{
		if (cldr.gameObject.tag == "Arrow") {
			Destroy (cldr.gameObject);
			hitCount++;
			if (hitCount >= hitThreshold) {
				HandleDestruction();
			}

		}
		if (cldr.gameObject.tag == "CanonBall") {
			Destroy(cldr.gameObject);
			HandleDestruction();
		}
	}

	public override void GameMasterDestruction(){
		HandleDestruction();
	}
}
