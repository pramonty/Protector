using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L5Tower : Tower {

	protected override void Update ()
	{
		if (myHealth <= 0) {
			GameObject turbine = GameObject.FindGameObjectWithTag ("Turbine");
			if (turbine) {
				myHealth = turbine.GetComponent<WindTurbine> ().GetMyHealth ();
				Destroy (turbine);
			} else {
				gameMaster.TowerDestroyed();
				Destroy(gameObject);
			}
		}
	}

	protected override void OnTriggerEnter (Collider cldr)
	{
		if (cldr.gameObject.tag == "Laser") {
			//shouldChangeColor=true;
			DealDamage (2);
			Destroy (cldr.gameObject);
		}
		if (cldr.gameObject.tag == "Shell") {
			DealDamage(10);
			Destroy(cldr.gameObject);
		}
	}
}
