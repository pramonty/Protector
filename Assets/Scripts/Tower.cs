using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : CustomParentClass {

	protected Mesh myMesh;
	//private Material myMaterial;
	//private Color originalColor;
	//private bool shouldChangeColor;
	protected int myHealth;
	protected GameMaster gameMaster;


	protected virtual void Awake ()
	{
		myHealth=1000;
	}


	void Start ()
	{
		//myMaterial=GetComponent<MeshRenderer>().material;
		//originalColor=myMaterial.color;
		//shouldChangeColor=false;
		gameMaster=FindObjectOfType<GameMaster>();

	}

	protected virtual void Update ()
	{
		/*if (shouldChangeColor) {
			shouldChangeColor = false;
			myMaterial.color = Color.red;
		} else {
			myMaterial.color = originalColor;
		}*/
		if (myHealth <= 0) {
			gameMaster.TowerDestroyed();
			Destroy(gameObject);
		}
	}

	protected virtual void OnTriggerEnter (Collider cldr)
	{
		if (cldr.gameObject.tag == "Laser") {
			//shouldChangeColor=true;
			DealDamage(5);
			Destroy(cldr.gameObject);
		}
	}
	protected virtual void OnCollisionEnter (Collision clsn)
	{
		if (clsn.gameObject.tag == "Laser") {
			//shouldChangeColor=true;
			DealDamage(5);
			Destroy(clsn.gameObject);
		}
	}

	public void DealDamage (int damage)
	{
		myHealth-=damage;
	}

	public int GetMyHealth(){
		return myHealth;
	}



}
