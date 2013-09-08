using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Person : Clickable {
	public float health = 100f;

	// Used when the person has reached an eventual bed
	protected bool shouldGoToBed = false;
	protected Bed currentBed;

	// Used when walking to a position
	protected bool shouldGoToCustomDest;
	protected Vector3 customDestination;

	protected float walkSpeed = 10f;
	protected List<PathNode> currentPath;

	private bool movedLastFrame;
	private Color dispColor;
	
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

	public void GoToPosition(Vector3 position) {
		customDestination = position;

		// See if we need pathfinding
		Vector3 pos1 = transform.position;
		Vector3 pos2 = position;
		pos1.y = pos2.y = 0.2f; // fuckthepolice.jpg

		if (!PathNodeMapper.IsClearPath(pos1, pos2)) {
			AStar astar = new AStar();
			List<PathNode> path = astar.FindPath(transform.position, customDestination);

			currentPath = path;
		}

		// DROP IT LIKE IT'S HOT
		RemoveFromSurgery();
		shouldGoToBed = false;

		shouldGoToCustomDest = true;
	}

	public virtual void OnPathCompleted() {
		shouldGoToBed = (currentBed != null);
	}

	protected virtual void Update() {
		if (currentPath != null && currentPath.Count != 0) {
			FollowPath(currentPath);
		} else if (shouldGoToCustomDest) {
			GoToCustomPosition();	
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

	protected void GoToCustomPosition() {
		Vector3 diff = customDestination - transform.position;
		diff.y = 0f;

		if (diff.magnitude < 0.3f) {
			OnPathCompleted();
			shouldGoToCustomDest = false;
			return;
		}

		movedLastFrame = true;

		diff.Normalize();

		Vector3 pos = transform.position;
		pos += diff * Time.deltaTime * walkSpeed;
		transform.position = pos;
	}
	
	protected void RemoveFromSurgery() {
		if (currentBed != null) {
			currentBed.RemovePerson(this);
			currentBed = null;
		}
	}
}
