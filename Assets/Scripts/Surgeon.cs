﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Surgeon : Person {
	public float exp = 100;

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

		currentBed = order.objectAction as Bed;
	}

	public bool IsActor() {
		return true;
	}
	
	public void OperationProbability() {
		List<Nurse> nurses = new List<Nurse>();
		Nurse Laila = new Nurse();
		nurses.Add(Laila);
		
		foreach (Nurse nurse in nurses) {
			
		}
		float probability = Laila.exp;
	}

	protected override void OnBedReached(Bed bed) {
		transform.position = bed.GetPrimaryPosition();
	}
}
