using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/* Order defines what is going to happen:
 * ATOR(s) does something to SUBJECT using OBJECT
 */
public class Order {
	public Order() {
		actors = new List<Clickable>();	
	}

	public List<Clickable> actors;
	public Clickable subject;
	public Clickable objectAction;
}

/* The InputOrder is a wrapper around an 'Order' object
 * which is under construction.
 * 
 * Clickable objects must add themselves to the Order
 * as they see fit, and may use the data contained within
 * at any time. The Clickable objects are blindly trusted.
 */
public class InputOrder {
	private Order _order;
	public Order order {
		get { return _order; }
	}

	public InputOrder() {
		_order = new Order();
	}
	
	public void AddAsActor(Clickable actor) {
		_order.actors.Add(actor);
	}
	
	public void AddAsSubject(Clickable Subject) {
		_order.subject = Subject;
	}
	
	public void AddAsObject(Clickable ObjectAction) {
		_order.objectAction = ObjectAction;
	}
	
	public void PerformOrder() {
		foreach (Clickable actor in order.actors) {
			actor.BeginPerform(order);
		}
	}
	
	public void Clear() {
		_order = new Order();
	}
}