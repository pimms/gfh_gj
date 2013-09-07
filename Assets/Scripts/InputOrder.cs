using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct Order {
	
	public List<Clickable> actors;// = new List<Clickable>();
	public Clickable subject;// = new Clickable();
	public Clickable objectAction;// = new Clickable();
}

public class InputOrder {
	//public List<Clickable> actors = new List<Clickable>();
	//public Clickable subject = new Clickable();
	//public Clickable objectAction = new Clickable();
	Order order = new Order();
	
	public void AddAsActor(Clickable actor) {
		order.actors.Add(actor);
	}
	
	public void AddAsSubject(Clickable Subject) {
		order.subject = Subject;
	}
	
	public void AddAsObject(Clickable ObjectAction) {
		order.objectAction = ObjectAction;
	}
	
	public void PerformOrder() {
		foreach (Clickable actor in order.actors) {
			actor.OnMouseClick(0, order);
		}
	}
	
	public void ClearOrder() {
		//order.actors.Clear();
		order.subject = null;
		order.objectAction = null;
	}
}