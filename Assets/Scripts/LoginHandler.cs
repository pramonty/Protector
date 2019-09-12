using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.Security.Cryptography;
using UnityEngine.Networking;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class LoginHandler : MonoBehaviour {

	public GameObject loginPanel;
	public GameObject userIdBox;
	public GameObject pswrdBox;
	public GameObject loginMessageText;

	private Color loginPanelColor;

	// Use this for initialization
	void Start () {
		loginPanel.SetActive(false);
		loginPanelColor=loginPanel.GetComponent<Image>().color;
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void ShowLoginPanel(){
		loginPanel.SetActive(true);
		loginPanelColor.a=0;
		loginPanel.GetComponent<Image>().color=loginPanelColor;
		InvokeRepeating("FadeInPanel",0,0.05f);
	}

	void FadeInPanel ()
	{
		loginPanelColor.a += 0.1f;
		loginPanel.GetComponent<Image>().color=loginPanelColor;
		if (loginPanel.GetComponent<Image> ().color.a >= 1) {
			CancelInvoke();
		}
	}

	public void HandleCancel(){
		loginPanel.SetActive(false);
	}

	public void HandleLogin(){
		string userId=userIdBox.GetComponent<InputField>().text;
		string psswerd=pswrdBox.GetComponent<InputField>().text;
		string hashToSend=ComputeAndReturnHash(userId,psswerd);
		string endpoint="https://bepnerss19.execute-api.us-east-2.amazonaws.com/PRD/handlelogin";
		string host="bepnerss19.execute-api.us-east-2.amazonaws.com";
		string canonicalURI="/PRD/handlelogin";
		string requestParameter="{\"path\":3,\"playerLogin\":{\"userId\":\""+userId+"\",\"hexHashString\":\""+hashToSend+"\"}}";
		UnityWebRequest request=RestRequestTest.PostRequest(host,endpoint,requestParameter,canonicalURI);
		StartCoroutine(SendRequest(request));
		//while(!request.isDone){

		//}
		//print(request.downloadHandler.text);
	}

	IEnumerator SendRequest (UnityWebRequest request)
	{
		Text messageDisplayText = loginMessageText.GetComponent<Text> ();
		messageDisplayText.fontSize = 25;
		messageDisplayText.alignment = TextAnchor.MiddleCenter;
		messageDisplayText.text = "Please Wait while we validate.";
		messageDisplayText.color=Color.black;
		yield return request.SendWebRequest ();
		var response = JObject.Parse (request.downloadHandler.text);
		if ((int)response ["code"] == 0) {
			messageDisplayText.text = "Login Succesfull";
			messageDisplayText.color = Color.green;
			GetComponent<Text>().text=userIdBox.GetComponent<InputField>().text;
			userIdBox.GetComponent<InputField>().text="";
			pswrdBox.GetComponent<InputField>().text="";
		} else if ((int)response ["code"] == 3) {
			messageDisplayText.text="Invalid Username/Password";
			messageDisplayText.color=Color.red;
		}

	}

	string ComputeAndReturnHash (string usrId, string pswerd)
	{
		string hashableString = usrId + pswerd;
		SHA256 hashingObj = SHA256Managed.Create ();
		byte[] hash = hashingObj.ComputeHash (Encoding.UTF8.GetBytes (hashableString));
		StringBuilder strb = new StringBuilder (hash.Length*2);
		foreach (byte b in hash) {
			strb.AppendFormat("{0:x2}",b);
		}
		return strb.ToString();
	}
}
