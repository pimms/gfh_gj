using UnityEngine;
using System.Collections;

public class Patient : Person {

	protected override void Start() {
		base.Start();
	}

	void Update() {
		base.Update();
	}

	public void OnMouseClick(int mouseButton, Order queue) {

	}
	
	public void NurseTrue() {
		
	}
	
	public void SurgeonTrue() {
		
	}

	public bool IsSubject() {
		return true;
	}
}
