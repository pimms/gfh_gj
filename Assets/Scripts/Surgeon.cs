using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Surgeon : Person {
	const int MAXEXP = 200;
	public double exp = 10;

    public float surgeryPerformance = 0f;
    const int OPERATIONMAXTIME = 5;

    float xpCoeff;
    float basePercent;
    protected float operationTimer;
    float survivalRate;

	protected override void Start () {
		base.Start();
	}
	
	void Update () {
		base.Update();

		Vector3 pos = transform.position;
		pos.y = 1f;
		transform.position = pos;

        operationTimer -= Time.deltaTime;
        if (currentBed != null && currentBed.nurse != null) {
            surgeryPerformance += (Time.deltaTime / (OPERATIONMAXTIME)) * (currentBed.nurse.health + health);
            xpCoeff = (1 + (1 - (1 / ((Mathf.Sqrt((float)currentBed.nurse.exp) / 1000) + 1)))) * (1 + (1 - (1 / ((Mathf.Sqrt((float)exp) / 1000) + 1))));
        }
        if (currentBed != null && currentBed.patient != null) {
            survivalRate = surgeryPerformance * (basePercent * (currentBed.patient.health / 100) * xpCoeff);
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
	
	public float OperationProbability(Nurse Laila, Patient Bob) {
        int random = Random.Range(0, 100);
        
        

        if (random < Patient.patOutRates[Bob.sickness, 0]) {
            return Patient.patSurgRates[Bob.sickness, 0];
            //return true;
        } else {
            return Patient.patSurgRates[Bob.sickness, 1];
            //return false;
        }
	}

	protected override void OnBedReached(Bed bed) {
		transform.position = bed.GetPrimaryPosition();
        operationTimer = 5;

        Debug.Log("Get in that bed bitch! " + currentBed.patient.IsInBed);
        Debug.Log("NURSE: " + currentBed.nurse.exp);
        Debug.Log("Patient: " + currentBed.patient.health);
        while (operationTimer <= 0) { Debug.Log("Waiting to fisish surgery."); }

        basePercent = OperationProbability(currentBed.nurse, currentBed.patient);

        /*
        if (OperationProbability(currentBed.nurse, currentBed.patient)) {
            currentBed.nurse.exp -= 5;
            currentBed.patient.Kill();
        } else {
            currentBed.nurse.exp += 3;
            exp += 10;
        }
        */
	}
}
