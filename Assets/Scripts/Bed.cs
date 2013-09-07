using UnityEngine;
using System.Collections;

public class Bed : Clickable {

	void Start () {
	
	}
	
	void Update () {
	
	}

	void OnMouseClick(int mouseButton, InputOrder inOrder) {
		if (inOrder.order.actors.Count != 0 &&
		inOrder.order.subject != null) {
			inOrder.AddAsObject(this);
		}
	}
}
