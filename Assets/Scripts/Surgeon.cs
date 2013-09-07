﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Surgeon : Person {
	const int MAXEXP = 500;
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
		double survivalProbability = ((Laila.exp * 0.001) * (exp * 0.01)) * Bob.sickness;
		double deathProbability = Random.Range(1, 100) * 0.01;
		if (survivalProbability < deathProbability) {
			Bob.isDead = true;
		}
		return Bob.isDead;
	}

	protected override void OnBedReached(Bed bed) {
		// OPERATE THAT FUCKKKKERRRRRRRRRRRRRRRRR
		transform.position = bed.GetPrimaryPosition();
		bool BobIsDead = OperationProbability(Nurse Laila, Patient Bob);
		if (BobIsDead) {
			Laila.exp -= 5;
		} else {
			Laila.exp += 3;
			exp += 10;
		}
	}
}
