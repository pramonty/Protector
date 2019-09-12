using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L4FireEvasiveShots : MonoBehaviour {

	public GameObject flare;

	private float flareSpeed;
	private bool lensIsThere;
	private GameObject sloMoLens;

	// Use this for initialization
	void Start () {
		flareSpeed=30;
		lensIsThere=false;
	}
	
	// Update is called once per frame
	void Update ()
	{

		foreach (GameObject arrow in GameObject.FindGameObjectsWithTag("Arrow")) {
			bool inSomeOther = false;
			foreach (GameObject div in GameObject.FindGameObjectsWithTag("Flare")) {
				if (div.GetComponent<L4Flares> ().GetTarget ().GetInstanceID () == arrow.GetInstanceID ()) {
					inSomeOther = true;
				}
			}
			if (!inSomeOther) {
				GameObject flr = Instantiate (flare, transform.position, flare.transform.rotation);
				flr.GetComponent<L4Flares> ().SetTarget (arrow);
				flr.GetComponent<L4Flares> ().SetSpeed (flareSpeed);
				flr.GetComponent<Rigidbody> ().angularVelocity = new Vector3 (0, 5, 0);
			}

		}

		if (lensIsThere) {
			if (!sloMoLens) {
				sloMoLens=GameObject.FindGameObjectWithTag("SloMoLens");
			}
			Color tempColor=sloMoLens.GetComponent<MeshRenderer>().material.color;
			tempColor.a-=0.04f*Time.deltaTime;
			sloMoLens.GetComponent<MeshRenderer>().material.color=tempColor;
		}
			
		
	}

	void HandleTimeGrenade(){
		flareSpeed=1;
		lensIsThere=true;
		Invoke("NegateTimeGrenade",10);
	}

	void NegateTimeGrenade(){
		lensIsThere=false;
		Destroy(sloMoLens);
		flareSpeed=30;
	}
}
