using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minions : CustomParentClass {

	public GameObject laserPrefab;
	public GameObject decimatedPrefab;
	public float minusX;
	public float plusX;
	public float minusY;
	public float plusY;
	public float velocityScaler;
	public int destructionWorth;
	public int thresholdHit;
	public float arriveThreshold;

	protected GameObject towerObjective;
	protected Rigidbody myRigidBody;
	protected bool shouldDance;
	protected bool dancingVelocityImparted;
	protected float upperBound;
	protected float lowerBound;
	protected Banker bnkr;
	protected int hitCount;

	// Use this for initialization
	protected virtual void Start () {
		myRigidBody=GetComponent<Rigidbody>();
		myRigidBody.velocity=new Vector3(3f,0f,0f) * velocityScaler;
		towerObjective=GameObject.FindGameObjectWithTag("Tower");
		shouldDance=false;
		dancingVelocityImparted=false;
		hitCount=0;
		bnkr=FindObjectOfType<Banker>();
		upperBound=towerObjective.transform.position.y+towerObjective.GetComponent<MeshRenderer>().bounds.size.y/2;
		lowerBound=towerObjective.transform.position.y-towerObjective.GetComponent<MeshRenderer>().bounds.size.y/2;
	}
	
	// Update is called once per frame
	protected virtual void Update ()
	{
		if (WithinBounds () && dancingVelocityImparted) {
			if (Time.frameCount % 40 == 0) {
				if (Random.Range (0, 10) > 5) {
					Fire();
				}
			}
		}
		if (towerObjective) {
			if (Mathf.Abs (transform.position.x - towerObjective.transform.position.x) <= arriveThreshold) {
				shouldDance = true;
			}
		} else {
			shouldDance=false;
			myRigidBody.velocity=new Vector3(3f,0f,0f);
		}

		if (shouldDance) {
			Dance();
		}

		if (hitCount >= thresholdHit) {
			HandleDestruction();
		}
		
	}

	protected virtual void HandleDestruction(){
			bnkr.IncrementMoney (destructionWorth);
			Instantiate (decimatedPrefab, transform.position, decimatedPrefab.transform.rotation);
			Destroy (gameObject);
	}

	protected bool WithinBounds ()
	{
		if (transform.position.y <= upperBound && transform.position.y >= lowerBound) {	
			return true;
		} else {
			return false;
		}
	}

	protected virtual void Fire ()
	{
		GameObject laser = (GameObject)Instantiate (laserPrefab, transform.position, laserPrefab.transform.rotation);
		laser.GetComponent<Rigidbody> ().velocity = new Vector3 (5, 0, 0);
	}

	void OnCollisionEnter (Collision clsn)
	{
		if (clsn.gameObject.tag == "Arrow" || clsn.gameObject.tag == "Trident") {
			Destroy (clsn.gameObject);
			hitCount++;
		}

	}

	protected virtual void OnTriggerEnter (Collider cldr)
	{
		
		if (cldr.gameObject.tag == "Probe" && !shouldDance) {
				myRigidBody.velocity = Vector3.zero;
				shouldDance = true;
		}
	}

	protected void Dance ()
	{
		
		if (!dancingVelocityImparted) {
			myRigidBody.velocity = new Vector3 (Random.Range (-5f, -1f), Random.Range (-5f, 5f), 0f);
			dancingVelocityImparted = true;

		}
		if (transform.position.y > plusY) {
			myRigidBody.velocity = new Vector3 (myRigidBody.velocity.x, Random.Range(-5f,-1f)*velocityScaler, 0f);

		} else if (transform.position.y < minusY) {
			myRigidBody.velocity = new Vector3 (myRigidBody.velocity.x, Random.Range(1f,5f)*velocityScaler, 0f);

		} else if (transform.position.x < minusX) {
			myRigidBody.velocity = new Vector3 (Random.Range(1f,5f)*velocityScaler, myRigidBody.velocity.y, 0f);

		} else if (transform.position.x > plusX) {
			myRigidBody.velocity = new Vector3 (Random.Range(-5f,-1f)*velocityScaler, myRigidBody.velocity.y, 0f);

		}			
		
	}

	public virtual void GameMasterDestruction(){

	}
}
