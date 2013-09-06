using UnityEngine;
using System.Collections;

public class Clickable : MonoBehaviour {
	void Start() {

	}

	void Update() {

	}

	/* When the person is clicked, it must respond. How? Dunno. 
	 * @param mouseButton		0 = left click, 1 = right click
	 */
	public virtual void OnMouseClick(int mouseButton, InputQueue queue) {
		Debug.Log("OnMouseClick() is not overriden in subclass: " + this.ToString());
	}
}
