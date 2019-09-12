using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FindGO : MonoBehaviour {

	private static string element="SideSphere";

	[MenuItem("Helper/SelectByTag")]
	public static void FindSideSpheres(){
		GameObject[] objects=GameObject.FindGameObjectsWithTag(element);
		Selection.objects=objects;
	}

	[MenuItem("Helper/DestroySideSpheres")]
	public static void DestroySideSpheres ()
	{
		foreach (GameObject obj in GameObject.FindGameObjectsWithTag(element)) {
			DestroyImmediate(obj);
		}
	}
}
