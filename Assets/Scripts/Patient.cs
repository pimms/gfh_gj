﻿using UnityEngine;
using System.Collections;

public class Patient : Person {

	protected override void Start() {
		base.Start();
	}

	void Update() {
		base.Update();
	}

	public override void OnMouseClick(int mouseButton, InputOrder inOrder) {
		if (inOrder.order.actors.Count != 0) {
			inOrder.AddAsSubject(this);
		}
	}
	
	public override void BeginPerform(Order order) {
		Vector3 pos = transform.position;
		Vector3 objectPos = order.objectAction.transform.position;

		//AStar path = new AStar(pos, objectPos);
		AStar path = new AStar();
		path.FindPath(pos, objectPos);
	}

	public override void BeginPerform(Order order) {
		base.BeginPerform(order);

		AStar astar = new AStar();
		currentPath = astar.FindPath(transform.position, order.objectAction.transform.position);
	}

	public bool IsSubject() {
		return true;
	}
}
