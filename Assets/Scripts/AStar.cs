using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AStar {
	List<PathNode> pathNodes;
	List<PathNode> open;
	List<PathNode> closed;

	public AStar(List<PathNode> nodes) {
		pathNodes = nodes;

		open = new List<PathNode>();
		closed = new List<PathNode>();
	}

	public List<PathNode> FindPath(Vector3 from, Vector3 to) {
		PathNode startNode = GetClosest(from);
		PathNode endNode = GetClosest(to);

		PathNode cur = startNode;
		open.Add(cur);

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
				if (closed.Contains(node) && cur.G + 1 >= cur.G) {
					continue;
				} else {
					node.Parent = cur;
					node.G = cur.G + 1;
					node.H = Manhattan(node, endNode);

					if (!open.Contains(node)) {
						open.Add(node);
					}
				}
			}
		}

		return null;
	}

	private PathNode GetClosest(Vector3 position) {
		PathNode closest = open[0];
		float minDist = Vector3.Distance(closest.transform.position, position);

		foreach (PathNode anode in open) {
			float dist = Vector3.Distance(anode.transform.position, position);
			if (dist < minDist) {
				closest = anode;
			}
		}

		return closest;
	}

	private int Manhattan(PathNode node1, PathNode node2) {
		float xdiff = Mathf.Abs(node1.transform.position.x - node2.transform.position.x);
		float zdiff = Mathf.Abs(node1.transform.position.z - node2.transform.position.z);

		return Mathf.RoundToInt(xdiff + zdiff);
	}

	private List<PathNode> BuildPath(PathNode startNode, PathNode endNode) {
		List<PathNode> path = new List<PathNode>();

		PathNode cur = endNode;

		while (cur.Parent != startNode) {
			path.Add(cur);
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
