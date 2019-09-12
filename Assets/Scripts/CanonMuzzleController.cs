using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonMuzzleController : MonoBehaviour {

	public GameObject canonBall;
	public GameObject fireExplosionPrefab;
	public GameObject acidSplashPrefab;

	private bool targetLocked;
	private GameObject target;
	private Rigidbody myRgdBd;
	private Transform referencePointTransform;
	private bool positiveAngVel;
	private bool firedOnce;
	private bool commandedToDie;
	private int timesHit;

	// Use this for initialization
	void Start () {
		targetLocked=false;
		myRgdBd=GetComponent<Rigidbody>();
		myRgdBd.angularVelocity=new Vector3(0,0,0.5f);
		referencePointTransform=transform.GetChild(0).transform;
		positiveAngVel=true;
		firedOnce=false;
		commandedToDie=false;
		timesHit=0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!targetLocked) {
			target = GetEnemy ();
			if (target)
				targetLocked = true;
		}
		if ((transform.rotation.x <= -0.16f && positiveAngVel) || (transform.rotation.x >= 0.14f && !positiveAngVel)) {
			myRgdBd.angularVelocity = new Vector3 (0, 0, (positiveAngVel ? -0.5f : 0.5f));
			positiveAngVel = !positiveAngVel;
		}
		if (target && !commandedToDie) {
			Vector3 targetNormalized = (target.transform.position - transform.position).normalized;
			Vector3 referenceNormalized = (referencePointTransform.position - transform.position).normalized;
			if (InRange (targetNormalized, referenceNormalized) && !firedOnce) {
				Fire (referenceNormalized);
				firedOnce = true;
			}
		} else {
			targetLocked = false;
		}
		if (timesHit >= 10 && !commandedToDie) {
			commandedToDie=true;
			HandleDestruction();

		}

	}

	void Fire (Vector3 direction)
	{
		GameObject firedCanonBall=Instantiate(canonBall,referencePointTransform.position,Quaternion.identity);
		GameObject firedExplosion=Instantiate(fireExplosionPrefab,referencePointTransform.position+direction*1.25f,Quaternion.identity);
		ParticleSystem.MainModule firedModule=firedExplosion.GetComponent<ParticleSystem>().main;
		firedModule.startLifetime=0.2f;
		firedExplosion.transform.localScale=new Vector3(1.25f,0.25f,0.5f);
		float rotateBy = Vector3.Angle(new Vector3(1,0,0),direction);
		firedExplosion.transform.Rotate(new Vector3(0,0,rotateBy));
		firedCanonBall.GetComponent<Rigidbody>().velocity=direction*10;
	}

	bool InRange (Vector3 tgt, Vector3 rfrnc)
	{
		if (Mathf.Abs (tgt.x - rfrnc.x) < 0.1f && Mathf.Abs (tgt.y - rfrnc.y) < 0.1f && Mathf.Abs (tgt.z - rfrnc.z) < 0.1f) {
			return true;
		}
		firedOnce=false;
		return false;
	}

	GameObject GetEnemy ()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("UFOGO");
		if (enemies.Length != 0) {
			return enemies [0];
		} else {
			enemies = GameObject.FindGameObjectsWithTag ("GroundEnemy");
			if (enemies.Length != 0) {
				return enemies [0];
			} else {
				return GameObject.FindGameObjectWithTag("StarShip");
			}
		}
	}

	void OnTriggerEnter (Collider cldr)
	{
		if (cldr.gameObject.tag == "PotShot") {
			timesHit++;
			Instantiate(acidSplashPrefab,transform.position,acidSplashPrefab.transform.rotation);
		}
	}

	void HandleDestruction ()
	{
		GetEnemy().GetComponent<L3Boss>().SetCanonDemolished(true);
		for (int i = 0; i < transform.parent.childCount; i++) {
			GameObject sibling = transform.parent.GetChild (i).gameObject;
			if (sibling != gameObject) {
				sibling.AddComponent<Rigidbody> ().useGravity = true;
				sibling.AddComponent<BoxCollider> ();
			} else {
				myRgdBd.useGravity=true;
				GetComponent<BoxCollider>().isTrigger=false;
			}
		}
		Instantiate(fireExplosionPrefab,transform.position,Quaternion.identity).transform.localScale=new Vector3(0.75f,0.75f,0.75f);
		Destroy(transform.parent.gameObject,3);
	}
}
