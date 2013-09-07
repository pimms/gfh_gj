using UnityEngine;
using System.Collections;


public class MoneyText : MonoBehaviour {

	
	int ShowMoney(){
		guiText.text = "Money = ";
		
	}
	
	// Use this for initialization
	void Start () {
		ShowMoney ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
