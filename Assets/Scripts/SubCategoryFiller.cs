using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubCategoryFiller : MonoBehaviour {

	public GameObject imgPrefab;
	public GameObject descriptionPanel;
	public KeyValuePair[] arsenalKV;
	public KeyValuePair[] enemyKV;

	[System.Serializable]
	public class KeyValuePair{
		public Sprite image;
		public string description;
	}

	void Start(){
		descriptionPanel.GetComponent<Text>().text="";
	}

	public void DisplayArsenal ()
	{

		DisplayImages(arsenalKV);
		
	}

	public void DisplayEnemies ()
	{

		DisplayImages(enemyKV);
	}


	void DisplayImages (KeyValuePair[] kv)
	{
		descriptionPanel.GetComponent<Text>().text="";
		int totalImages = kv.Length;
		int horizontalOffset = 10;
		int verticalOffset = 0;
		DestroyChildren();
		for (int i = 1; i <= totalImages; i++) {
			if (i % 3 == 1 && i != 1) {
				horizontalOffset = 10;
				verticalOffset += 130;
			}
			GameObject GO = (GameObject)Instantiate (imgPrefab, transform);
			int val=i-1;
			Image goImage=GO.GetComponent<Image>();
			goImage.sprite = kv[val].image;
			Color tempColor=goImage.color;
			tempColor.a=0.5f;
			goImage.color=tempColor;
			GO.AddComponent<Button>().onClick.AddListener(()=>ShowMessage(kv[val].description,GO));
			GO.GetComponent<RectTransform> ().SetInsetAndSizeFromParentEdge (RectTransform.Edge.Left, horizontalOffset, 100);
			GO.GetComponent<RectTransform> ().SetInsetAndSizeFromParentEdge (RectTransform.Edge.Top, verticalOffset, 100);
			horizontalOffset += 130;
			
		}
	}

	void ShowMessage (string message, GameObject myImage)
	{
		descriptionPanel.GetComponent<Text> ().text = message;
		foreach (GameObject img in GameObject.FindGameObjectsWithTag("StandardImage")) {
				Color tempColor=img.GetComponent<Image>().color;
				tempColor.a=(img.GetInstanceID()==myImage.GetInstanceID())?1:0.5f;
				img.GetComponent<Image>().color=tempColor;
		}
	}

	void DestroyChildren(){
		for(int i=0;i<transform.childCount;i++){
			Destroy(transform.GetChild(i).gameObject);
		}
	}
}
