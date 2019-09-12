using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour {

	private GameObject[] arrows;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		arrows = GameObject.FindGameObjectsWithTag ("Arrow");
		foreach (GameObject arrow in arrows) {
			arrow.GetComponent<Rigidbody>().velocity=5*((transform.position-arrow.transform.position).normalized);
		}
	}

	void OnTriggerEnter (Collider cldr)
	{
		if (cldr.gameObject.tag == "Arrow") {
			Destroy(cldr.gameObject);
		}
			
	}
}
