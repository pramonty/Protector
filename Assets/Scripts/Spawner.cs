using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public GameObject UFOPrefab;
	public GameObject StarShipPrefab;

	private bool starShipSpawned;

	// Use this for initialization
	void Start () {		
		starShipSpawned=false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		SpawnMaster.EnemyType enumReturned =SpawnMaster.ShouldSpawn(transform.position);
		if (enumReturned == SpawnMaster.EnemyType.UFO) {
			Instantiate(UFOPrefab,transform.position,UFOPrefab.transform.rotation);
		}else if(enumReturned == SpawnMaster.EnemyType.STARSHIP && !starShipSpawned){
			Invoke("SpawnStarShip",5f);
			starShipSpawned=true;
		}

	}

	void SpawnStarShip(){
		Instantiate(StarShipPrefab,transform.position,StarShipPrefab.transform.rotation);			
		starShipSpawned=false;
	}
}
