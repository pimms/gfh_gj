using UnityEngine;
using System.Collections;

public class Bed : Clickable {

	void Start () {
	
	}
	
	void Update () {
	
	}

	public override void OnMouseClick(int mouseButton, InputOrder inOrder) {
		if (inOrder.order.actors.Count != 0 
		&&  inOrder.order.subject != null) {
			inOrder.AddAsObject(this);

			inOrder.PerformOrder();
			inOrder.Clear();
		}
	}

	public virtual Vector3 GetLiePosition() {
		Vector3 liePos = new Vector3(0f, 0.7f, 0f);
		liePos += transform.position;
		return liePos;
	}

	public virtual Quaternion GetLieRotation() {
		Vector3 euler = transform.rotation.eulerAngles;
		euler.z = -90f;
		return Quaternion.Euler(euler);
	}

	public virtual Vector3 GetPrimaryPosition() {
		Vector3 primary = new Vector3(0f, 0f, -1f);
		primary += transform.position;
		return primary;
	}

	public virtual Vector3 GetAssistPosition() {
		Vector3 assist = new Vector3(0f, 0f, 1f);
		assist += transform.position;
		return assist;
	}
}
