using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Person : Clickable {
	public int health = 100;

	// Used when the person has reached an eventual bed
	protected bool shouldGoToBed = false;
	protected Bed currentBed;

	protected float walkSpeed = 10f;
	protected List<PathNode> currentPath;

	private bool movedLastFrame;
	private Color dispColor;
	
	void OnGUI () {
		
		Rect rect = new Rect(0f,0f, Screen.width, Screen.height );
		Vector2 pos = Camera.main.WorldToScreenPoint( transform.position );
		
		if( rect.Contains(pos) ){
			float distance = ( Camera.main.transform.position - transform.position).magnitude;
			if ( distance < 35f){
				GUI.Box(new Rect(pos.x-400/distance, Screen.height - pos.y-1000/distance, 800/distance, 600/distance), health.ToString() ); //health.ToString()
			}
		}
	}
	
	protected virtual void Start () {
		currentPath = new List<PathNode>();
	}

	public void FollowPath(List<PathNode> path) {
		if (path == null || path.Count == 0) {
			return;
		}

		movedLastFrame = true;

		Vector3 dest = path[0].transform.position;
		dest.y = transform.position.y;

		float length = Vector3.Distance(transform.position, dest);
		if (length < 0.3f) {
			path.RemoveAt(0);
			FollowPath(path);
			return;
		}

		Vector3 diff = dest - transform.position;
		diff.y = 0f;
		diff.Normalize();

		Vector3 pos = transform.position;
		pos += diff * Time.deltaTime * walkSpeed;
		transform.position = pos;
	}

	public virtual void OnPathCompleted() {
		shouldGoToBed = (currentBed != null);
	}


	protected virtual void Update() {
		if (currentPath.Count != 0) {
			FollowPath(currentPath);
		} else if (movedLastFrame) {
			OnPathCompleted();
			movedLastFrame = false;
		}

		if (shouldGoToBed) {
			GoToBed();
		}
	}

	protected virtual void OnBedReached(Bed bed) {
		Debug.LogWarning("OnBedReached() not overriden in class " + this.ToString());
	}


	protected void GoToBed() {
		Vector3 diff = currentBed.transform.position - transform.position;
		diff.y = 0f;

		if (diff.magnitude < 0.3f) {
			OnBedReached(currentBed);
			shouldGoToBed = false;
		}

		diff.Normalize();

		Vector3 pos = transform.position;
		pos += diff * Time.deltaTime * walkSpeed;
		transform.position = pos;
	}
	
	protected void RemoveFromSurgery() {
		OrBed orBed = currentBed as OrBed;
		if (orBed != null) {
			orBed.RemovePerson(this);
		}

		currentBed = null;
	}
}
