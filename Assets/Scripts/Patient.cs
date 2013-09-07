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

	public bool IsSubject() {
		return true;
	}
}
