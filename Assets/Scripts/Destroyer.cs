using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour {

	void OnTriggerExit (Collider cldr)
	{
		/*if (cldr.gameObject.tag == "UFO" || cldr.gameObject.tag=="Missile" || cldr.gameObject.tag=="BrokenShieldWall") {
			Destroy (cldr.gameObject.transform.parent.gameObject);
		} else if(cldr.gameObject.tag!="Warper") {
			Destroy(cldr.gameObject);
		}*/
		Destroy(cldr.gameObject.transform.root.gameObject);
	}

	void OnCollisionEnter (Collision clsn)
	{
		if (clsn.gameObject.tag == "Arrow") {
			Destroy(clsn.gameObject);
		}
	}
}
