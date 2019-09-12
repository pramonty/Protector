using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L3Boss : LevelBoss {

	public float forwardVelocity;
	public GameObject fireShotPrefab;

	private static L3Boss singletonInstance;
	private GameMaster gmMstr;
	private Rigidbody myRgdBd;
	private Animator myAnim;
	private bool initialGravityHandled;
	private bool arrivedAtLocation;
	private GameObject trgtCnn;
	private bool reverseWalkInitiated;
	private bool fireTargetDecider;
	private int myHealth;
	private bool canonDemolished;


	void Awake ()
	{
		if (singletonInstance == null) {
			singletonInstance = this;
			gmMstr=GameObject.FindObjectOfType<GameMaster>();
			myHealth=gmMstr.GetBossHealth()==-1?1000:gmMstr.GetBossHealth();
		} else if(singletonInstance!=this){
			Destroy(gameObject);
		}
	}
	// Use this for initialization
	void Start () {
		myRgdBd=GetComponent<Rigidbody>();
		myAnim=GetComponent<Animator>();
		trgtCnn=GameObject.FindGameObjectWithTag("Canon");
		initialGravityHandled=false;
		arrivedAtLocation=false;
		reverseWalkInitiated=false;
		fireTargetDecider=true;
		canonDemolished=false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (transform.position.y <= -3.76f && !initialGravityHandled) {
			myRgdBd.useGravity = false;
			myRgdBd.velocity = new Vector3 (forwardVelocity, 0, 0);
			initialGravityHandled = true;
		}
		if (transform.position.x >= -6.8f && !arrivedAtLocation) {
			myRgdBd.velocity = Vector3.zero;
			myAnim.SetTrigger ("Idle");
			arrivedAtLocation = true;
		}
		if (canonDemolished && !reverseWalkInitiated) {
			myAnim.SetTrigger ("ReverseWalk");
			myRgdBd.velocity = new Vector3 (-forwardVelocity, 0, 0);
			reverseWalkInitiated = true;
		}
		if (myHealth <= 0) {
			gmMstr.StarShipDestroyed();
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter (Collider cldr)
	{
		if (cldr.gameObject.tag == "Arrow") {
			myHealth-=10;
			Destroy(cldr.gameObject);
		}
		if (cldr.gameObject.tag == "CanonBall") {
			Destroy(cldr.gameObject);
		}
	}
	void Fire ()
	{
		
		GameObject fireTarget = (fireTargetDecider && !canonDemolished) ? trgtCnn : GameObject.FindGameObjectWithTag ("Tower");
		if (fireTarget) {
			GameObject fireShot = Instantiate (fireShotPrefab, transform.position + new Vector3 (5.3f, 1.42f, 0), Quaternion.identity);
			float xVelocity = 5;
			float timeTakenToReachTarget = Mathf.Abs (fireShot.transform.position.x - fireTarget.transform.position.x) / xVelocity;
			float yTarget = fireTarget.transform.position.y - fireShot.transform.position.y;
			float acclDueToGravity = Physics.gravity.y;
			float initialYVelocity = (yTarget - 0.5f * acclDueToGravity * Mathf.Pow (timeTakenToReachTarget, 2)) / timeTakenToReachTarget;
			fireShot.GetComponent<Rigidbody> ().velocity = new Vector3 (xVelocity, initialYVelocity, 0);
			fireShot.GetComponent<Rigidbody> ().useGravity = true;
		}
		fireTargetDecider=!fireTargetDecider;
	}

	public void SetCanonDemolished (bool val)
	{
		canonDemolished=val;
	}

	public override int GetMyHealth(){
		return myHealth;
	}
}
