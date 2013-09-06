using UnityEngine;
using System.Collections;


public class MouseInput: MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	void checkMouseClick() {
		if ( Input.GetMouseButtonDown(0) ){
			
			RaycastHit rayHit = new RaycastHit();
			Ray selectRay = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
			
			if ( Physics.Raycast(selectRay ,out rayHit ) ) {
				Clickable clicked = rayHit.transform.GetComponent<Clickable>() as Clickable;
				if ( clicked != null ) { 
					rayHit.transform.Translate(Vector3.forward);
					//clicked.OnMouseClick(0,0);
				}
			}
		}
	}
		
	
	// Update is called once per frame
	
	void Update () {
		checkMouseClick();
	}
}