using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L2StarShip : MonoBehaviour {


	private static L2StarShip instance;
	private Rigidbody myRigidBody;
	private bool arrivedAndShouldDance;
	private float danceTime;
	private bool flameStarted;
	private GameMaster gameMaster;
	private bool destroyed;

	void Awake ()
	{
		if (instance == null) {
			instance=this;
		}else if(instance!=this){
			Destroy(gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		myRigidBody=GetComponent<Rigidbody>();
		myRigidBody.velocity=new Vector3(3,0,0);
		arrivedAndShouldDance=false;
		danceTime=0;
		flameStarted=false;
		gameMaster=FindObjectOfType<GameMaster>();
		destroyed=false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (transform.position.x >= -6.9 && !arrivedAndShouldDance && !destroyed) {
			arrivedAndShouldDance = true;
			myRigidBody.velocity = new Vector3 (0, -Random.Range (2, 5), 0);
			danceTime = Time.timeSinceLevelLoad;
			myRigidBody.angularVelocity=new Vector3(-1.2f,0,0);
		}

		if (StillIntactSpheres () == 0 && !destroyed) {
			arrivedAndShouldDance=false;
			myRigidBody.velocity=new Vector3(0,0,5);
			myRigidBody.angularVelocity=new Vector3(5,5,5);
			gameMaster.StarShipDestroyed();
			destroyed=true;
		}

		if (arrivedAndShouldDance) {
			Dance ();
			if (Time.timeSinceLevelLoad - danceTime > 10 && !flameStarted) {
				BroadcastMessage ("StartFlameThrower");
				flameStarted = true;
			}
		}
		if (!GameMaster.GetShieldHasArrived ()) {
			myRigidBody.velocity=new Vector3(-3,0,0);
			myRigidBody.angularVelocity=Vector3.zero;
			arrivedAndShouldDance=false;
			BroadcastMessage("StopFlameThrower");
		}
	}

	void Dance ()
	{
		
		if (transform.position.y <= 3) {
			float randVal = Random.Range (2, 5);
			myRigidBody.velocity = new Vector3 (0, randVal, 0);
			float angVel = (Mathf.PI / 3) / (4.3f / randVal);
			myRigidBody.angularVelocity = new Vector3 (angVel, 0, 0);
		}
		if (transform.position.y >= 7.3) {
			float randVal = Random.Range (2, 5);
			myRigidBody.velocity = new Vector3 (0, -randVal, 0);
			float angVel = (Mathf.PI / 3) / (4.3f / randVal);
			myRigidBody.angularVelocity = new Vector3 (-angVel, 0, 0);
		}


	}

	int StillIntactSpheres ()
	{
		int intactSpheres = 0;
		for (int i = 0; i < transform.childCount; i++) {
			GameObject thisChild = transform.GetChild (i).gameObject;
			if (thisChild.tag == "HeadSphere" || thisChild.tag == "SideSphere") {
				intactSpheres++;
			}
		}
		return intactSpheres;
	}

}
