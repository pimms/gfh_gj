using UnityEngine;
using System.Collections;

/* Class used for moving the camera in an RTS-like
 * fashion.
 */
public class CameraMovement : MonoBehaviour {
	public float movementFactor = 0.1f;

	private float lastMouseX;
	private float lastMouseY;

	void Start () {
		
	}
	
	void Update () {
		if (Input.GetMouseButton(2)) {
			float mouseX = Input.mousePosition.x;
			float mouseY = Input.mousePosition.y;

			Vector3 euler = transform.rotation.ToEuler();

			float deltaX = mouseX - lastMouseX;
			float deltaY = mouseY - lastMouseY;

			// TODO: SOME FANCY SHIT
			
			lastMouseX = mouseX;
			lastMouseY = mouseY;
		} else {
			lastMouseX = Input.mousePosition.x;
			lastMouseY = Input.mousePosition.y;
		}
	}
}
