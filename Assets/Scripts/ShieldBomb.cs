using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBomb : CustomParentClass {

	public GameObject appearSmoke;
	public GameObject shieldWallPrefab;
	public Vector3 pointOfInception;
	public Vector3 smokeInceptionPoint;
	public float instantiateZPosition;
	public float waitTime;
	public float smokeScaleValue;


	private bool commandedToCreate;
	private MessageHandler gameMessageHandler;
	protected Rigidbody myRgdBd;
	private bool setTowardsTarget;

	//Params for Testing spiral movement
	private Vector3 radialDirection;
	protected Vector3 myLastVel;


	// Use this for initialization
	protected virtual void Start () {
		//pointOfInception=new Vector3(5.24f,-3.3f,14.77f);
		commandedToCreate=false;
		gameMessageHandler=FindObjectOfType<MessageHandler>();
		myRgdBd=GetComponent<Rigidbody>();
		setTowardsTarget=false;

		//Stuff for spiral movement testing
		myLastVel=GetComponent<Rigidbody>().velocity;
		radialDirection=pointOfInception-transform.position;
	}

	protected virtual void Update ()
	{
	}
	
	// Update is called once per frame
	protected virtual void FixedUpdate ()
	{

		if (FirstEntryCondition ()) {
			if (GameObject.FindGameObjectWithTag ("ShieldWall")) {
				gameMessageHandler.SetMessage ("ShieldWall already present. Stop wasting Coins");
				Destroy (gameObject);
			} else if (!commandedToCreate) {
				if (appearSmoke) {
					Destroy(GetComponent<MeshFilter>());
					Instantiate (appearSmoke, smokeInceptionPoint, Quaternion.identity).transform.localScale = new Vector3 (1, 1, 1) * smokeScaleValue;
				}
				Invoke ("CreateAndDie", waitTime);
				myRgdBd.velocity=Vector3.zero;
				myLastVel=Vector3.zero;
				commandedToCreate = true;
			}
		}


		if (transform.position.z >= instantiateZPosition && !setTowardsTarget) {
			radialDirection = pointOfInception - transform.position;
			myRgdBd.velocity = radialDirection.normalized * 25;
			myLastVel=myRgdBd.velocity;
			setTowardsTarget = true;
		}

		if (setTowardsTarget && !commandedToCreate) {
			VelocityReduction();

		}

	}

	protected virtual void VelocityReduction ()
	{
			float reductionFactor=Mathf.Pow(10,Time.deltaTime*Mathf.Log10(2));
			myRgdBd.velocity=myLastVel/reductionFactor;
			myLastVel=myRgdBd.velocity;
	}

	protected virtual bool FirstEntryCondition(){
		   return ((transform.position-pointOfInception).magnitude<=0.5f && !commandedToCreate);
		 //return (transform.position.z >= instantiateZPosition && !commandedToCreate);
	}

	void CreateAndDie ()
	{	
			Instantiate(shieldWallPrefab,pointOfInception,shieldWallPrefab.transform.rotation);
			Destroy(gameObject);
	}
}
