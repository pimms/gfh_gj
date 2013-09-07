using UnityEngine;
using System.Collections;

public class Patient : Person {

	protected override void Start() {
		base.Start();
		walkSpeed = 6f;
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
		base.BeginPerform(order);

		transform.rotation = Quaternion.identity;
		currentBed = order.objectAction as Bed;

		AStar astar = new AStar();
		currentPath = astar.FindPath(transform.position, order.objectAction.transform.position);
	}

	public bool IsSubject() {
		return true;
	}

	public override void OnPathCompleted() {
		base.OnPathCompleted();

		// Go to bed if bed is set
		shouldGoToBed = (currentBed != null);
	}

	protected override void OnBedReached(Bed bed) {
		// lie down in the fucking bed
		transform.rotation = bed.GetLieRotation();
		transform.position = bed.GetLiePosition();
	}
}
