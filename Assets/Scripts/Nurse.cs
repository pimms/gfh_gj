using UnityEngine;
using System.Collections;

public class Nurse : Person {

	void Start() {
		base.Start();
	}

	void Update() {
		base.Update();
	}

	public void OnMouseClick(int mouseButton, InputOrder queue) {

	}

	public bool IsActor() {
		return true;
	}
}
