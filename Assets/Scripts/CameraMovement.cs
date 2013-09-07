using UnityEngine;
using System.Collections;

/* Class used for moving the camera in an RTS-like
 * fashion.
 */
public class CameraMovement : MonoBehaviour {
	

	public float movementFactor = 0.1f;

	private float lastMouseX;
	private float lastMouseY;
	private Vector3 cameraPos;
	float angle;

	void Start () {
		
	}
	
	void Update () {
		const float panSize = 30;
		float mouseX = Input.mousePosition.x;
		float mouseY = Input.mousePosition.y;

  
		if (mouseX < panSize || mouseX > Screen.width - panSize || mouseY < panSize || mouseY > Screen.height - panSize ) {
			Vector3 panVector = new Vector3((mouseX-Screen.width/2)/1000, 0f , (mouseY-Screen.height/2)/1000);
			panVector = transform.TransformDirection(panVector);
			panVector.y = 0f;
			transform.Translate( panVector , Space.World);
		} else {

		}
	}
}
