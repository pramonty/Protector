using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L2WallBehaviour : MonoBehaviour {

	void OnParticleCollision(GameObject GO){
		//transform.localScale=new Vector3(transform.localScale.x,transform.localScale.y-0.001f,transform.localScale.z);
		transform.localScale=0.999f*transform.localScale;
		transform.Translate(new Vector3(0,0.005f,0));
	}

	void Update ()
	{
		if (transform.localScale.y<=0.45f) {
			Destroy(gameObject);
		}
	}
}
