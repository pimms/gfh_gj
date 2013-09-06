using UnityEngine;
using System.Collections;

public class SphereMove : MonoBehaviour {

	public GameObject plane;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = plane.transform.position;
		pos.x += 0.1f;
		plane.transform.position = pos;
	}
}
