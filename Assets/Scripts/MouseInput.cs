using UnityEngine;
using System.Collections;


public class MouseInput: MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	void checkMouseInput() {
		RaycastHit rayHit = new RaycastHit();
		Ray selectRay = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
		if ( Physics.Raycast(selectRay ,out rayHit ) ) {
			Clickable objectUnderMouse = rayHit.transform.GetComponent<Clickable>() as Clickable;
			if ( objectUnderMouse != null ) {
				//objectUnderMouse.
				checkMouseClick( objectUnderMouse );
			}
		}
		
	}
	
	void checkMouseClick( Clickable clicked ) {
		if ( Input.GetMouseButtonDown(0) ) {
			//clicked.OnMouseClick(0, );
		} else if (Input.GetMouseButtonDown(1)) {
			//clicked.OnMouseClick(1, );
		}
	}
		
	
	// Update is called once per frame
	
	void Update () {
		checkMouseInput();
	}
}