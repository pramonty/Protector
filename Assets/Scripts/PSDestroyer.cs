using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSDestroyer : MonoBehaviour {

	private ParticleSystem myPS;
	// Use this for initialization
	void Start () {
		myPS=GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!myPS.IsAlive()) {
			Destroy(gameObject);
		}
	}
}
