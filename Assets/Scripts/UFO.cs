using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UFO : Minions {

	protected override void Start(){
		base.Start();
		upperBound=towerObjective.transform.position.y+towerObjective.GetComponent<MeshRenderer>().bounds.size.y;
		lowerBound=towerObjective.transform.position.y-towerObjective.GetComponent<MeshRenderer>().bounds.size.y;
	}

	public override void GameMasterDestruction(){
		HandleDestruction();
	}
}
