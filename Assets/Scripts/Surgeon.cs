using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Surgeon : Person {
	// Max exp = ???
	public double exp = 100;

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
		Vector3 pos = transform.position;
		Vector3 subjectPos = order.subject.transform.position;
		Vector3 objectPos = order.objectAction.transform.position;

		Debug.Log("My patient is at " + objectPos.ToString());

		AStar astar = new AStar();
		currentPath = astar.FindPath(pos, objectPos);
	}

	public bool IsActor() {
		return true;
	}
	
	public bool OperationProbability(Nurse Laila, Patient Bob) {
		double survivalProbability = ((Laila.exp * 0.001) * (exp * 0.01)) * Bob.sickness;
		int deathProbability = Random.Range(1, 100) * 0.01;
		if (survivalProbability < deathProbability) {
			Bob.isDead = true;
		}
		return Bob.isDead;
	}
	protected override void OnBedReached(Bed bed) {
		OperationProbability();
		// OPERATE THAT FUCKKKKERRRRRRRRRRRRRRRRR
	}
}
