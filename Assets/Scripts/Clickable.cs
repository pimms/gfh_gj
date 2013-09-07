using UnityEngine;
using System.Collections;

public class Clickable : MonoBehaviour {
	public GameObject pfSelectionArrow;
	private GameObject selectionArrow;


	void Start() {

	}

	void Update() {

	}

	/* When the person is clicked, it must respond. How? Dunno. 
	 * @param mouseButton		0 = left click, 1 = right click
	 */
	public virtual void OnMouseClick(int mouseButton, InputOrder queue) {
		Debug.Log("OnMouseClick() is not overriden in subclass: " + this.ToString());
	}
	
	/* Called every tick while the mouse hovers over the object, used for highlights
	 */
	public virtual void OnMouseHover() {

	}

	public virtual void BeginPerform(Order order) {
		Debug.LogError("BeginPerform() not overriden in " + this.ToString());
	}

	/* Can the clickable object act upon other objects?
	 */
	public virtual bool IsActor() {
		return false;
	}

	/* Can the clickable object be performed actions upon?
	 */
	public virtual bool IsSubject() {
		return false;
	}

	/* Can the clickable object be used in an action? (beds, etc.)
	 */
	public virtual bool IsObject() {
		return false;
	}


	public void OnSelect() {
		CreateSelectionArrow();
	}

	public void OnDeselect() {
		Destroy(selectionArrow);
	}

	private void CreateSelectionArrow() {
		if (selectionArrow != null) return;

		selectionArrow = Instantiate(pfSelectionArrow) as GameObject;
		selectionArrow.transform.parent = transform;
		selectionArrow.transform.localPosition = new Vector3(0f, 4f, 0f);
	}
}
