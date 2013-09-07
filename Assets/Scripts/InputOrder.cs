using UnityEngine;
using System.Collections;
using System.Collections.Generic;

struct Order {
	public List<Clickable> actors = new List<Clickable>();
	public Clickable subject = new Clickable();
	public Clickable objectAction = new Clickable();
}

public class InputOrder {
	public List<Clickable> actors = new List<Clickable>();
	public Clickable subject = new Clickable();
	public Clickable objectAction = new Clickable();
	public Order order = new Order();
	
	public void AddAsActor(Clickable actor) {
		actors.Add(actor);
	}
	
	public void AddAsSubject(Clickable Subject) {
		subject = Subject;
	}
	
	public void AddAsObject(Clickable ObjectAction) {
		objectAction = ObjectAction;
	}
	
	public void CreateOrder() {
		//Order order = new Order();
		order.actors = actors;
		order.subject = subject;
		order.objectAction = objectAction;
		
		order.Perform();
	}
	
	public void PerformOrder() {
		foreach (Clickable actor in actors) {
			actor.OnMouseClick(0, order);
		}
	}
	
	public void ClearOrder() {
		actors.Clear();
		Subject = null;
		ObjectAction = null;
	}
}