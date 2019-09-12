using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : MonoBehaviour {

	public Mesh decimatedMesh;

	void OnCollisionEnter (Collision clsn)
	{
		if (clsn.gameObject.tag == "Trident") {
			Destroy(clsn.gameObject);
			GetComponent<MeshFilter>().mesh=decimatedMesh;
			gameObject.AddComponent<Shatterer>().Shatter();
		}
	}
}
