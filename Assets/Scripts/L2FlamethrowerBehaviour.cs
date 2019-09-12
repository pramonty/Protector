using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L2FlamethrowerBehaviour : MonoBehaviour {

	private ParticleSystem myPS;


	// Use this for initialization
	void Start ()
	{
		myPS = GetComponent<ParticleSystem> ();
		if (GameObject.FindGameObjectWithTag ("ShieldWall")) {
			myPS.trigger.SetCollider (0, GameObject.FindGameObjectWithTag ("ShieldWall").GetComponent<BoxCollider> ());
		}
//		print("Called by: "+gameObject.name);
	}
}
