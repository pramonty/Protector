using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondHeadController : MonoBehaviour {

	private bool redToBlue;

	void Start(){
		GetComponent<MeshRenderer>().material.color=Color.red;
		GetComponent<MeshRenderer>().material.SetColor("_EmissionColor",Color.red);
		redToBlue=true;
	}
	
	
	// Update is called once per frame
	void Update ()
	{
		Color mainColor = GetComponent<MeshRenderer> ().material.color;
		Color emissionColor = GetComponent<MeshRenderer> ().material.GetColor ("_EmissionColor");	
		if (redToBlue) {
			float mcr=mainColor.r;
			float mcb=mainColor.b;
			float ecr=emissionColor.r;
			float ecb=emissionColor.b;
			mainColor = new Color (mcr - 1 * Time.deltaTime, 0, mcb + 1 * Time.deltaTime);
			emissionColor = new Color (ecr - 1 * Time.deltaTime, 0, ecb + 1 * Time.deltaTime);
			GetComponent<MeshRenderer> ().material.color = mainColor;
			GetComponent<MeshRenderer> ().material.SetColor ("_EmissionColor",emissionColor);
			redToBlue = (mainColor.r <= 0) ? false : true;
		} else {
			float mcr=mainColor.r;
			float mcb=mainColor.b;
			float ecr=emissionColor.r;
			float ecb=emissionColor.b;
			mainColor = new Color (mcr + 1 * Time.deltaTime, 0, mcb - 1 * Time.deltaTime);
			emissionColor = new Color (ecr + 1 * Time.deltaTime, 0, ecb -1* Time.deltaTime);
			GetComponent<MeshRenderer> ().material.color = mainColor;
			GetComponent<MeshRenderer> ().material.SetColor ("_EmissionColor",emissionColor);
			redToBlue = (mainColor.b <= 0) ? true : false;
		}
	}

	void OnTriggerEnter (Collider cldr)
	{
		if (cldr.gameObject.tag == "Mace") {
			SendMessageUpwards("DiamondHeadDestroyed");
			GetComponent<Shatterer>().Shatter();
		}
	}
}
