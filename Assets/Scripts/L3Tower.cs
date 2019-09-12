using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L3Tower : Tower {

	public GameObject acidSplashPrefab;

	protected override void OnTriggerEnter (Collider cldr)
	{
		if (cldr.gameObject.tag == "PotShot") {
			//shouldChangeColor=true;
			DealDamage(30);
			Instantiate(acidSplashPrefab,cldr.gameObject.transform.position,acidSplashPrefab.transform.rotation);
			Destroy(cldr.gameObject);
		}
	}

	protected override void OnCollisionEnter (Collision clsn)
	{
		if (clsn.gameObject.tag == "PotShot") {
			//shouldChangeColor=true;
			DealDamage(30);
			Instantiate(acidSplashPrefab,clsn.gameObject.transform.position,acidSplashPrefab.transform.rotation);
			Destroy(clsn.gameObject);
		}
	}

	void OnParticleCollision (GameObject ps)
	{
		if (myHealth <= 995) {
			myHealth += 5;
		}
	}
}
