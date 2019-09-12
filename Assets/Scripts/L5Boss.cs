using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L5Boss : StarShip {

	private bool diamondHeadDestroyed;
	private SphereCollider myCldr;

	protected override void Start ()
	{
		base.Start();
		diamondHeadDestroyed=false;
		myCldr=GetComponent<SphereCollider>();
	}


	protected override void OnTriggerEnter (Collider cldr)
	{
		if (cldr.gameObject.tag == "Arrow") {
			if (!diamondHeadDestroyed) {
				cldr.gameObject.GetComponent<Rigidbody> ().velocity = (cldr.gameObject.transform.position - transform.position).normalized * 40;
			} else {
				Destroy(cldr.gameObject);
				myHealth-=10;
			}
		}
	}


	/*void OnCollisionEnter (Collision clsn)
	{

		if (clsn.gameObject.tag == "Arrow") {
			Destroy(clsn.gameObject);
		}
	}*/

	void DiamondHeadDestroyed(){
		diamondHeadDestroyed=true;
		myCldr.radius=2.3f;
		myCldr.center=new Vector3(0,3,0.5f);

	}

}
