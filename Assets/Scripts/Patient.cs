using UnityEngine;
using System.Collections;

public class Patient : Person {
	// Levels of sickness: 1 is difficult, 5 is easy.
	public double sickness = 1;

	private bool isInBed;
	public bool IsInBed {
		get { return isInBed; }
	}

	public static int[,] patBedRates = {
		{5, 90, 5},
		{25, 65, 10},
		{20, 60, 20},
		{15, 55, 30},
		{10, 0, 90},
	};
	public static int[,] patOutRates = {
		{30, 70, 0},
		{60, 30, 10},
		{50, 45, 5},
		{40, 45, 15},
		{30, 50, 20},
	};
	public static int[,] patSurgRates = {
		{50, 50},
		{40, 60},
		{30, 70},
		{20, 80},
		{10, 90},			
	};
	public static int[,] patHealthSec = {
		{-10, 2, -40},
		{20, -20, -25},
		{10, -10, -20},
		{10, -5, -15},
		{20, -5, -10},	
	};


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
		isInBed = false;

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
		isInBed = true;
	}

    public void Kill() {
        // TODO: Smoke effect!
        DestroyObject(this);
    }
}
