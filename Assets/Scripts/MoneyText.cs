using UnityEngine;
using System.Collections;

public class MoneyText : MonoBehaviour {
	
	public Money MoneyInAccount;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI(){
		// Making a box for showing current money value
		GUI.Box(new Rect(10,10,50,25), MoneyInAccount+" £");
		
	}
	
}
