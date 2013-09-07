using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathNode : MonoBehaviour {
	public List<PathNode> neighbours;

	private int g;
	private int h;
	private PathNode parent;

	public int G {
		set { g = value; }
		get { return g; }
	}

	public int H {
		set { h = value; }
		get { return h; }
	}

	public int F {
		get { return g + h; }
	}

	public PathNode Parent {
		set { parent = value; }
		get { return parent; }
	}

	void Start () {
	
	}
	
	void Update () {
	
	}

	public void Reset() {
		
	}
}
