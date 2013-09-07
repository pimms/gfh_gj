using UnityEngine;
using System.Collections;

public class PathNodeMapper : MonoBehaviour {
	public GameObject initialNode;
	public GameObject pfPathNode;

	private const int maxNodes = 40;
	GameObject[,] setNodes;

	/* Used for direction checking when generating 
	 * path node connections
	 */
	private static int[,] arrDir = new int[4, 2] {
		{1, 0}, {-1, 0},
		{0, 1}, {0, -1},
	};

	void Start() {
		if (initialNode == null) {
			Debug.LogError("No inital node set");
			return;
		}

		setNodes = new GameObject[maxNodes, maxNodes];
		//CreateNeighbours(initialNode);
		
		Debug.Log("DONE, LOL");
	}

	private void CreateNeighbours(GameObject node) {
		int ix = Mathf.RoundToInt(node.transform.position.x);
		int iz = Mathf.RoundToInt(node.transform.position.z);
		Debug.Log("Creating node: " + ix + ", " + iz);
		
		if (setNodes[ix, iz] != null) {
			return;
		}
		
		setNodes[ix, iz] = node;

		for (int i = 0; i < 4; i++) {
			int x = arrDir[i, 0];
			int z = arrDir[i, 1];

			// Ensure that the new coordinates are valid
			if (x + ix >= 0 && x + ix < maxNodes && z + iz >= 0 && z + iz < maxNodes) {
				Vector3 direction = new Vector3((float)x, 0, (float)z);

				if (IsClearPath(node.transform.position, direction)) {
					GameObject neighbour = CreateOrFindNode(ix+x, iz+z);
					LinkNodes(node, neighbour);
					CreateNeighbours(neighbour);
				} 
			}
		}
	}

	private void LinkNodes(GameObject node1, GameObject node2) {
		PathNode path1 = node1.GetComponent<PathNode>();
		PathNode path2 = node2.GetComponent<PathNode>();
		
		if (!path1.neighbours.Contains(path2)) {
			path1.neighbours.Add(path2);
		}
		
		if (!path2.neighbours.Contains(path1)) {
			path2.neighbours.Add(path1);
		}
	}

	private bool IsClearPath(Vector3 position, Vector3 direction) {
		return !Physics.Raycast(position, direction, 1f, int.MaxValue);
	}
	
	private GameObject CreateOrFindNode(int x, int z) {
		if (setNodes[x, z] != null) {
			return setNodes[x, z];
		}
		
		return CreateNode(x, z);
	}

	private GameObject CreateNode(int x, int z) {
		Vector3 position = new Vector3((float)x, 0.2f, (float)z);

		GameObject gameObject = Instantiate(pfPathNode) as GameObject;
		gameObject.transform.position = position;

		return gameObject;
	}
}
