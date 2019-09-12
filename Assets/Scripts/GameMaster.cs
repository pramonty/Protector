using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class GameMaster : MonoBehaviour {

	public AudioClip[] clips;
	public GameObject winCard;
	public GameObject loseCard;
	public Sprite lvlsWinAsset;
	public string levelToUnlock;
	public string lvlWinAssetHeader;
	public string lvlWinAssetBody;

	//public GameObject identifier;

	private static bool shieldHasArrived;
	private static bool gameOVer;
	private MessageHandler messageHandler;
	private ImageBehaviour[] allMyArsenals;
	private GameObject frstMsg;
	private LevelManager lvlMngr;
	private AudioSource audSrc;
	private int bossHealth;
	private GameObject boss;
	private Banker bnkr;
	private Player plyr;
	private GameDetailCapture myGDC;

	void Awake(){
		frstMsg=GameObject.FindGameObjectWithTag("FirstMessage");
	}

	// Use this for initialization
	void Start ()
	{
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		shieldHasArrived = false;
		gameOVer = false;
		bnkr = FindObjectOfType<Banker> ();
		messageHandler = FindObjectOfType<MessageHandler> ();
		lvlMngr = FindObjectOfType<LevelManager> ();
		audSrc = GetComponent<AudioSource> ();
		myGDC = GetComponent<GameDetailCapture> ();
		plyr = FindObjectOfType<Player> ();
		bossHealth = -1;
		allMyArsenals = new ImageBehaviour[GameObject.FindGameObjectsWithTag ("Arsenal").Length];
		int i = 0;
		foreach (GameObject go in GameObject.FindGameObjectsWithTag("Arsenal")) {
			allMyArsenals [i] = go.GetComponent<ImageBehaviour> ();
			i++;
		}
		if (lvlMngr) {
			if (lvlMngr.GetCurrentLevel () == "Level3" || lvlMngr.GetCurrentLevel () == "Level5") {
				Physics.gravity = new Vector3 (0, -8, 0);
			} else {
				Physics.gravity = Vector3.zero;
			}
		}

	}
	
	// Update is called once per frame
	void Update ()
	{
		if (GameObject.FindGameObjectWithTag ("ShieldWall")) {
			shieldHasArrived = true;
		} else {
			shieldHasArrived = false;
		}
		int money = bnkr.GetMoney ();
		foreach (ImageBehaviour ib in allMyArsenals) {
			if (money >= ib.minimumMoneyNeeded && !ib.GetMessageShown () && ib.minimumMoneyNeeded != 0) {
				messageHandler.SetMessage ("You now have " + money + " coins, which lets you use " + ib.myPrefab.name);
				ib.SetMessageShown (true);
			}
		}
		if (GameObject.FindGameObjectWithTag ("StarShip")) {
			if(GameObject.FindGameObjectWithTag ("StarShip").GetComponent<LevelBoss>()){
				bossHealth=GameObject.FindGameObjectWithTag("StarShip").GetComponent<LevelBoss>().GetMyHealth();
			}
		}


	}

	public static bool GetShieldHasArrived(){
		return shieldHasArrived;
	}

	public static bool GetGameOver ()
	{
		return gameOVer;
	}

	public void StarShipDestroyed ()
	{
		gameOVer = true;
		plyr.enabled = false;
		frstMsg.SetActive (true);
		frstMsg.GetComponentInChildren<Text> ().text = "YOU WIN!!!";
		frstMsg.GetComponent<FirstMessageBehaviour> ().SetPersistentDisplay (true);
		audSrc.clip = clips [0];
		myGDC.PersistGameDetails (1, Convert.ToInt32 (lvlMngr.GetCurrentLevel ().Substring (5)));
		if (!PlayerPrefManager.GetSoundOff ())
			audSrc.Play ();
		PlayerPrefManager.SetLevelUnlocked (levelToUnlock, true);
		PlayerPrefManager.SetPlayerCoins (PlayerPrefManager.GetPlayerCoins () + bnkr.GetMoney ());
		PlayerPrefManager.SetNextLevel (levelToUnlock);
		GameObject[] minions=ReturnGameobjectsWithTag(new string[]{"UFOGO","GroundEnemy"});
		foreach (GameObject minion in minions) {
			minion.GetComponent<Minions>().GameMasterDestruction();
		}
		Screen.sleepTimeout=10;
		Invoke("ShowWinCard",clips[0].length);
	}

	public void TowerDestroyed(){
		gameOVer=true;
		plyr.enabled=false;
		frstMsg.SetActive(true);
		frstMsg.GetComponentInChildren<Text>().text="YOU LOSE!!!";
		frstMsg.GetComponent<FirstMessageBehaviour>().SetPersistentDisplay(true);
		audSrc.clip=clips[1];
		myGDC.PersistGameDetails(0,Convert.ToInt32(lvlMngr.GetCurrentLevel().Substring(5)));
		if(!PlayerPrefManager.GetSoundOff())audSrc.Play();
		Screen.sleepTimeout=10;
		Invoke("ShowLoseCard",clips[1].length);
	}

	void ShowWinCard ()
	{
		frstMsg.transform.GetChild (0).GetComponent<Text> ().text = "";
		GameObject levelWinCard = (GameObject)Instantiate (winCard, frstMsg.transform);
		levelWinCard.transform.GetChild (0).GetComponent<Image> ().sprite = lvlsWinAsset;
		levelWinCard.transform.GetChild (1).GetComponent<Text> ().text = lvlWinAssetHeader;
		levelWinCard.transform.GetChild (2).GetComponent<Text> ().text = lvlWinAssetBody;
		Animator anim = levelWinCard.transform.GetChild (0).gameObject.GetComponent<Animator> ();
		if (anim != null) {
			float delayTime=anim.GetCurrentAnimatorClipInfo(0)[0].clip.length;
			//StartCoroutine(ShowAd(delayTime));
		}
	}

	void ShowLoseCard(){
		frstMsg.transform.GetChild(0).GetComponent<Text>().text="";
		Instantiate(loseCard,frstMsg.transform);
		//lvlMngr.ShowAdNow();
	}

	/*IEnumerator ShowAd (float delayTime)
	{
		yield return new WaitForSeconds(delayTime);
		lvlMngr.ShowAdNow();
	}*/


	public void LoadLevel (int direction)
	{
		if (direction == 0) {
			lvlMngr.LoadScene ("Start");
		} else if(direction==1) {
			lvlMngr.LoadScene(levelToUnlock);
		}

	}

	public void RestartLevel ()
	{
		lvlMngr.RestartLevel();
	}

	public int GetBossHealth(){
		return bossHealth;
	}

	private GameObject[] ReturnGameobjectsWithTag(string[] tags){
		List<GameObject> gameObjects=new List<GameObject>();
		foreach(string tag in tags){
			GameObject[] objs=GameObject.FindGameObjectsWithTag(tag);
			gameObjects=gameObjects.Concat(objs.ToList()).ToList();
		}

		return gameObjects.ToArray();
	}

}
