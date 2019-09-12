using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEvolved : MonoBehaviour {

	public GameObject[] enemies;

	private Experience exp;

	// Use this for initialization
	void Start () {
		exp=GameObject.FindObjectOfType<Experience>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		if (SpawnMasterEvolved.enemyToSpawn (exp.GetExperience(),transform.position) == 1 && !GameMaster.GetGameOver()) {
			Instantiate(enemies[0],transform.position,enemies[0].transform.rotation);
		}
		if (SpawnMasterEvolved.enemyToSpawn (exp.GetExperience(),transform.position) == 2 && !GameMaster.GetGameOver()) {
			Instantiate(enemies[1],transform.position,enemies[1].transform.rotation);
		}
		if (SpawnMasterEvolved.enemyToSpawn (exp.GetExperience(),transform.position) == 3 && !GameMaster.GetGameOver()) {
			Instantiate(enemies[2],transform.position-new Vector3(5,0,0),enemies[2].transform.rotation);
		}
		
	}
}
