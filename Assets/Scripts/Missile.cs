using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour {

	private Rigidbody myRgdbd;

	void Start ()
	{
		myRgdbd=GetComponent<Rigidbody>();
	}
	
	void ImpartVelocity(float velocity){
		myRgdbd.velocity=new Vector3(velocity,0,0);
	}
}
