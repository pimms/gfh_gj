using UnityEngine;
using System.Collections;

public class Patient : Person {
	public GameObject pfSmokeOfDeath;
	public GameObject pfSmokeOfGoingHome;

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
	public static int[,] patHealthMin = {
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

	protected override void Update() {
		base.Update();

		HealthTimerUpdate();
		HealthUpdate();
	}

	void OnGUI() {

		Rect rect = new Rect(0f, 0f, Screen.width, Screen.height);
		Vector2 pos = Camera.main.WorldToScreenPoint(transform.position);

		if (rect.Contains(pos)) {
			float distance = (Camera.main.transform.position - transform.position).magnitude;
			if (distance < 35f) {
				GUI.color = Color.white;
				GUI.backgroundColor = new Color(1f-(health / 100f), (health / 100f), 0f, 1f);

				Rect box = new Rect(pos.x - 400 / distance, Screen.height - pos.y - 1000 / distance, health / 2f, 20f);
				GUI.Button(box, "");
			}
		}
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

	public void Randomize() {
		sickness = Random.Range(1, 5);
		health = Random.Range(50, 100);
	}


	protected override void OnBedReached(Bed bed) {
		// lie down in the fucking bed
		transform.rotation = bed.GetLieRotation();
		transform.position = bed.GetLiePosition();
		isInBed = true;
	}

    public void Kill() {
		AddSmokeEffect(pfSmokeOfDeath);
		Destroy(gameObject);
    }

	public void SendHome() {
		AddSmokeEffect(pfSmokeOfGoingHome);
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
		Debug.Log("Sickness: " + sickness);
		if (sickness < 1 || sickness > 5) {
			return;
		}

		if (isInBed) {
			int dice = Mathf.RoundToInt(Random.Range(0, 1));
			health += Time.deltaTime * patHealthMin[sickness - 1, dice] / 60f;
		} else {
			health += Time.deltaTime * patHealthMin[sickness - 1, 2] / 60f;	
		}

		health = Mathf.Clamp(health, -10f, 100f);

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


	private void AddSmokeEffect(GameObject prefab) {
		Instantiate(prefab, transform.position, transform.rotation);
	}
}
