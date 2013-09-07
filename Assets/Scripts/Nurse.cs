using UnityEngine;
using System.Collections;


public class Nurse : Person {

	void Start() {
		base.Start();
	}

	void Update() {
		base.Update();
	}

	public void OnMouseClick(int mouseButton, Order order) {
		Vector3 pos = transform.position();
		Vector3 subjectPos = order.subject.transform.position();
		Vector3 objectPos = order.objectAction.transform.position();
		
		//PathFind pos(subjectPos, objectPos);
	}

	public bool IsActor() {
		return true;
	}
}
