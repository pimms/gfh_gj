using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Surgeon : Person {
	const int MAXEXP = 200;
	public double exp = 10;

    public bool patientInBed;
    public double surgeryPerformance = 0;
    const int OPERATIONMAXTIME = 5;
    float startTime;
    float xpCoeff;

	protected override void Start () {
		base.Start();
	}
	
	void Update () {
		base.Update();
		energyUpdate();
		Vector3 pos = transform.position;
		pos.y = 1f;
		transform.position = pos;

        if (currentBed != null) {
			if (currentBed.patient != null) {
				patientInBed = currentBed.patient.IsInBed;
			} else {
				patientInBed = false;
			}
		}

        startTime = Time.realtimeSinceStartup;
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
	
	public override void OnMouseClick(int mouseButton, InputOrder inOrder) {
		inOrder.AddAsActor(this);
		Debug.Log("BITCH DON'T CLICK ME");
	}

	public override void BeginPerform(Order order) {
		shouldGoToBed = false;
		RemoveFromSurgery();

		currentBed = order.objectAction as Bed;
		/*
		OrBed orBed = currentBed as OrBed;
		if (orBed != null) {
			if (orBed.surgeon != null) {
				return;
			}
			orBed.surgeon = this;
		}*/

		Vector3 pos = transform.position;
		Vector3 objectPos = order.objectAction.transform.position;

		AStar astar = new AStar();
		currentPath = astar.FindPath(pos, objectPos);
	}

	public bool IsActor() {
		return true;
	}
	
	public bool OperationProbability(Nurse Laila, Patient Bob) {
        int random = Random.Range(0, 100);
        // surgeryPerformance += (Time.deltaTime / (OPERATIONMAXTIME)) * currentBed.nurse.efficiency;
        if (random < Patient.patOutRates[Bob.sickness, 0]) {
            return true;
        } else {
            return false;
        }
	}

	protected override void OnBedReached(Bed bed) {
		transform.position = bed.GetPrimaryPosition();
        float operationEndTime = startTime + 5;

        //OrBed orBed = bed as OrBed;
        //if (orBed == null) return;
        Debug.Log("Get in that bed bitch! " + currentBed.patient.IsInBed);
        Debug.Log("NURSE: " + currentBed.nurse.exp);
        Debug.Log("Patient: " + currentBed.patient.health);
        if (OperationProbability(currentBed.nurse, currentBed.patient)) {
            currentBed.nurse.exp -= 5;
            currentBed.patient.Kill();
        } else {
            currentBed.nurse.exp += 3;
            exp += 10;
        }
	}
}
