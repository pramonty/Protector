using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kiwi : Minions {

	//public int destructionWorth;
	public int hitThreshold;
	public float towerAdvanceThreshold;
	public GameObject featherPS;

	//private GameObject towerObjective;
	//private Rigidbody myRigidBody;
	private bool hasBeenLaunched;
	private Animator myAnim;
	private Tower levelTower;
	//private Banker bnkr;
	//private int hitCount;
	private bool hasReached;
	private bool forwardAdvanceTakenCareOf;
	private bool inCntcWthTwr;
	// Use this for initialization
	protected override void Start () {
		towerObjective=GameObject.FindGameObjectWithTag("Tower");
		bnkr=GameObject.FindObjectOfType<Banker>();
		myRigidBody=GetComponent<Rigidbody>();
		hasBeenLaunched=false;
		myAnim=GetComponent<Animator>();
		levelTower=GameObject.FindObjectOfType<Tower>();
		hitCount=0;
		hasReached=false;
		forwardAdvanceTakenCareOf=false;
		inCntcWthTwr=false;
	}


	protected override void Update ()
	{
		if (transform.position.x >= towerAdvanceThreshold && !forwardAdvanceTakenCareOf) {
			myRigidBody.velocity = new Vector3 (0, myRigidBody.velocity.y, 0);
			forwardAdvanceTakenCareOf = true;
		}

		if (!towerObjective) {
			myAnim.SetTrigger("PeckToWalk");
			hasReached=false;
			inCntcWthTwr=false;
			myRigidBody.constraints=RigidbodyConstraints.FreezeRotation|RigidbodyConstraints.FreezePositionZ;
		}
	}
	
	public void JumpVelocity(float velocity){
		myRigidBody.velocity=new Vector3((hasReached?0:1),velocity*Random.Range(1,2.2f),0);

	}

	protected override void OnTriggerEnter (Collider cldr)
	{
		if (cldr.gameObject.tag == "Arrow" && !hasBeenLaunched) {
			myAnim.SetTrigger ("Jump");
			hasBeenLaunched = true;
		}
		if (cldr.gameObject.tag == "Tower") {
			if (!hasReached) {
				Invoke ("ManagePecking", 0);
			}
			inCntcWthTwr = true;
		}
		if(cldr.gameObject.transform.parent){
		if (cldr.gameObject.transform.parent.gameObject.tag == "Canon") {
			DestroyMe();
		}
		}
	}

	void OnTriggerExit (Collider cldr)
	{
		if (cldr.gameObject.tag == "Tower") {
			inCntcWthTwr=false;
		}
	}

	void ManagePecking(){
		myRigidBody.velocity=Vector3.zero;
		myRigidBody.constraints=RigidbodyConstraints.FreezePositionX|RigidbodyConstraints.FreezeRotation|RigidbodyConstraints.FreezePositionZ;
		myAnim.SetTrigger("Peck");
		myAnim.SetBool("hasReached",true);
		hasReached=true;
	}

	void PeckingDamage (int damage)
	{
		if (inCntcWthTwr) {
			levelTower.DealDamage (damage);
		}
	}

	public void MoveForward (float velocity)
	{
		if (!hasBeenLaunched) {
			myRigidBody.velocity = new Vector3 (velocity, 0, 0);
		}
	}

	public void StopMoving ()
	{
		if (!hasBeenLaunched) {
			myRigidBody.velocity = Vector3.zero;
		}
	}

	void OnCollisionEnter (Collision clsn)
	{
		if (clsn.gameObject.tag == "Landscape" && hasBeenLaunched) {
			hasBeenLaunched = false;
		}
		if (clsn.gameObject.tag == "Arrow") {
			Destroy (clsn.gameObject);
			hitCount++;
			if (hitCount >= hitThreshold) {
				DestroyMe();
			}
		}
		if (clsn.gameObject.tag == "CanonBall") {
			Destroy (clsn.gameObject);
			DestroyMe();
		}

	}

	void DestroyMe(){
		Instantiate(featherPS,transform.position,Quaternion.identity);
		bnkr.IncrementMoney (destructionWorth);
		Destroy(gameObject);
	}

	public override void GameMasterDestruction(){
		DestroyMe();
	}

}
