using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MouseInput: MonoBehaviour {
	// Use this for initialization

	private InputOrder inputOrder;
	private Vector2 currentMouse;
	private Vector2 startMouse;
	private bool dragStart;
	private bool clickStart;
	Vector2 startBox;
	Vector2 endBox;
	
	//Clickable clicked = rayHit.transform.GetComponent<Clickable>() as Clickable;
	
	void Start () {
		inputOrder = new InputOrder();
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

	bool withinDrag( Vector2 coordinates ){
		if ( coordinates.x > startBox.x && coordinates.x < endBox.x && coordinates.y < startBox.y && coordinates.y > endBox.y  ) {
			return true;
		}
		
		return false;
	}
	
	void mouseDrag( int mouseKey ) {
		List<Clickable> beds = new List<Clickable>();
		List<Clickable> clickItems = new List<Clickable>();

		foreach (Clickable clickables in FindObjectsOfType(typeof(Clickable)) as Clickable[]){
			if ( withinDrag(Camera.main.WorldToScreenPoint(clickables.transform.position))){
				if (clickables as Bed != null) {
					beds.Add(clickables);
				} else {
					clickItems.Add(clickables);
				}
			}
		}

		if (beds.Count > 0 || clickItems.Count > 0) {
			if (!Input.GetKey(KeyCode.LeftShift)) {
				inputOrder.Clear();
			}

			if (clickItems.Count == 0) {
				foreach (Clickable bed in beds) {
					bed.OnMouseClick(100 + mouseKey, inputOrder);
				}
			} else {
				foreach (Clickable item in clickItems) {
					item.OnMouseClick(100 + mouseKey, inputOrder);
				}
			}
		}
	}
	
	void mouseClick( int mouseKey ) {
		RaycastHit rayHit = new RaycastHit();
		Ray selectRay = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
		if ( Physics.Raycast(selectRay ,out rayHit ) ) {
			Clickable clicked = rayHit.transform.GetComponent<Clickable>();

			if (clicked == null && rayHit.transform.parent != null) {
				clicked = rayHit.transform.parent.GetComponent<Clickable>();
			}

			if ( clicked != null ) {
				if (!Input.GetKey(KeyCode.LeftShift)) {
					inputOrder.Clear();
				}

				clicked.OnMouseClick(mouseKey, inputOrder);
			} else {
				if (mouseKey == 1) {
					Vector3 destination = rayHit.point;
					if (Mathf.Abs(destination.y) < 0.1f) {
						Vector3 center = new Vector3();

						// Find the center of the group
						foreach (Clickable actor in inputOrder.order.actors) {
							center += actor.transform.position / inputOrder.order.actors.Count;
						}

						// Move them relative to one another
						foreach (Clickable actor in inputOrder.order.actors) {
							Vector3 actorDest = destination + (actor.transform.position - center);
							Person person = actor as Person;
							person.GoToPosition(actorDest);
						}
					}
				} else {
					inputOrder.Clear();
				}
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
				if ((startMouse-currentMouse).magnitude > 15f) {
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
