using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L4Tower : Tower {

	protected override void Awake(){
		myHealth=2000;
	}

	protected override void OnTriggerEnter(Collider cldr){
		if (cldr.gameObject.tag == "Laser") {
			DealDamage(2);
			Destroy(cldr.gameObject);
		}
		if (cldr.gameObject.tag == "UFOGO") {
			DealDamage(30);
			StartCoroutine(DestroyUFO(cldr.gameObject,0.25f));
		}
	}

	IEnumerator DestroyUFO (GameObject obj, float delayTime)
	{
		yield return new WaitForSeconds (delayTime);
		if (obj) {
			L4Minnion enemyMinion = obj.GetComponent<L4Minnion> ();
			GameObject explsn = Instantiate (enemyMinion.decimatedPrefab, obj.transform.position, Quaternion.identity);
			explsn.transform.localScale = new Vector3 (4, 4, 4);
			enemyMinion.GetBanker ().IncrementMoney (enemyMinion.destructionWorth / 2);
			Destroy (obj);
		}
	}
}
