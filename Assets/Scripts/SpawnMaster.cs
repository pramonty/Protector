using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMaster {

	public enum EnemyType{UFO,STARSHIP,NONE};

	public static EnemyType ShouldSpawn (Vector3 spawnerPosition)
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("UFOGO");
		int enemiesInLane = 0;
		Vector3 minXWalaEnemy = Vector3.zero;
		bool noEnemyPresent = true;

		foreach (GameObject enemy in enemies) {
			noEnemyPresent = false;
			//Vector3 enemyPosition = enemy.transform.position;
			//if (IsApproximately (enemyPosition.z, spawnerPosition.z, 0.1f)) {
				enemiesInLane++;
			//}
		}

		if (!GameMaster.GetGameOver ()) {
			if ((enemiesInLane < 5 || noEnemyPresent) && !GameMaster.GetShieldHasArrived ()) {
				return EnemyType.UFO;
			} else if (GameMaster.GetShieldHasArrived ()) {
				if (GameObject.FindGameObjectsWithTag ("StarShip").Length < 1 && enemiesInLane == 0) {	
					return EnemyType.STARSHIP;
				} else {
					return EnemyType.NONE;
				}
			} else {
				return EnemyType.NONE;
			}
		}else{
		return EnemyType.NONE;
	}
	}

	private static bool IsApproximately(float a,float b,float tolerance){
		if(Mathf.Abs(a-b)<tolerance) return true;
		else return false;
	}
}
