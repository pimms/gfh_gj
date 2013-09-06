using UnityEngine;
using System.Collections;

public class PathNodeMapper : MonoBehaviour {
	public GameObject initialNode;
	public GameObject pfPathNode;

	GameObject[,] setNodes;

	void Start() {
		if (initialNode == null) {
			Debug.LogError("No inital node set");
			return;
		}

		setNodes = new GameObject[64,64];
		CreateNeighbours(initialNode);
		Debug.Log("DONE, LOL");
	}

	private void CreateNeighbours(GameObject node) {
		int ix = Mathf.RoundToInt(node.transform.position.x);
		int iz = Mathf.RoundToInt(node.transform.position.z);
		Debug.Log("Creating node: " + ix + ", " + iz);

		if (setNodes[ix, iz] == null) {
			setNodes[ix, iz] = node;
		} else {
			return;
		}

		int[,] arrDir = new int[4, 2] {
			{1, 0}, {-1, 0},
			{0, 1}, {0, -1},
		};

		for (int i = 0; i < 4; i++) {
			int x = arrDir[i, 0];
			int z = arrDir[i, 1];
			Vector3 direction = new Vector3((float)x, 0, (float)z);
			if (IsClearPath(node.transform.position, direction)) {
				GameObject neighbour = CreateNode(ix + x, iz + z);
				LinkNodes(node, neighbour);
				CreateNeighbours(neighbour);
			} else {
				Debug.Log("NOT CLEAR: " + x + ", " + z);
			}
		}
	}

	private void LinkNodes(GameObject node1, GameObject node2) {
		PathNode path1 = node1.GetComponent<PathNode>();
		PathNode path2 = node2.GetComponent<PathNode>();

		path1.neighbours.Add(path2);
		path2.neighbours.Add(path1);
	}

	private bool IsClearPath(Vector3 position, Vector3 direction) {
		return !Physics.Raycast(position, direction);
	}

	private GameObject CreateNode(int x, int z) {
		Vector3 position = new Vector3((float)x, 0.2f, (float)z);

		GameObject gameObject = Instantiate(pfPathNode) as GameObject;
		gameObject.transform.position = position;

		return gameObject;
	}
}
