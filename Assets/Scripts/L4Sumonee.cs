using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L4Sumonee : CustomParentClass {

	private MeshRenderer tubeRenderer;
	private MeshRenderer cageRenderer;
	private Material tubeMaterial;
	private Material cageMaterial;
	private int hitCounter;

	void Start(){
		tubeRenderer=transform.GetChild(1).gameObject.GetComponent<MeshRenderer>();
		cageRenderer=transform.GetChild(0).gameObject.GetComponent<MeshRenderer>();
		cageMaterial=cageRenderer.material;
		tubeMaterial=tubeRenderer.material;
		hitCounter=0;
	}

	void Update ()
	{
		foreach (GameObject laser in GameObject.FindGameObjectsWithTag("Laser")) {
			Rigidbody laserRB=laser.GetComponent<Rigidbody>();
			float laserSpeed=laserRB.velocity.magnitude;
			laserRB.velocity=(transform.position-laser.transform.position).normalized*laserSpeed;
		}
	}


	void OnTriggerEnter (Collider cldr)
	{
		if (cldr.gameObject.tag == "Laser") {
			Destroy(cldr.gameObject);
		}
	}

	void OnParticleCollision (GameObject ps)
	{
		hitCounter++;
		if (hitCounter % 25 == 0) {
			Color updatedTubeColour = tubeMaterial.GetColor ("_EmissionColor") * 0.75f;
			Color updatedCageColor = cageMaterial.color;
			updatedCageColor.a *= 0.9f;
			tubeMaterial.SetColor ("_EmissionColor", updatedTubeColour);
			cageMaterial.color = updatedCageColor;
			tubeRenderer.UpdateGIMaterials ();
			cageRenderer.UpdateGIMaterials ();
			if (cageMaterial.color.a <= 0.2) {
				Destroy(gameObject);
			}
		}


	}
}
