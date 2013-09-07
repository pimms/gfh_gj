using UnityEngine;
using System.Collections;


public class Nurse : Person {

	protected override void Start() {
		base.Start();
	}

	void Update() {
		base.Update();
	}

	public override void OnMouseClick(int mouseButton, InputOrder inOrder) {
		inOrder.AddAsActor(this);
	}

	public override void BeginPerform(Order order) {
		Vector3 pos = transform.position;
		Vector3 subjectPos = order.subject.transform.position;
		Vector3 objectPos = order.objectAction.transform.position;

		//AStar path = new AStar(pos, objectPos);
		AStar path = new AStar();
		path.FindPath(pos, objectPos);
	}

	public bool IsActor() {
		return true;
	}
}
