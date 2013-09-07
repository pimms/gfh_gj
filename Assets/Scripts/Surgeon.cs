﻿using UnityEngine;
using System.Collections;

public class Surgeon : Person {
	protected override void Start () {
		base.Start();
	}
	
	void Update () {
		base.Update();
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
