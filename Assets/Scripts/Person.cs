using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Person : Clickable {
	private List<PathNode> currentPath;

	protected virtual void Start () {
		currentPath = new List<PathNode>();

		List<PathNode> allNodes = new List<PathNode>();

		foreach (GameObject g in GameObject.FindGameObjectsWithTag("Path Nodes")) {
			allNodes.Add(g.GetComponent<PathNode>());
		}

		Vector3 destination = new Vector3(23f, 1f, 37f);

		Timer t = new Timer();

		AStar astar = new AStar();
		currentPath = astar.FindPath(transform.position, destination);

		t.End("'find path'");
		Debug.Log("PATH LENGTH: " + currentPath.Count);
	}
	
	protected virtual void Update () {
		FollowPath(currentPath);
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
