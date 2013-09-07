using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Person : Clickable {
	protected List<PathNode> currentPath;
	
	protected virtual void Start () {
		currentPath = new List<PathNode>();
	}
	
	protected virtual void Update () {
		if (currentPath.Count != 0) {
			FollowPath(currentPath);
		}
	}

	public void FollowPath(List<PathNode> path) {
		if (path == null || path.Count == 0) {
			return;
		}

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
		pos += diff * Time.deltaTime * 10f;
		transform.position = pos;
	}
}
