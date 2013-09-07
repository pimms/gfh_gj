using UnityEngine;
using System.Collections;

public class OrBed : Bed {
	// set externally like wild beasts
	public Surgeon surgeon;
	public Nurse nurse;

	void Start () {
	
	}

	void Update () {
	
	}

	public void RemovePerson(Person person) {
		if (person == surgeon) {
			surgeon = null;
		} else if (person == nurse) {
			nurse = null;
		}
	}
}
