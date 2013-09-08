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

		Vector3 pos = transform.position;
		pos.y = 1f;
		transform.position = pos;

        if (currentBed) { patientInBed = currentBed.patient.IsInBed; }

        startTime = Time.realtimeSinceStartup;
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
