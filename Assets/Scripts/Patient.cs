using UnityEngine;
using System.Collections;

public class Patient : Person {

	void Start() {
		base.Start();
	}

	void Update() {
		base.Update();
	}

	public void OnMouseClick(int mouseButton, InputOrder queue) {

	}
	
	public void NurseTrue() {
		
	}
	
	public void SurgeonTrue() {
		
	}

	public bool IsSubject() {
		return true;
	}
}
