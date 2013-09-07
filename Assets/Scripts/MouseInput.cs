using UnityEngine;
using System.Collections;


public class MouseInput: MonoBehaviour {
	// Use this for initialization
	
	private Vector2 currentMouse;
	private Vector2 startMouse;
	private bool dragStart;
	private bool clickStart;
	Vector2 startBox;
	Vector2 endBox;
	
	//Clickable clicked = rayHit.transform.GetComponent<Clickable>() as Clickable;
	
	void Start () {
		startMouse = new Vector2(100, 100);
		currentMouse = new Vector2(200, 200);
		clickStart = false;
		dragStart = false;
	}

	void OnGUI () {

		if ( dragStart == true ) {
			if ( currentMouse.x > startMouse.x ) {
				startBox.x = startMouse.x;
				endBox.x = currentMouse.x;
			} else {
				startBox.x = currentMouse.x;
				endBox.x = startMouse.x;
			}
			if ( currentMouse.y < startMouse.y ) {
				startBox.y = startMouse.y;
				endBox.y = currentMouse.y;
			} else {
				startBox.y = currentMouse.y;
				endBox.y = startMouse.y;
			}
			GUI.Box(new Rect(startBox.x, Screen.height -  startBox.y, endBox.x - startBox.x, startBox.y - endBox.y), "");
			
		}
	}
			
	/*
	void checkMouseInput() {
		RaycastHit rayHit = new RaycastHit();
		Ray selectRay = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
		if ( Physics.Raycast(selectRay ,out rayHit ) ) {
			Clickable objectUnderMouse = rayHit.transform.GetComponent<Clickable>() as Clickable;
			if ( objectUnderMouse != null ) {
				objectUnderMouse.OnMouseHover();
				checkMouseClick( objectUnderMouse );
			}
		}
	}
	
	void checkMouseOrder() {
		if (Input.GetMouseButtonUp(0) && !Input.GetMouseButtonUp(1)) {
			RaycastHit rayHit = new RaycastHit();
			Ray selectRay = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
			if ( Physics.Raycast(selectRay ,out rayHit ) ) {
				Clickable clicked = rayHit.transform.GetComponent<Clickable>() as Clickable;
				if ( clicked != null ) {
					//clicked.OnMouseClick(0, );
				}
			}		
		}
	}
	
	void selectUnitsBox (){
		foreach (var gameObj in FindObjectsOfType(typeof(GameObject)) as GameObject[]) {
			
		}

	}
	
	void checkMouseSelection() { 
		if ( Input.GetMouseButtonDown(1)){ 
			startBox = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			endBox = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			dragging = true;
			selection = false;
		} else if ( Input.GetMouseButtonUp(1 )){
			dragging = false;
			selectUnitsBox ();
			selection = true;
		}
	}
	
	
	*/
	
	bool withinDrag( Vector2 coordinates ){
		if ( coordinates.x > startBox.x && coordinates.x < endBox.x && coordinates.y < startBox.y && coordinates.y > endBox.y  ) {
			return true;
		}
		
		return false;
	}
	
	void mouseDrag( int mouseKey ) {
		foreach (Clickable clickables in FindObjectsOfType(typeof(Clickable)) as Clickable[]){
			if ( withinDrag(Camera.main.WorldToScreenPoint(clickables.transform.position))){
				//clicked.OnMouseClick(mouseKey+100, );
				clickables.transform.Translate( new Vector3(1,1,1));//test
			}
		}
	}
	
	void mouseClick( int mouseKey ) {
		RaycastHit rayHit = new RaycastHit();
		Ray selectRay = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
		if ( Physics.Raycast(selectRay ,out rayHit ) ) {
			Clickable clicked = rayHit.transform.GetComponent<Clickable>() as Clickable;
			if ( clicked != null ) {
				//clicked.OnMouseClick(mouseKey, );
				//clicked.transform.Translate( new Vector3(1,1,1));//test
			}
		}
	}
	
	
	void checkMouseInput( int mouseKey ) {
		currentMouse = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		if ( Input.GetMouseButtonDown(mouseKey)){ 
			startMouse = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			clickStart = true;
		} else if ( clickStart ) {
			if ( !dragStart ) {
				if ((startMouse-currentMouse).magnitude > 1.3f) {
				dragStart = true;
				} 
				if ( Input.GetMouseButtonUp( mouseKey )) {
					mouseClick( mouseKey );
					dragStart = false;
					clickStart = false;
				}	
			} else if ( Input.GetMouseButtonUp( mouseKey )) {
				mouseDrag( mouseKey );
				clickStart = false;
				dragStart = false;
			}
		}
	}

	
	// Update is called once per frame
	void Update () {
		checkMouseInput( 0 );
		checkMouseInput( 1 );
	}
}
