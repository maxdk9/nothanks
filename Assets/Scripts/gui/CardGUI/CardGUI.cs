using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardGUI : MonoBehaviour {

	
	public TMPro.TextMeshProUGUI CenterNumber;
	public TMPro.TextMeshProUGUI ULNumber;
	public TMPro.TextMeshProUGUI Score;
	
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void draw(int number, bool showscore)
	{
		CenterNumber.text = ULNumber.text = number.ToString();
		Score.text=showscore?"-"+number:"";

	}
}
