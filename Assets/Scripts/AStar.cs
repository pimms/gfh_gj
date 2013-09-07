using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AStar {
	List<PathNode> open;
	List<PathNode> closed;

	private const float gPerStep = 10f;

	public AStar() {
		open = new List<PathNode>();
		closed = new List<PathNode>();
	}

	public List<PathNode> FindPath(Vector3 from, Vector3 to) {
		PathNode startNode = GetClosest(from);
		PathNode endNode = GetClosest(to);

		Debug.Log("Going from " + startNode.name + " to " + endNode.name);

		PathNode cur = startNode;
		open.Add(cur);

		foreach (PathNode node in PathNode.allNodes) {
			node.H = Distance(node, endNode);
		}

		while (open.Count != 0) {
			cur = open[0];
			foreach (PathNode node in open) {
				if (node.F < cur.F) {
					cur = node;
				}
			}

			if (cur == endNode) {
				List<PathNode> path = BuildPath(startNode, endNode);
				Reset();
				return path;
			}

			open.Remove(cur);
			closed.Add(cur);

			foreach (PathNode node in cur.neighbours) {
				float tentG = Distance(node, cur);
				if (open.Contains(node)) {
					if (cur.G + tentG < node.G) {
						node.Parent = cur;
						node.G = cur.G + tentG;
					}
				} else if (!closed.Contains(node)) {
					open.Add(node);
					node.Parent = cur;
					node.G = cur.G + tentG;
				}
			}
		}

		return null;
	}

	private PathNode GetClosest(Vector3 position) {
		PathNode closest = PathNode.allNodes[0];
		float minDist = Vector3.Distance(closest.transform.position, position);

		foreach (PathNode anode in PathNode.allNodes) {
			float dist = Vector3.Distance(anode.transform.position, position);
			if (dist < minDist) {
				closest = anode;
				minDist = dist;
			}
		}

		return closest;
	}

	private float Distance(PathNode node1, PathNode node2) {
		return Vector3.Distance(node1.transform.position, node2.transform.position);
	}

	private List<PathNode> BuildPath(PathNode startNode, PathNode endNode) {
		List<PathNode> path = new List<PathNode>();

		PathNode cur = endNode;

		while (cur.Parent != null) {
			path.Add(cur);
			cur = cur.Parent;
		}

		path.Add(startNode);

		path.Reverse();
		return path;
	}

	private void Reset() {
		foreach (PathNode node in open) {
			node.Reset();
		}

		foreach (PathNode node in closed) {
			node.Reset();
		}

		open.Clear();
		closed.Clear();
	}
}
