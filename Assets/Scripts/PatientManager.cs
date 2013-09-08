using UnityEngine;
using System.Collections;

public class PatientManager : MonoBehaviour {
	public GameObject pfPatient;

	private float timer = 5f;

	void Start () {
	
	}
	
	void Update () {
		timer -= Time.deltaTime;
		if (timer <= 0f) {
			SpawnPatient();
			timer = 20f;
		}
	}

	private void SpawnPatient() {
		GameObject gameObj = Instantiate(pfPatient) as GameObject;
		gameObj.transform.position = new Vector3(24f, 1f, 1.6f);

		Patient patient = gameObj.GetComponent<Patient>();
		patient.Randomize();
	}
}
