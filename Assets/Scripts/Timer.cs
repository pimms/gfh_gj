using UnityEngine;
using System.Collections;

public class Timer {
	float startTime;

	public Timer() {
		Begin();
	}

	public void Begin() {
		startTime = Time.timeSinceLevelLoad;
	}

	public void End(string task) {
		float endTime = Time.timeSinceLevelLoad;
		Debug.Log("Task " + task + " completed in " + (endTime - startTime));
	}
}
