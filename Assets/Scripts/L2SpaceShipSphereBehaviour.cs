using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L2SpaceShipSphereBehaviour : MonoBehaviour {

	public Material iceMaterial;
	public Mesh decimatedMesh;
	public GameObject puffEffect;

	private MeshRenderer myMeshRenderer;
	private bool readyToBeDestroyed;
	private ParticleSystem flameThrower;
	private MessageHandler levelsMessageHandler;

	void Awake ()
	{
		if (transform.childCount>0) {
			if (transform.GetChild (0).gameObject.tag == "FlameThrower") {
				flameThrower = transform.GetChild (0).gameObject.GetComponent<ParticleSystem> ();
				flameThrower.Stop();
			}
		}
	}

	void Start ()
	{
		myMeshRenderer = GetComponent<MeshRenderer> ();
		readyToBeDestroyed = false;
		levelsMessageHandler=GameObject.FindObjectOfType<MessageHandler>();
	}



	void OnTriggerEnter (Collider cldr)
	{
		if (cldr.gameObject.tag == "Arrow" && !readyToBeDestroyed) {
			Destroy (cldr.gameObject);
		} else if(cldr.gameObject.tag == "Arrow" && readyToBeDestroyed){
			Destroy(cldr.gameObject);
			GetComponent<MeshFilter>().mesh=decimatedMesh;
			gameObject.AddComponent<Shatterer>().Shatter();
		}else if (cldr.gameObject.tag == "IceSceptre" && ShouldFreeze()) {
			GameObject mysticCloud= (GameObject)Instantiate(puffEffect,transform.position,Quaternion.identity);
			mysticCloud.transform.localScale=new Vector3(0.45f,0.45f,0.45f);
			myMeshRenderer.material=iceMaterial;
			readyToBeDestroyed=true;
			Destroy(cldr.gameObject);
			StopFlameThrower();
		}
	}

	bool ShouldFreeze ()
	{
		bool readyToFreeze = true;
		if (tag == "SideSphere") {
			return true;
		} else if (tag == "HeadSphere") {
			foreach (GameObject sphr in GameObject.FindGameObjectsWithTag("SideSphere")) {
				if (!sphr.GetComponent<L2SpaceShipSphereBehaviour> ().GetReadyToBeDestroyed ()) {
					readyToFreeze = false;
				}
			}
		}
		if (!readyToFreeze) {
			levelsMessageHandler.SetMessage("Freeze  the SideSpheres First!!");
		}
		return readyToFreeze;
			
	}

	void StartFlameThrower ()
	{
		if (flameThrower) {
			if (!readyToBeDestroyed) {
				flameThrower.Play ();
			}
		}
	}

	void StopFlameThrower ()
	{
		if (flameThrower) {
			flameThrower.Stop();
		}
	}
	public bool GetReadyToBeDestroyed ()
	{
		return readyToBeDestroyed;
	}
}
