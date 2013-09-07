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
		
<<<<<<< HEAD
		GUI.Box(Rect(10,10,50,25), MoneyInAccount+ " £");
=======
		//GUI.Box(Rect (10,10,50,25), MoneyInAccount+ " £");
>>>>>>> c98fef8894869885a4457e39fca7e268c92d4ab4
		
	}
}
