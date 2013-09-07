using UnityEngine;
using System.Collections;

public class Patient : Person {

	public static int patBedRates[,];		
	public static int patOutRates[,];
	public static int patSurgRates[,];
	public static int patHealthSec[,];

	protected override void Start() {
		base.Start();
		walkSpeed = 6f;
	}

	void Update() {
		base.Update();
	}

	public override void OnMouseClick(int mouseButton, InputOrder inOrder) {
		inOrder.AddAsSubject(this);
	}

	public override void BeginPerform(Order order) {
		shouldGoToBed = false;
		transform.rotation = Quaternion.identity;
		currentBed = order.objectAction as Bed;

		AStar astar = new AStar();
		currentPath = astar.FindPath(transform.position, order.objectAction.transform.position);
	}

	public bool IsSubject() {
		return true;
	}

	protected override void OnBedReached(Bed bed) {
		// lie down in the fucking bed
		transform.rotation = bed.GetLieRotation();
		transform.position = bed.GetLiePosition();
	}
}
