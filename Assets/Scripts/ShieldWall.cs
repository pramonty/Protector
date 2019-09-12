using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldWall : MonoBehaviour {

	public static ShieldWall instance;
	public GameObject brokenPrefab;
	public GameObject explosion;
	private float scaleVal;
	private bool targetReached;

	void Awake ()
	{
		if (instance == null) {
			instance=this;
		}else if(instance!=this){
			Destroy(gameObject);
		}
	}

	void Start(){
		scaleVal=0.25f;
		transform.localScale=new Vector3(scaleVal,scaleVal,scaleVal);
		targetReached=false;
	}

	void Update ()
	{
		if (!targetReached) {
			scaleVal+=(0.375f*Time.deltaTime);
			transform.localScale=new Vector3(scaleVal,scaleVal,scaleVal);
			targetReached=scaleVal>=1?true:false;
		}
	}

	void OnTriggerEnter (Collider cldr)
	{
		if (cldr.gameObject.tag == "Missile") {
			Destroy (cldr.gameObject.transform.parent.gameObject);
			Instantiate(brokenPrefab,transform.position,transform.rotation);
			GameObject explosionGO= (GameObject)Instantiate(explosion,transform.position,Quaternion.identity);
			explosionGO.transform.localScale=new Vector3(4,4,4);
			Destroy(gameObject);
			//myShatterer.Shatter ();
		} /*else if (cldr.gameObject.tag == "Arrow") {
			Destroy(cldr.gameObject);
		}*/
	}
}
