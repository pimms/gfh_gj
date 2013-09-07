﻿using UnityEngine;
using System.Collections;

public class Patient : Person {

	protected override void Start() {
		base.Start();
	}

	void Update() {
		base.Update();
	}

	public override void OnMouseClick(int mouseButton, InputOrder inOrder) {
		if (inOrder.order.actors.Count != 0) {
			inOrder.AddAsSubject(this);
		}
	}
	
	public void NurseTrue() {
		
	}
	
	public void SurgeonTrue() {
		
	}

	public bool IsSubject() {
		return true;
	}
}
