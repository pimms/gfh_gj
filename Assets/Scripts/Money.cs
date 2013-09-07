using UnityEngine;
using System.Collections;


public class Money : MonoBehaviour {

		// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	float MoneyInAccount = 100;


	void AddMoneyPatient(){
		if (Patient.Operation == true){
			MoneyInAccount += Patient.Money();
		}else{
			//FUCK OFF!
		}
	}
}
