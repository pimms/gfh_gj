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
		if (!_order.actors.Contains(actor)) {
			_order.actors.Add(actor);
		}

		actor.OnSelect();
	}
	
	public void AddAsSubject(Clickable Subject) {
		_order.subject = Subject;
		Subject.OnSelect();
	}
	
	public void AddAsObject(Clickable ObjectAction) {
		_order.objectAction = ObjectAction;
	}
	
	
	public void PerformOrder() {
		Debug.Log("Performing order!");
		
		if (  _order.objectAction.GetType()  == typeof(Bed) || _order.objectAction.GetType()  == typeof(OrBed)  ) {
			foreach (Clickable actor in _order.actors) {
				if ( actor.GetType() == typeof(Nurse) && (_order.objectAction as Bed).nurse == null ) {
					Bed bed = _order.objectAction as Bed;
					bed.nurse = actor as Nurse;
					actor.BeginPerform(_order);
				} else if ( actor.GetType() == typeof(Surgeon) && _order.objectAction.GetType() == typeof(OrBed) && (_order.objectAction as OrBed).surgeon == null ) {
					OrBed orBed = _order.objectAction as OrBed;
					orBed.surgeon = actor as Surgeon;
					actor.BeginPerform(_order);
					
				}

			}
			if (order.subject != null && (_order.objectAction as Bed) != null && (_order.objectAction as Bed).patient == null) {
				Bed bed = _order.objectAction as Bed;
				bed.patient = order.subject as Patient;
				order.subject.BeginPerform(_order);
			}
		}
	}
	
	public void Clear() {
		foreach (Clickable actor in _order.actors) {
			actor.OnDeselect();
		}

		if (_order.subject != null) _order.subject.OnDeselect();
		if (_order.objectAction != null) _order.objectAction.OnDeselect();

		_order = new Order();
	}
}