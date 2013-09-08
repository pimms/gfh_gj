using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class PathNode : MonoBehaviour {
	public static List<PathNode> allNodes;

	public List<PathNode> neighbours;

	private float g;
	private float h;
	private PathNode parent;

	public float G {
		set { g = value; }
		get { return g; }
	}

	public float H {
		set { h = value; }
		get { return h; }
	}

	public float F {
		get { return g + h; }
	}

	public PathNode Parent {
		set { parent = value; }
		get { return parent; }
	}

	void Awake() {
		Debug.Log("wyawyewayey");
		if (allNodes == null) {
			allNodes = new List<PathNode>();
		}

		if (!allNodes.Contains(this)) {
			allNodes.Add(this);
		}
	}

	void Start () {
		Destroy(transform.FindChild("Capsule").gameObject);
	}
	
	void Update () {
	
	}

	public void Reset() {
		g = 0f;
		h = 0f;
		parent = null;
	}
}
