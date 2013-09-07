using UnityEngine;
using System.Collections;

public class Money : MonoBehaviour {

	int money = 100;
	
	int AddMoney(){
		if (Patient.Operation == true){
			Money += Patient.Money();
		}
	}
	
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
