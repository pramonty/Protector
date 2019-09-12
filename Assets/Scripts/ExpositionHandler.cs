using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpositionHandler : CustomParentClass {

	public GameObject expositionUnit;
	public GameObject expositionPanel;
	public Sprite[] images;
	public string[] desc;

	private int numberOfPages;
	private int currentDisplayPage;

	void Start(){
		numberOfPages=(int)Mathf.Ceil((float)images.Length/3f);
		currentDisplayPage=0;
	}

	public void DisplayPage ()
	{
		if (!expositionPanel.activeSelf) {
			expositionPanel.SetActive (true);
			showPage ();
		}
	}

	public void PreviousPage ()
	{
		
		if (currentDisplayPage >= 2) {
			currentDisplayPage -= 2;
			showPage ();
		}
	}

	public void NextPage ()
	{
		if (currentDisplayPage < numberOfPages) {
			showPage ();
		}
	}

	public void showPage(){
		int upperLimit;
		int verticalOffset = 50;
		int value=currentDisplayPage*3;
		upperLimit=images.Length-value<=3?images.Length-value:3;
		DestroyEUS();
		for (int i = value; i < value+upperLimit; i++) {
			GameObject GO=Instantiate(expositionUnit,expositionPanel.transform);
			GO.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top,verticalOffset,100);
			verticalOffset+=200;
			GO.transform.GetChild(0).gameObject.GetComponent<Image>().sprite=images[i];
			GO.transform.GetChild(1).gameObject.GetComponent<Text>().text=desc[i];
		}
		currentDisplayPage++;
	}

	void DestroyEUS ()
	{
		foreach (GameObject go in GameObject.FindGameObjectsWithTag("EU")) {
			Destroy(go);
		}
	}

	public void ContinueGame ()
	{
		expositionPanel.SetActive(false);
		currentDisplayPage=0;
	}
}
