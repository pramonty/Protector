using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyShattersArrow : MonoBehaviour {

	void OnTriggerEnter (Collider cldr)
	{
		if (cldr.gameObject.tag == "Arrow") {
			cldr.gameObject.transform.GetChild(0).gameObject.AddComponent<Shatterer>().Shatter();
		}
	}
}
