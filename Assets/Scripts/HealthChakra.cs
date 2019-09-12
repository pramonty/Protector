using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthChakra : MonoBehaviour {

	public GameObject healingSmokePrefab;

	private Rigidbody myRgdBd;
	private Vector3 referencePointPosition;

	// Use this for initialization
	void Start ()
	{
		myRgdBd = GetComponent<Rigidbody> ();
		referencePointPosition=transform.GetChild(1).position;
		Vector3 normalDirection=(referencePointPosition-transform.position).normalized;
		myRgdBd.angularVelocity = 10*normalDirection;


	}
	
	void OnTriggerEnter (Collider cldr)
	{
		if (cldr.gameObject.tag == "Tower") {
			Instantiate(healingSmokePrefab,cldr.gameObject.transform.position,healingSmokePrefab.transform.rotation);
			Destroy(gameObject);
		}
	}
}
