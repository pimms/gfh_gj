using UnityEngine;
using System.Collections;

public class SelectionArrow : MonoBehaviour {
	Vector3 basePos;

	void Start () {
		basePos = transform.localPosition;
	}
	
	void Update () {
		Vector3 pos = new Vector3();
		pos.y = Mathf.Cos(Time.time * 2f);
		transform.localPosition = basePos + pos;

		transform.rotation = Quaternion.Euler(270f, Time.time*90f, 0f);
	}
}
