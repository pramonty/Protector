using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindTurbine : Tower {

	protected override void OnTriggerEnter (Collider cldr)
	{
		if (cldr.gameObject.tag == "Shell") {
			myHealth-=100;
			Destroy(cldr.gameObject);
		}	
	}

	protected override void Update ()
	{
		if (myHealth <= 0) {
			Destroy(gameObject);
		}
	}

}
