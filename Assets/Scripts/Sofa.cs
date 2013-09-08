using UnityEngine;
using System.Collections;

public class Sofa : Bed {
	public Surgeon surgeon = null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public override void RemovePerson(Person person) {
		if (person == nurse) {
			nurse = null;
        } else if (person == patient) {
            patient = null;
        } else {
			surgeon = null;
		}
	}
}
