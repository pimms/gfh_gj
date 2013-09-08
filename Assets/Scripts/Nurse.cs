using UnityEngine;
using System.Collections;


public class Nurse : Person {
	const double MAXEXP = 100;
	public double exp = 10;
	
	protected float energyTimer;
	
	void OnGUI() {

		Rect rect = new Rect(0f, 0f, Screen.width, Screen.height);
		Vector2 pos = Camera.main.WorldToScreenPoint(transform.position);

		if (rect.Contains(pos)) {
			float distance = (Camera.main.transform.position - transform.position).magnitude;
			if (distance < 35f) {
				GUI.color = Color.white;
				GUI.backgroundColor = new Color(1f, 1f, 1f, 1f);

				Rect box = new Rect(pos.x - 400 / distance, Screen.height - pos.y - 1000 / distance, health / 2f, 10f);
				GUI.Button(box, "");
			}
		}
	}
	
	
	protected override void Start() {
		base.Start();
		walkSpeed = 8f;
	}

	protected override void Update() {
		base.Update();
		energyUpdate();

		Vector3 pos = transform.position;
		pos.y = 1f;
		transform.position = pos;
	}
	
	public float efficieny(){
		return (0.75f+health/200f);
		
	}
	
	public void energyUpdate() {
		
		float delta = Time.deltaTime;
		
		Sofa sofaPtr = currentBed as Sofa;
		
		if ( sofaPtr != null) {
				if (health < 100f) {
				health += (100f/20f) * Time.deltaTime;
			}
			
		} else if (currentBed != null && currentBed.patient != null ) {
			if (health > 0f) {
				health -= (100f/180f) * Time.deltaTime;
			}
			
		} else {
			if (health < 100f) {
				health += (100f/90f) * Time.deltaTime;
			}
		}

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

    public float XpCoefficient()
    {
        return (1f + (0.6f - (1f / (Mathf.Sqrt((float)exp) / 1000f) + 1)));
    }
}
