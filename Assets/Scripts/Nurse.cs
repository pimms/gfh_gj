using UnityEngine;
using System.Collections;


public class Nurse : Person {

	protected override void Start() {
		base.Start();
	}

	void Update() {
		base.Update();
	}

	public void OnMouseClick(int mouseButton, Order order) {
		Vector3 pos = transform.position();
		//Vector3 subjectPos = order.subject.transform.position();
		Vector3 objectPos = order.objectAction.transform.position();
		
		AStar path = new AStar(pos, objectPos);
	}

	public bool IsActor() {
		return true;
	}
}
