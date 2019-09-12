using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L4Flares : MonoBehaviour {

	private GameObject target;
	private Rigidbody myRgdBd;
	private float speed;


	// Use this for initialization
	void Awake () {
		myRgdBd=GetComponent<Rigidbody>();
		speed=30;
	}

	public void SetSpeed (float speed)
	{
		this.speed=speed;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		if (target) {
			myRgdBd.velocity = (target.transform.position - transform.position).normalized * speed;
		} else {
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter (Collider cldr)
	{
		if (cldr.gameObject.tag == "Arrow") {
			Destroy(cldr.gameObject);
			Destroy(gameObject);
		}

	}

	public void SetTarget(GameObject target){
		this.target=target;
	}

	public GameObject GetTarget ()
	{
		return target;
	}
}
