using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;
using UnityEngine.Networking;

public class GameDetailCapture : MonoBehaviour {

	private string userId;
	private string startDttm;
	private string endDttm;
	private int gameResult;
	private string host;
	private string endpoint;
	private string requestParameter;
	private string canonicalURI;

	// Use this for initialization
	void Start () {
		userId="pramonty";
		startDttm=DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
	}

	public void PersistGameDetails(int gameResult,int level){
		this.gameResult=gameResult;
		endDttm=DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
		host="un9nwjxz7h.execute-api.us-east-2.amazonaws.com";
		canonicalURI="/DEV/storegamedetails";
		endpoint="https://un9nwjxz7h.execute-api.us-east-2.amazonaws.com/DEV/storegamedetails";
		requestParameter="{\"path\":1,\"gameDetails\":{\"playerId\":\""+userId+"\",\"startDttm\":\""+startDttm+
		                        "\",\"endDttm\":\""+endDttm+"\",\"level\":"+level+",\"gameResult\":"+this.gameResult+"}}";
		//Thread childRequestThread=new Thread(new ThreadStart(SendRequest));
		//childRequestThread.Start();
		UnityWebRequest request=RestRequestTest.PostRequest(host,endpoint,requestParameter,canonicalURI);
		StartCoroutine(SendWebRequest(request));
	}

	IEnumerator SendWebRequest(UnityWebRequest request){
		print("Before");
		yield return request.SendWebRequest();
		print("After");
		print(request.downloadHandler.text);
	}
}
