using UnityEngine;
using System.Collections;

public class Patient : Person {
	// Levels of sickness: 1 is difficult, 5 is easy.
	public int sickness = 1;

	private bool isInBed;
	public bool IsInBed {
		get { return isInBed; }
	}

	private float healthTimer;

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

		healthTimer = UnityEngine.Random.Range(30f, 60f);
	}

	void Update() {
		base.Update();

		HealthTimerUpdate();
		HealthUpdate();
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
		Destroy(gameObject);
    }

	public void SendHome() {
		// TODO: Increase some score or some shit
		Destroy(gameObject);
	}


	private void HealthTimerUpdate() {
		healthTimer -= Time.deltaTime;
		if (healthTimer < 0) {
			healthTimer = UnityEngine.Random.Range(30f, 60f);

			int dice = UnityEngine.Random.Range(0, 99);
			int[,] arr = (isInBed) ? (patBedRates) : (patOutRates);

			if (dice < arr[sickness, 0]) {
				DecreaseHealth();
			} else if (dice < arr[sickness, 1]) {
				// Do nothing
			} else {
				IncreaseHealth();
			}
		}
	}

	private void HealthUpdate() {
		if (sickness < 1 || sickness > 5) {
			return;
		}

		int dice = Random.Range(0, 1);

		health += (float)patHealthSec[sickness, dice] * Time.deltaTime;
		health = Mathf.Clamp(health, -100f, 100f);

		if (health <= 0f) {
			Kill();
		}
	}


	private void DecreaseHealth() {
		sickness--;

		if (sickness <= 0) {
			Kill();
		}
	}

	private void IncreaseHealth() {
		sickness++;
		if (sickness > 5) {
			SendHome();
		}
	}
}
