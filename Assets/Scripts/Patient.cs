using UnityEngine;
using System.Collections;

public class Patient : Person {

	protected override void Start() {
		base.Start();
		walkSpeed = 6f;
	}

	void Update() {
		base.Update();

		if (shouldGoToBed) {
			Vector3 diff = currentBed.transform.position - transform.position;
			diff.y = 0f;

			if (diff.magnitude < 0.3f) {
				LieInBed(currentBed);
				shouldGoToBed = false;
			}

			diff.Normalize();

			Vector3 pos = transform.position;
			pos += diff * Time.deltaTime * walkSpeed;
			transform.position = pos;
		}
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

	private void LieInBed(Bed bed) {
		transform.rotation = bed.GetLieRotation();
		transform.position = bed.GetLiePosition();
	}
}
