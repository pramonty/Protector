using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L5BossArsenal : MonoBehaviour {

	private static GameObject myInstance;
	private GameObject targetShieldWall;
	private Rigidbody myRgdBd;
	private ParticleSystem myPS;
	private bool goneBerserk;

	void Awake ()
	{
		if (myInstance) {
			Destroy (gameObject);
		} else {
			myInstance=gameObject;
		}
	}

	// Use this for initialization
	void Start () {
		targetShieldWall=GameObject.FindGameObjectWithTag("ShieldWall");
		myRgdBd=GetComponent<Rigidbody>();
		myPS=transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
		myRgdBd.velocity=(targetShieldWall.transform.position-transform.position).normalized*2;
		goneBerserk=false;

	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Mathf.Abs (transform.position.x - targetShieldWall.transform.position.x) <= 0.1f && !goneBerserk) {
			GoBerserk();
			goneBerserk=true;
		}
	}

	void GoBerserk ()
	{
		ParticleSystem.Burst burst=new ParticleSystem.Burst();
		burst.cycleCount=0;
		burst.count=30;
		myPS.emission.SetBurst(0,burst);
		myRgdBd.velocity=Vector3.zero;
		StartCoroutine(Die());
	}

	IEnumerator Die(){
	 	yield return new WaitForSeconds(1.25f);
	 	Destroy(targetShieldWall);
	 	Destroy(gameObject);
	}
}
