using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarShip : LevelBoss {

	public GameObject missilePrefab;
	public static StarShip instance;
	public float maxX;
	public float maxY;
	public float minY;

	protected Rigidbody myRgdbd;
	protected bool arrivedAndShouldDance;
	protected int myHealth;
	protected float lastSpawnTime;
	protected Vector3 gravityBeforeSpawn;
	protected bool deathStroke;
	protected GameMaster gameMaster;
	//private bool shouldFire;

	void Awake ()
	{
		if (instance == null) {
			instance=this;
		}else if(instance!=this){
			Destroy(gameObject);
		}
	}

	// Use this for initialization
	protected virtual void Start () {
		gameMaster=FindObjectOfType<GameMaster>();
		myHealth=((gameMaster.GetBossHealth()==-1)?1000:gameMaster.GetBossHealth());
		myRgdbd=GetComponent<Rigidbody>();
		myRgdbd.velocity=new Vector3(3,0,0);
		arrivedAndShouldDance=false;
		lastSpawnTime=Time.timeSinceLevelLoad;
		deathStroke=false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (transform.position.x >= maxX && !arrivedAndShouldDance && !deathStroke) {
			myRgdbd.velocity = new Vector3 (0, Random.Range (2, 5), 0);
			arrivedAndShouldDance = true;
			SetSpawnTime();
		}
		if (arrivedAndShouldDance) {
			Dance ();
		}

		FireMissile();

		if (myHealth <= 0) {
			gameMaster.StarShipDestroyed();
			Destroy(gameObject);
		}

		if (!GameMaster.GetShieldHasArrived () && !deathStroke) {
			arrivedAndShouldDance=false;
			myRgdbd.velocity=new Vector3(-3,0,0);
			deathStroke=true;
			AdditionalUseCases();
		}

	}

	protected virtual void FireMissile ()
	{
		if ((Time.timeSinceLevelLoad - lastSpawnTime >= 10) && myRgdbd.velocity.y>0) {
			Instantiate (missilePrefab, transform.position, missilePrefab.transform.rotation);
			lastSpawnTime = Time.timeSinceLevelLoad;
		}
	}

	protected virtual void SetSpawnTime ()
	{
	}

	protected virtual void AdditionalUseCases ()
	{
		
	}

	void LateUpdate ()
	{
		/*if (!GameMaster.GetShieldHasArrived () && !deathStroke) {
			arrivedAndShouldDance=false;
			myRgdbd.velocity=new Vector3(-3,0,0);
			deathStroke=true;
			AdditionalUseCases();
		}*/
	}



	void Dance ()
	{
		if (transform.position.y > maxY) {
			myRgdbd.velocity = new Vector3 (0, -Random.Range (2, 5), 0);
		} else if (transform.position.y < minY) {
			myRgdbd.velocity = new Vector3 (0, Random.Range (2, 5), 0);
		}
	}

	protected virtual void OnTriggerEnter (Collider cldr)
	{
		if (cldr.tag == "Arrow") {
			Destroy(cldr.gameObject);
			myHealth-=20;
		}
	}

	public override int GetMyHealth(){
		return this.myHealth;
	}
}
