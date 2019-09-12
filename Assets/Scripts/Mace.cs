using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mace : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Vector3 nrmlzdDrctn=(transform.GetChild(0).position-transform.position).normalized;
		GetComponent<Rigidbody>().angularVelocity=nrmlzdDrctn*12;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
