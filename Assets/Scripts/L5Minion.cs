using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L5Minion : Minions {

	public GameObject tank;
	

	protected override void OnTriggerEnter (Collider cldr)
	{
		base.OnTriggerEnter (cldr);
		if (cldr.gameObject.tag == "Arrow") {
			Destroy(cldr.gameObject);
			hitCount++;
		}
	}



	protected override void Fire(){
		for(int i=0;i<2;i++){
			GameObject laser = (GameObject)Instantiate (laserPrefab, transform.GetChild(i).position, laserPrefab.transform.rotation);
			laser.GetComponent<Rigidbody> ().velocity = new Vector3 (Mathf.Clamp(5f+myRigidBody.velocity.x,3,8), 0f, 0f);
			laser.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor",Color.red);
		}

	}	

	protected override void HandleDestruction ()
	{
		if (ShouldSpawnGrounEnemy()) {
			Instantiate (tank, transform.position, tank.transform.rotation);
		}
		DestroyMe();
	}

	void DestroyMe(){
		bnkr.IncrementMoney (destructionWorth);
		Instantiate (decimatedPrefab, transform.position, decimatedPrefab.transform.rotation);
		Destroy (gameObject);
	}

	bool ShouldSpawnGrounEnemy(){
		return (GameObject.FindGameObjectsWithTag("GroundEnemy").Length==0 && GameObject.FindGameObjectsWithTag("ShieldWall").Length==0)?true:false;
	}

	public override void GameMasterDestruction(){
		DestroyMe();
	}
}
