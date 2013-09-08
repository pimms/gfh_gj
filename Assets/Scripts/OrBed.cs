using UnityEngine;
using System.Collections;

public class OrBed : Bed {
	// set externally like wild beasts
	public Surgeon surgeon;


	void Start () {
	
	}

	void Update () {
	
	}
	

	public override void RemovePerson(Person person) {
		if (person == surgeon) {
			surgeon = null;
		} else if (person == nurse) {
			nurse = null;
        }
        else if (person == patient) {
            patient = null;
        }
	}
}
