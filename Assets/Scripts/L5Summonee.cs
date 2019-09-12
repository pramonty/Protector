using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L5Summonee : MonoBehaviour {

	private MeshRenderer myMeshRndr;
	private bool downhill;
	private float magnitudeTracker;
	private Rigidbody myRgdBd;
	private bool stopped;

	// Use this for initialization
	void Start () {
		myMeshRndr=GetComponent<MeshRenderer>();
		downhill=true;
		magnitudeTracker=1;
		myRgdBd=GetComponent<Rigidbody>();
		stopped=false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (downhill) {
			HandleColorPulse (0.25f);
			downhill = (magnitudeTracker <= 0.1f) ? false : true;
		} else {
			HandleColorPulse (4);
			downhill = (magnitudeTracker >= 2.5f) ? true : false;
		}

		foreach (GameObject laser in GameObject.FindGameObjectsWithTag("Laser")) {
			Vector3 direction=(transform.position-laser.transform.position).normalized;
			laser.GetComponent<Rigidbody>().velocity=laser.GetComponent<Rigidbody>().velocity.magnitude*direction;
		}

	}

	void HandleColorPulse (float changeAmount)
	{
			Color emissionColor = myMeshRndr.material.GetColor ("_EmissionColor");
			float decrementAmount = Mathf.Pow (10, (Mathf.Log (changeAmount) * Time.deltaTime));
			emissionColor *= decrementAmount;
			magnitudeTracker *= decrementAmount;
			myMeshRndr.material.SetColor ("_EmissionColor", emissionColor);
	}

	void OnTriggerEnter (Collider cldr)
	{
		if (cldr.gameObject.tag == "Arrow") {
			Destroy (cldr.gameObject);
		}
		if (cldr.gameObject.tag == "Laser") {
			StartCoroutine(DestroyLaser(cldr.gameObject));
		}
		if (cldr.gameObject.tag == "Landscape" && !stopped) {
			myRgdBd.useGravity=false;
			myRgdBd.velocity=Vector3.zero;
			stopped=true;
		}
	}

	IEnumerator DestroyLaser(GameObject lsr){
		yield return new WaitForSeconds(1.5f);
		Destroy(lsr);
	}

}
