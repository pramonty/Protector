using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMasterEvolved {

	public static int enemyToSpawn(int experience,Vector3 position){
		GameObject[] typeOneEnemies=GameObject.FindGameObjectsWithTag("UFOGO");
		int typeOneEnemiesCount=typeOneEnemies.Length;
		GameObject[] typeTwoEnemies=GameObject.FindGameObjectsWithTag("GroundEnemy");
		GameObject canon=GameObject.FindGameObjectWithTag("Canon");
		int typeTwoEnemiesCount=typeTwoEnemies.Length;
		int typeTwoEnemyThreshold=experience/2;
		if(canon && typeOneEnemiesCount==0 && typeTwoEnemiesCount==0)
			return 3;
		if(typeOneEnemiesCount<3 && !canon)
			return 1;
		if(typeTwoEnemiesCount<typeTwoEnemyThreshold && Time.timeSinceLevelLoad>10 && SatisfiesDistanceCondition(typeTwoEnemies,position) && !canon)
			return 2;
		return 0;
	}

	static bool SatisfiesDistanceCondition (GameObject[] typeTwoEnemies, Vector3 position)
	{
		float minXPosition = 0;
		foreach (GameObject enemy in typeTwoEnemies) {
			if (minXPosition > enemy.transform.position.x) {
				minXPosition=enemy.transform.position.x;
			}
		}
		if(Mathf.Abs(minXPosition-position.x)>2.5f)
			return true;
		else
			return false;
	}
}
