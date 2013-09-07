using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Surgeon : Person {
	const int MAXEXP = 200;
	public double exp = 10;

	protected override void Start () {
		base.Start();
	}
	
	void Update () {
		base.Update();
	}

	public override void OnMouseClick(int mouseButton, InputOrder inOrder) {
		inOrder.AddAsActor(this);
		Debug.Log("BITCH DON'T CLICK ME");
	}

	public override void BeginPerform(Order order) {
		shouldGoToBed = false;
		RemoveFromSurgery();

		currentBed = order.objectAction as Bed;
		OrBed orBed = currentBed as OrBed;
		if (orBed != null) {
			if (orBed.surgeon != null) {
				return;
			}
			orBed.surgeon = this;
		}

		Vector3 pos = transform.position;
		Vector3 objectPos = order.objectAction.transform.position;

		AStar astar = new AStar();
		currentPath = astar.FindPath(pos, objectPos);
	}

	public bool IsActor() {
		return true;
	}
	
	public bool OperationProbability(Nurse Laila, Patient Bob) {
        int random = Random.Range(1, 101);
        if (random < Patient.patOutRates[Bob.sickness, 0]) {
            return true;
        } else {
            return false;
        }
	}

	protected override void OnBedReached(Bed bed) {
		float oldY = transform.position.y;
		transform.position = bed.GetPrimaryPosition();
		transform.position += new Vector3(0f, oldY, 0f);

        OrBed orBed = bed as OrBed;
        if (orBed == null) return;

        //if (OperationProbability(orBed.nurse, orBed.patient)) {
            //orBed.nurse.exp -= 5;
            orBed.patient.Kill();
		//} else {
			//orBed.nurse.exp += 3;
			//exp += 10;
		//}

	}
}
