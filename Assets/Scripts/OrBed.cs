using UnityEngine;
using System.Collections;

public class OrBed : Bed {
<<<<<<< HEAD
	Patient Gey 
=======
	// set externally like wild beasts
	public Surgeon surgeon;
	public Nurse nurse;

>>>>>>> a52ff5d951afe1221ead2da5a7b3192edbe447ca
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
