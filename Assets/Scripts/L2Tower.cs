using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L2Tower : Tower {

	protected override void OnTriggerEnter(Collider cldr){
		if (cldr.gameObject.tag == "Laser") {
			//shouldChangeColor=true;
			DealDamage(5);
			Destroy(cldr.gameObject);
		}
	}
}
