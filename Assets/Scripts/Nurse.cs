using UnityEngine;
using System.Collections;

public class Nurse : Person {

	// Use this for initialization
	void Start() {

	}

	// Update is called once per frame
	void Update() {

	}

	public void OnMouseClick(int mouseButton, Order queue) {
		Vector3 pos = transform.position();
		Vector3 subjectPos = queue.subject.transform.position();
		Vector3 objectPos = queue.objectAction.transform.position();
		
		//PathFind pos(subjectPos, objectPos);
	}

	public bool IsActor() {
		return true;
	}
}
