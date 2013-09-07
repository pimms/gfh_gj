using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathNodeMapper : MonoBehaviour {
	public List<PathNode> pathNodes;


	void Start() {
		pathNodes = new List<PathNode>();

		if (!GetPathNodes()) {
			Debug.LogError("No path nodes found");
			return;
		}

		MapNeighbours();
	}

	private bool GetPathNodes() {
		pathNodes.Clear();

		foreach (GameObject go in GameObject.FindGameObjectsWithTag("Path Nodes")) {
			pathNodes.Add(go.GetComponent<PathNode>());
		}

		return pathNodes.Count != 0;
	}

	private void MapNeighbours() {
		/* Map the node-relations in a classy
		 * O(n^2) fashion :)
		 */
		List<PathNode> done = new List<PathNode>();
		int i = 0;

		foreach (PathNode node in pathNodes) {
			node.name = "Path Node " + (++i);

			foreach (PathNode neighbour in pathNodes) {
				if (done.Contains(node)) continue;
				if (node == neighbour) continue;

				if (IsClearPath(node, neighbour)) {
					LinkNodes(node, neighbour);
				}
			}

			done.Add(node);
		}
	}

	private void LinkNodes(PathNode node1, PathNode node2) {
		if (!node1.neighbours.Contains(node2)) {
			node1.neighbours.Add(node2);
		}

		if (!node2.neighbours.Contains(node1)) {
			node2.neighbours.Add(node1);
		}
	}

	private bool IsClearPath(PathNode node1, PathNode node2) {
		float dist = Vector3.Distance(node1.transform.position, node2.transform.position);
		Vector3 direction = node2.transform.position - node1.transform.position;
		direction.Normalize();

		return !Physics.Raycast(node1.transform.position, direction, dist, int.MaxValue);
	}
}
