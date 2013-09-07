using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputOrder {
	public List<Clickable> actors = new List<Clickable>();
	public Clickable Subject = new Clickable();
	public Clickable ObjectAction = new Clickable();
	
	public void AddAsActor(Clickable actor) {
		actors.Add(actor);
	}
	
	public void AddAsSubject(Clickable subject) {
		Subject = subject;
	}
	
	public void AddAsObject(Clickable objectAction) {
		ObjectAction = objectAction;
	}
	
	public void ClearOrder() {
		actors.Clear();
		Subject = null;
		ObjectAction = null;
	}
}