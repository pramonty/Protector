using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenShieldWall : MonoBehaviour {

	private Rigidbody myRgdbd;

	// Use this for initialization
	void Start () {
		myRgdbd=GetComponent<Rigidbody>();
		myRgdbd.AddForce(new Vector3(0,-300,0));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
