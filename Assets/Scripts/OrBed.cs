using UnityEngine;
using System.Collections;

public class OrBed : Clickable {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public override void OnMouseClick(int mouseButton, InputOrder inOrder) {
		if (inOrder.order.actors.Count != 0 
		&&  inOrder.order.subject != null) {
			inOrder.AddAsObject(this);

			inOrder.PerformOrder();
			inOrder.Clear();
		}
	}
}
