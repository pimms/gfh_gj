using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Person : Clickable {
	
	protected int health = 100;
	
	void OnGUI () {
		
		Rect rect = new Rect(0f,0f, Screen.width, Screen.height );
		Vector2 pos = Camera.main.WorldToScreenPoint( transform.position );
		
		if( rect.Contains(pos) ){
			float distance = ( Camera.main.transform.position - transform.position).magnitude;
			if ( distance < 35f){
				GUI.Box(new Rect(pos.x-400/distance, Screen.height - pos.y-1000/distance, 800/distance, 600/distance), health.ToString() ); //health.ToString()
			}
		}
	}
	
	protected float walkSpeed = 10f;
	protected List<PathNode> currentPath;

	private bool movedLastFrame;
	
	private Color dispColor;
	
	protected virtual void Start () {
		currentPath = new List<PathNode>();
	}
	
	protected virtual void Update () {
		if (currentPath.Count != 0) {
			FollowPath(currentPath);
		} else if (movedLastFrame) {
			OnPathCompleted();
			movedLastFrame = false;
		}
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
	
	public virtual void OnPathCompleted() {
		
	}
}
