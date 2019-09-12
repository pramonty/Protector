using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L2Minion : Minions {

	protected override void Start(){
		base.Start();
		upperBound=towerObjective.transform.position.y+towerObjective.GetComponent<MeshRenderer>().bounds.size.y;
		lowerBound=towerObjective.transform.position.y-towerObjective.GetComponent<MeshRenderer>().bounds.size.y;
	}

	protected override void Fire(){
		GameObject laser = (GameObject)Instantiate (laserPrefab, transform.position, laserPrefab.transform.rotation);
		laser.GetComponent<Rigidbody> ().velocity = new Vector3 (Mathf.Clamp(5f+myRigidBody.velocity.x,3,8), 0f, 0f);
		laser.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor",Color.yellow);
	}

	public override void GameMasterDestruction(){
		HandleDestruction();
	}
}
