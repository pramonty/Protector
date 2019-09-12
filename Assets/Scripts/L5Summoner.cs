using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L5Summoner : ShieldBomb {

	//private Rigidbody myRgdBd;
	private GameObject referencePoint;
	private float birthTime;
	// Use this for initialization
	protected override void Start () {
		base.Start();
		//myRgdBd=GetComponent<Rigidbody>();
		referencePoint=transform.GetChild(0).gameObject;
		myRgdBd.angularVelocity=(referencePoint.transform.position-transform.position).normalized*5;
		birthTime=Time.timeSinceLevelLoad;
	}
	
	// Update is called once per frame
	protected override void Update ()
	{
		base.Update ();
		if (Time.timeSinceLevelLoad - birthTime >= 0.5f) {
			float velMagnitude = myRgdBd.velocity.magnitude;
			/*Vector3 currentVelocityDirection=myRgdBd.velocity.normalized;
		Vector3 targetDirection=pointOfInception.normalized;
		Vector3 finalResult=Vector3.RotateTowards(currentVelocityDirection,targetDirection,0.3f,0.2f);
		myRgdBd.velocity=finalResult*velMagnitude;*/
			Vector3 radialDirection = (pointOfInception - transform.position).normalized;
			myRgdBd.velocity = (myRgdBd.velocity.normalized + radialDirection).normalized * velMagnitude;
		}
	}

	protected override bool FirstEntryCondition ()
	{
		return (transform.position-pointOfInception).magnitude<=0.5f;
	}
}
