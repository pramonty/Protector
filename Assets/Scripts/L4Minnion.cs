using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L4Minnion : Minions {

	private GameObject insectKiller;
	private ParticleSystem myPS;
	private Quaternion initialRotation;
	private bool goingUp;
	private bool previousGoingUp;
	private bool firstTimeRotationChange;
	private bool kamikaze;
	private bool insectKillerArrived;

	protected new void Start ()
	{
		base.Start ();
		goingUp = false;
		previousGoingUp = false;
		firstTimeRotationChange = true;
		kamikaze = false;
		myPS=transform.GetChild(2).gameObject.GetComponent<ParticleSystem>();
		initialRotation=transform.rotation;
		insectKillerArrived=false;
	}


	protected new void Update ()
	{
		if (!kamikaze && !insectKillerArrived) {
			base.Update ();
			goingUp = myRigidBody.velocity.y > 0 ? true : false;
			if (previousGoingUp != goingUp || firstTimeRotationChange) {
				int scaleFactor = firstTimeRotationChange ? 1 : 2;
				float distanceEval = !firstTimeRotationChange ? (plusY - minusY) : goingUp ? plusY - transform.position.y : transform.position.y - minusY;
				float angularVelocity = (20 * Mathf.PI / 180) / (distanceEval / Mathf.Abs (myRigidBody.velocity.y));
				float rotationAngle = goingUp ? -(angularVelocity * scaleFactor) : (angularVelocity * scaleFactor);
				myRigidBody.angularVelocity = new Vector3 (rotationAngle, 0, 0);
				firstTimeRotationChange = false;
			}

			previousGoingUp = goingUp;
		}
		if ((insectKiller=GameObject.FindGameObjectWithTag("ShieldWall")) && !insectKillerArrived) {
			insectKillerArrived=true;
			CommitSuicide();
		}
	}

	void CommitSuicide(){
		Vector3 targetRange=new Vector3(insectKiller.transform.position.x,Random.Range(-3.48f,6.53f),insectKiller.transform.position.z);
		myRigidBody.velocity=(targetRange-transform.position).normalized*3;
		myRigidBody.angularVelocity=Vector3.zero;
	}


	protected override void OnTriggerEnter (Collider cldr)
	{
		if (!insectKillerArrived) {
			base.OnTriggerEnter (cldr);
		}
		if (cldr.gameObject.tag == "Arrow" && !kamikaze) {
			hitCount++;
		} else if (cldr.gameObject.tag == "Arrow" && kamikaze) {
			hitCount++;
			if (hitCount >= 4) {
				DestroyMe ();
			}
		}

		if (cldr.gameObject.tag == "ShieldWall") {
			StartCoroutine(DestroyAfterSomeTime());
		}
	}

	void DestroyMe(){
			GameObject explsn = Instantiate (decimatedPrefab, transform.position, Quaternion.identity);
			explsn.transform.localScale = new Vector3 (3, 3, 3);
			bnkr.IncrementMoney(destructionWorth);
			Destroy (gameObject);
	}

	IEnumerator DestroyAfterSomeTime ()
	{	
		yield return new WaitForSeconds(1);
		DestroyMe();
	}

	protected override void Fire ()
	{
		for (int i = 0; i < 2; i++) {
			GameObject laser = (GameObject)Instantiate (laserPrefab, transform.GetChild(i).position, laserPrefab.transform.rotation);
			laser.GetComponent<Rigidbody> ().velocity = new Vector3 (Mathf.Clamp(5f+myRigidBody.velocity.x,3,8), 0f, 0f);
		}
	}

	protected override void HandleDestruction(){
		myRigidBody.velocity=(towerObjective.transform.position-transform.position).normalized*10;
		transform.rotation=initialRotation;
		InvokeRepeating("Kamikaze",0,0.5f);
	}

	void Kamikaze ()
	{
		float xAngVel = !kamikaze ? Random.Range (2, 5) : -myRigidBody.angularVelocity.x;
		myRigidBody.angularVelocity = new Vector3 (xAngVel, 0, 0);
		if (!kamikaze) {
			myPS.Play ();
			kamikaze = true;
			hitCount=0;
		}
	}

	public Banker GetBanker ()
	{
		return bnkr;
	}

	public override void GameMasterDestruction(){
		DestroyMe();
	}
}
