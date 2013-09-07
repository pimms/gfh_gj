using UnityEngine;
using System.Collections;


public class Nurse : Person {
	const double MAXEXP = 100;
	public double exp = 10;

	protected override void Start() {
		base.Start();
	}

	void Update() {
		base.Update();
	}

	public override void OnMouseClick(int mouseButton, InputOrder inOrder) {
		inOrder.AddAsActor(this);
	}

	public override void BeginPerform(Order order) {
		shouldGoToBed = false;

		// Abort previous surgery
		RemoveFromSurgery();

		// Is this a surgery? 
		currentBed = order.objectAction as Bed;
		OrBed orBed = currentBed as OrBed;
		if (orBed != null) {
			if (orBed.nurse != null) {
				return;
			}
			orBed.nurse = this;
		}

		// Pathfinding!
		Vector3 pos = transform.position;
		Vector3 objectPos = order.objectAction.transform.position;

		AStar astar = new AStar();
		currentPath = astar.FindPath(pos, objectPos);

		currentBed = order.objectAction as Bed;
	}

	protected override void OnBedReached(Bed bed) {
		transform.position = bed.GetAssistPosition();
	}

	public bool IsActor() {
		return true;
	}
}
