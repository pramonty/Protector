using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeGrenade : MonoBehaviour {

	public GameObject sloMoStrt;
	public GameObject cameraLens;
	public Vector3 lensPosition;

	private Rigidbody myRgdBd;

	// Use this for initialization
	void Start () {
		myRgdBd=GetComponent<Rigidbody>();
		myRgdBd.angularVelocity=new Vector3(3,3,3);
	}

	void OnTriggerEnter (Collider cldr)
	{
		if (cldr.gameObject.tag == "StarShip") {
			GameObject sloMoCld= Instantiate(sloMoStrt,gameObject.transform.position,Quaternion.identity);
			sloMoCld.transform.parent=cldr.gameObject.transform;
			Instantiate(cameraLens,lensPosition,cameraLens.transform.rotation);
			Destroy(gameObject);
		}
	}
}
