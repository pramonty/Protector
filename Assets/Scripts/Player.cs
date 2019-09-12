using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CustomParentClass {

	public float velocityScaler;
	public Vector3 shieldBombThrowPosition;

	private bool imageClicked;
	private GameObject[] arsenal;
	private ImageBehaviour currentImageBehaviour;
	private GameObject toGeneratePrefab;
	private MessageHandler messageHandler;
	private Banker bnkr;
	private PauseHandler pauseState;

	// Use this for initialization
	void Start ()
	{
		imageClicked = false;
		arsenal = GameObject.FindGameObjectsWithTag ("Arsenal");
		messageHandler=FindObjectOfType<MessageHandler>();
		bnkr=FindObjectOfType<Banker>();
		pauseState=FindObjectOfType<PauseHandler>();
	}
	
	// Update is called once per frame
	void Update ()
	{

		foreach (GameObject go in arsenal) {
			if (go.GetComponent<ImageBehaviour>().imageActive) {
				currentImageBehaviour=go.GetComponent<ImageBehaviour>();
				toGeneratePrefab=currentImageBehaviour.myPrefab;
				break;
			}
		}
		if (Input.GetKeyDown (KeyCode.Mouse0) && !ImageWasClicked() && currentImageBehaviour.imageActive) {
			Fire();
		}
	}

	void Fire ()
	{
		try {
			bnkr.DecrementMoney (currentImageBehaviour.minimumMoneyNeeded);
			Vector3 arrowBirthPosition = transform.position + new Vector3 (0f, 0f, 2.5f);
			GameObject arrow = (GameObject)Instantiate (toGeneratePrefab, arrowBirthPosition, toGeneratePrefab.transform.rotation);
			Vector3 rayDirection = (/*(toGeneratePrefab.tag == "ShieldBomb") ? (getShieldBombRay()):*/(Camera.main.ScreenPointToRay (Input.mousePosition).direction));
			float turnAboutY = Mathf.Atan2 (rayDirection.x, rayDirection.z) * (180 / Mathf.PI);
			float turnAboutX = Mathf.Atan2 (rayDirection.y, rayDirection.z) * (180 / Mathf.PI);
			arrow.transform.Rotate (new Vector3 (-turnAboutX, (AmongstChosenOnes() ? turnAboutY : -turnAboutY), 0f));
			arrow.GetComponent<Rigidbody> ().velocity = rayDirection * (toGeneratePrefab.tag=="ShieldBomb"?velocityScaler*2:velocityScaler);
			arrow.GetComponent<Rigidbody> ().angularVelocity = ((toGeneratePrefab.tag == "ShieldBomb") ? (new Vector3 (0f, 2.5f, 0f)) : Vector3.zero);			
		} catch (NotEnoughMoney ex) {
			messageHandler.SetMessage(ex.Message);
		}
	}

	bool AmongstChosenOnes(){
		if(toGeneratePrefab.tag == "Arrow" || toGeneratePrefab.tag == "IceSceptre" || toGeneratePrefab.tag == "Mace")
			return true;
		else
			return false;
	}

	Vector3 getShieldBombRay(){
		return (shieldBombThrowPosition-transform.position).normalized;
	}

	bool ImageWasClicked ()
	{
		
		if (imageClicked) {
			imageClicked = false;
			return true;
		} else {
			return false;
		}
	}

	public void ImageClicked (string name)
	{
		if(!pauseState.GetGamePaused())
			imageClicked = true;
		if (name != "Pause") {
			foreach (GameObject go in arsenal) {
				if (go.name == name) {
					go.GetComponent<ImageBehaviour> ().ImageClicked ();
					break;		
				}
			}
		}

	}
}
