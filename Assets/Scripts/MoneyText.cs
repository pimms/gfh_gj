using UnityEngine;
using System.Collections;

public class MoneyText : MonoBehaviour {
	
	float MoneyInAccount = 100f;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI(){
		
		GUI.Box(new Rect(10,10,50,25), MoneyInAccount+ " £");

	}
}
