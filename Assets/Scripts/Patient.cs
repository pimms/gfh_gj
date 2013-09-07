using UnityEngine;
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
	
	public override void BeginPerform(Order order) {
		Vector3 pos = transform.position;
		Vector3 objectPos = order.objectAction.transform.position;

		//AStar path = new AStar(pos, objectPos);
		AStar path = new AStar();
		path.FindPath(pos, objectPos);
	}

	public bool IsSubject() {
		return true;
	}
}
