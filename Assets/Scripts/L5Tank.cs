using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L5Tank : Minions {

	public GameObject muzzleBlast;
	public GameObject shellAmmo;
	//public int destructionWorth;

	private Rigidbody myRgdBd;
	private GameObject targetWT;
	private GameObject hitPS;
	//private Banker bnkr;
	private Animator myAnim;
	private bool reachedTarget;
	private bool facedTarget;
	private bool dying;
	private float tankTopRotateVal;
	private float turnAngle;
	private float timeSinceLastFired;
	private Transform tankTopTransform;
	private Transform muzzle;
	private Transform muzzleFirePoint;
	private Vector3 forwardVelocity;
	private int hitCounter;
	private int maceInstanceId;
	private float etaToReach;
	private float multiplyFactor;
	private float muzzleVerticalRotationAmount;
	private float firingInterval;


	// Use this for initialization
	protected override void Start () {
		bnkr=GameObject.FindObjectOfType<Banker>();
		myRgdBd=GetComponent<Rigidbody>();
		myAnim=GetComponent<Animator>();
		muzzleFirePoint=transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0);
		tankTopTransform = transform.GetChild (0).GetChild (0);
		muzzle=tankTopTransform.GetChild(0);
		hitPS=transform.GetChild(5).gameObject;
		reachedTarget=false;
		facedTarget=false;
		tankTopRotateVal=0;
		timeSinceLastFired=0;
		hitCounter=0;
		maceInstanceId=0;
		firingInterval=5;
		forwardVelocity=new Vector3(2.64f,0,0);
		float distanceFromGround=Mathf.Abs(transform.position.y-GameObject.Find("GroundReference").transform.position.y)/2;
		etaToReach=Mathf.Sqrt(2*(distanceFromGround)/Mathf.Abs(Physics.gravity.y));
		transform.localScale=transform.localScale.normalized*0.25f;
		multiplyFactor=Mathf.Pow(10,((Mathf.Log10(4))/etaToReach));
		dying=false;

	}

	
	// Update is called once per frame
	protected override void Update ()
	{

		if (transform.localScale.x < 1) {
			float val=Mathf.Pow(10,Mathf.Log10(multiplyFactor)*Time.deltaTime);
			transform.localScale*=val;
		}

		if (targetWT) {
			if (transform.position.x >= targetWT.transform.position.x && !reachedTarget && targetWT.tag != "Tower") {
				myAnim.SetBool ("Idle", true);
				myRgdBd.velocity = Vector3.zero;
				reachedTarget = true;
				Vector2 nrmlzdTrgtDrctn = (new Vector2 (targetWT.transform.position.x, targetWT.transform.position.z) 
											- new Vector2 (transform.position.x, transform.position.z)).normalized;
				Vector2 tankTopDirection = new Vector2 (1, 0);
				turnAngle = Vector2.Angle (tankTopDirection, nrmlzdTrgtDrctn);
				muzzleVerticalRotationAmount=12;
				tankTopRotateVal = 0;
				InvokeRepeating ("MuzzleRotation", 0, 0.005f);

			} else if (targetWT.tag == "Tower" && !reachedTarget) {
				reachedTarget=true;
				facedTarget=true;
				myAnim.SetBool ("Idle", true);
				myRgdBd.velocity=Vector3.zero;
				muzzle.Rotate(new Vector3(-12,0,0));
			}
		}else if(reachedTarget) {
				reachedTarget=false;
				facedTarget=false;
				Vector2 nrmlzdMuzzleDirection=(new Vector2(muzzleFirePoint.position.x,muzzleFirePoint.position.z)
										      - new Vector2(tankTopTransform.position.x,tankTopTransform.position.z)).normalized;
				turnAngle=Vector2.Angle(new Vector2(1,0),nrmlzdMuzzleDirection);
				Vector3 muzzleDirection=(muzzleFirePoint.position-transform.position).normalized;
				muzzleVerticalRotationAmount=Vector3.Angle(muzzleDirection,Vector3.ProjectOnPlane(muzzleDirection,new Vector3(0,1,0)).normalized);
				muzzleVerticalRotationAmount*=(12/19.18f);
				turnAngle=-turnAngle;
				tankTopRotateVal=0;
				InvokeRepeating ("MuzzleRotation", 0, 0.005f);
			}
		if (facedTarget && Time.timeSinceLevelLoad-timeSinceLastFired >=firingInterval) {
			Vector3 shellFireDirection=(muzzleFirePoint.position-muzzle.position).normalized;
			GameObject muzzleBlastExplosion=Instantiate(muzzleBlast,muzzleFirePoint.position,Quaternion.identity);
			GameObject firedShell=Instantiate(shellAmmo,muzzleFirePoint.transform.position,shellAmmo.transform.rotation);
			firedShell.GetComponent<Rigidbody>().velocity=shellFireDirection*10;
			firedShell.transform.rotation=Quaternion.LookRotation(shellFireDirection);
			muzzleBlastExplosion.transform.localScale=new Vector3(0.5f,0.5f,0.5f);
			timeSinceLastFired=Time.timeSinceLevelLoad;
		}
		
	}



	void MuzzleRotation ()
	{
		float rotateAmount = turnAngle > 0 ? -0.25f : 0.25f;
		tankTopTransform.Rotate (new Vector3 (0, 0, rotateAmount));
		muzzle.Rotate (new Vector3 (-(muzzleVerticalRotationAmount / (turnAngle / 0.25f)), 0, 0));
		tankTopRotateVal += 0.25f;
		if (tankTopRotateVal >= Mathf.Abs (turnAngle)) {
			if (turnAngle > 0) {
				facedTarget = true;
				timeSinceLastFired = Time.timeSinceLevelLoad;
			} else {	
				targetWT=FindTarget();
				myAnim.SetBool("Idle",false);
				myRgdBd.velocity=forwardVelocity;
			}
				Vector3 muzzleDirection=(muzzleFirePoint.position-transform.position).normalized;
				muzzleVerticalRotationAmount=Vector3.Angle(muzzleDirection,Vector3.ProjectOnPlane(muzzleDirection,new Vector3(0,1,0)).normalized);
			CancelInvoke();
		}
	}

	GameObject FindTarget ()
	{
		GameObject[] turbines = GameObject.FindGameObjectsWithTag ("Turbine");
		float previousDistance = 100f;
		GameObject target=null;
		foreach (GameObject turb in turbines) {
			float diff=Mathf.Abs (turb.transform.position.x - transform.position.x);
			if (diff <= previousDistance && turb.transform.position.x > transform.position.x) {
				previousDistance=diff;
				target=turb;
			}
		}

		if(!target){
			target=GameObject.FindGameObjectWithTag("Tower");
		}
		return target;
	}

	protected override void OnTriggerEnter (Collider cldr)
	{
		if (cldr.gameObject.tag == "Landscape" && !reachedTarget) {
			myRgdBd.useGravity = false;
			myRgdBd.velocity = forwardVelocity;
			targetWT = FindTarget ();
		}

		if (cldr.gameObject.tag == "ShieldWall") {
			Death();
		}

		if (cldr.gameObject.tag == "MC") {
			if (maceInstanceId != cldr.gameObject.GetInstanceID ()) {
				maceInstanceId = cldr.gameObject.GetInstanceID ();
				hitCounter++;
				firingInterval+=2.5f;
			}
			Destroy (cldr.gameObject);
			switch (hitCounter) {
			case 1:
				hitPS.GetComponent<ParticleSystem> ().Play ();
				break;
			case 2:
				ParticleSystem.EmissionModule mdl = hitPS.GetComponent<ParticleSystem> ().emission;
				mdl.rateOverTime = 25;
				break;
			case 3:
				Death();
				break;
			}
		}
	}

	void Death ()
	{
		if (!dying) {
			dying=true;
			DestroyMe();
		}
	}

	void DestroyMe(){
			GameObject explode = Instantiate (muzzleBlast, transform.position, Quaternion.identity);
			explode.transform.localScale = new Vector3 (2, 2, 2);
			bnkr.IncrementMoney (destructionWorth);
			Destroy (gameObject);
	}

	public override void GameMasterDestruction(){
		DestroyMe();
	}
}
