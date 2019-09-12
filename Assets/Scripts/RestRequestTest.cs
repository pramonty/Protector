using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Security.Cryptography;
using System.Threading;

public class RestRequestTest{

	public static UnityWebRequest PostRequest (string host, string endpoint, string requestParameter, string canonicalURI)
	{
		string accessKey = "AKIAIYCZHSLOSOI5FKFQ";
		string secretKey = "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";
		//string host="t0al3w3ci1.execute-api.us-east-2.amazonaws.com";
		//string endpoint="https://t0al3w3ci1.execute-api.us-east-2.amazonaws.com/POSTDEPL/playwithapi";
		DateTime timeNow = DateTime.UtcNow;
		string dateTime = timeNow.ToString ("yyyyMMddTHHmmssZ");
		string date = timeNow.Date.ToString ("yyyyMMdd");
		string location = "us-east-2";
		string service = "execute-api";
		string algorithm = "AWS4-HMAC-SHA256";
		//string canonicalURI="/POSTDEPL/playwithapi";
		string credentialScope = date + "/" + location + "/" + service + "/" + "aws4_request";
		string canonicalHeader = "content-type:application/json" + "\n" + "host:" + host + "\n" + "x-amz-date:" + dateTime + "\n";
		string canonicalQueryString = "";
		string signedHeader = "content-type;host;x-amz-date";
		//string requestParameter="{\"id\":1408,\"firstName\":\"Jason\",\"lastName\":\"Mamoa\"}";
		SHA256 hashingObj = SHA256Managed.Create ();
		string payloadHash = ToHexString (hashingObj.ComputeHash (Encoding.UTF8.GetBytes (requestParameter)));
		String canonicalRequest = "POST" + "\n" + canonicalURI + "\n" + canonicalQueryString + "\n" + canonicalHeader + "\n" + signedHeader + "\n" + payloadHash;
		string stringToSign = algorithm + "\n" + dateTime + "\n" + credentialScope + "\n" + ToHexString (hashingObj.ComputeHash (Encoding.UTF8.GetBytes (canonicalRequest)));
		byte[] signing_key = getSignatureKey (secretKey, date, location, service);
		string signature = ComputeHmacSha256Hash (signing_key, stringToSign);
		string authorizationHeader = algorithm + " " + "Credential=" + accessKey + "/" + credentialScope + ", " + "SignedHeaders=" + signedHeader + ", " + "Signature=" + signature;

		UnityWebRequest request = new UnityWebRequest (endpoint, "POST");
		request.SetRequestHeader ("Content-Type", "application/json");
		request.SetRequestHeader ("host", host);
		request.SetRequestHeader ("X-Amz-Date", dateTime);
		request.SetRequestHeader ("Authorization", authorizationHeader);
		request.downloadHandler = new DownloadHandlerBuffer ();
		request.uploadHandler = (UploadHandler)new UploadHandlerRaw (Encoding.UTF8.GetBytes (requestParameter));
		//request.chunkedTransfer = false;


		return request;


	}

	/*void SendAndParseRequest ()
	{
		request.SendWebRequest ();
		while (!request.isDone) {
		}
		Debug.Log(request.downloadHandler.text);
		Debug.Log(Encoding.UTF8.GetString(request.downloadHandler.data));
	}*/

	/*IEnumerator SendAndProcessWebRequest (UnityWebRequest req)
	{
		yield return req.SendWebRequest();
		Debug.Log(req.downloadHandler.text);
		Debug.Log(Encoding.UTF8.GetString(req.downloadHandler.data));
	}*/

	/*void GetRequest ()
	{
		string accessKey="AKIAIYCZHSLOSOI5FKFQ";
		string secretKey="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";
		string host="kex2zv6ln0.execute-api.us-east-2.amazonaws.com";
		string endpoint="https://kex2zv6ln0.execute-api.us-east-2.amazonaws.com/PROD/MyFirstTestFunction?";
		DateTime timeNow=DateTime.UtcNow;
		string datetime=timeNow.ToString("yyyyMMddTHHmmssZ");
		string date=timeNow.Date.ToString("yyyyMMdd");
		string location="us-east-2";
		string service="execute-api";
		string algorithm="AWS4-HMAC-SHA256";
		string canonicalUri="/PROD/MyFirstTestFunction";
		string credentialScope=date+"/"+location+"/"+service+"/"+"aws4_request";
		string canonicalHeader="host:"+host+"\n";
		string canonicalQueryString="SignatureVersion=4"+"&X-Amz-Algorithm=AWS4-HMAC-SHA256"+"&X-Amz-Credential="+
									WebUtility.UrlEncode(accessKey+"/"+credentialScope)+
									"&X-Amz-Date="+datetime+
									"&X-Amz-SignedHeaders=host";
		SHA256 hash256 = SHA256Managed.Create();
		string payloadHash=ToHexString(hash256.ComputeHash(Encoding.UTF8.GetBytes("")));
		string canonicalRequest="GET"+"\n"+canonicalUri+"\n"+canonicalQueryString+"\n"+canonicalHeader+"\n"+"host"+"\n"+payloadHash;
		string stringToSign=algorithm+"\n"+datetime+"\n"+credentialScope+"\n"+ToHexString(hash256.ComputeHash(Encoding.UTF8.GetBytes(canonicalRequest)));
		Byte[] signing_key=getSignatureKey(secretKey,date,location,service);
		string signature=ComputeHmacSha256Hash(signing_key,stringToSign);
		canonicalQueryString+="&X-Amz-Signature="+signature;
		string url = endpoint+canonicalQueryString;
		UnityWebRequest request = UnityWebRequest.Get (url);
		try {
			request.SendWebRequest();
			print(request.downloadHandler.text);
			print(request.responseCode);
		} catch (Exception ex) {
			print(ex.Message);
		}
	}*/

	static string ToHexString (byte[] data)
	{
		StringBuilder str = new StringBuilder (data.Length * 2);
		foreach (byte b in data) {
			str.AppendFormat("{0:x2}",b);
		}
		return str.ToString();
	}

	static string ComputeHmacSha256Hash (byte[] key, string data)
	{
		HMACSHA256 hmac=new HMACSHA256(key);
		Byte[] hashByte=hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
		return ToHexString(hashByte);
	}


	 static byte[] HmacSHA256(String data, byte[] key)
{
    String algorithm = "HmacSHA256";
    KeyedHashAlgorithm kha = KeyedHashAlgorithm.Create(algorithm);
    kha.Key = key;

    return kha.ComputeHash(Encoding.UTF8.GetBytes(data));
}

 static byte[] getSignatureKey(String key, String dateStamp, String regionName, String serviceName)
{
    byte[] kSecret = Encoding.UTF8.GetBytes(("AWS4" + key).ToCharArray());
    byte[] kDate = HmacSHA256(dateStamp, kSecret);
    byte[] kRegion = HmacSHA256(regionName, kDate);
    byte[] kService = HmacSHA256(serviceName, kRegion);
    byte[] kSigning = HmacSHA256("aws4_request", kService);

    return kSigning;
}
}
