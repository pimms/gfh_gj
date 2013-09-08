using UnityEngine;
using System.Collections;


public class Nurse : Person {
	const double MAXEXP = 100;
	public double exp = 10;

	protected override void Start() {
		base.Start();

		walkSpeed = 8f;
	}

	void Update() {
		base.Update();

		Vector3 pos = transform.position;
		pos.y = 1f;
		transform.position = pos;
	}

	public override void OnMouseClick(int mouseButton, InputOrder inOrder) {
		inOrder.AddAsActor(this);
	}

	public override void BeginPerform(Order order) {
		shouldGoToBed = false;

		// Abort previous surgery
		RemoveFromSurgery();
		
		/*
		// Is this a surgery? 
		currentBed = order.objectAction as Bed;
		OrBed orBed = currentBed as OrBed;
		if (orBed != null) {
			if (orBed.nurse != null) {
				return;
			}
			orBed.nurse = this;
		}
		*/

		// Pathfinding!
		Vector3 pos = transform.position;
		Vector3 objectPos = order.objectAction.transform.position;

		AStar astar = new AStar();
		currentPath = astar.FindPath(pos, objectPos);

		currentBed = order.objectAction as Bed;
	}

	protected override void OnBedReached(Bed bed) {
		//float oldY = transform.position.y;
		transform.position = bed.GetAssistPosition();
		transform.position += new Vector3(0f, 1f - 0.144761f, 0f);
	}

	public bool IsActor() {
		return true;
	}
}
