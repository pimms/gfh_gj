using UnityEngine;
using System.Collections;

public class ScoreTracker : MonoBehaviour {

	public static ScoreTracker singleton;

	private int remainingDeaths = 3;
	private int savedPatients;
	private bool gameOver;

	void Start () {
		singleton = this;
	}
	
	void Update () {
	
	}

	void OnGUI() {
		if (gameOver) {
			GameOverGUI();
		} else {
			GameGUI();
		}
	}

	public void OnPatientDeath() {
		if (gameOver) return;

		remainingDeaths--;

		if (remainingDeaths <= 0) {
			gameOver = true;
		}
	}

	public void OnPatientSentHome() {
		if (gameOver) return;

		savedPatients++;
	}


	private void GameOverGUI() {
		float w = Screen.width;
		float h = Screen.height;

		GUI.color = Color.white;
		GUI.backgroundColor = Color.gray;
		GUI.contentColor = Color.white;

		//  BACKGROUNDS
		Rect back = new Rect(w*0.1f, h*0.1f, w * 0.8f, h * 0.8f);
		GUI.Box(back, "");

		// LABELS
		GUIStyle labelStyle = new GUIStyle();
		labelStyle.alignment = TextAnchor.MiddleCenter;

		GUIContent labCont = new GUIContent();
		labCont.text = "Saved patients:\n" + savedPatients;

		Rect lab1 = new Rect(w / 2f - 150f, h / 3f, 300f, 40f);
		GUI.Label(lab1, labCont, labelStyle);

		// LABELS
		Rect lab2 = new Rect(w / 2f - 150f, h / 2f, 300f, 40f);
		labCont.text = "Verdict:\n" + GetVerdict();

		GUI.Label(lab2, labCont, labelStyle);

		
		// BUTTONS
		Rect button = new Rect(w / 2f-100f, h * 0.7f, 200f, 50f);
		if (GUI.Button(button, "New game")) {
			Application.LoadLevel(0);
		}
	}

	private void GameGUI() {
		float w = Screen.width;
		float h = Screen.height;

		Rect box = new Rect(w / 2f - 150f, 0f, 300f, 50f);
		GUI.Box(box, "Remaining fuckups: " + remainingDeaths 
					+ "\nSaved patients: " + savedPatients);
	}

	private string GetVerdict() {
		if (savedPatients < 3) {
			return "UNINSTALL GAME ASAP";
		} else if (savedPatients < 6) {
			return "Homeopath";
		} else if (savedPatients < 10) {
			return "John Dorian";
		} else if (savedPatients < 15) {
			return "Averagely medium";
		} else if (savedPatients < 20) {
			return "...\"Doctor\"";
		} else {
			return "The Master Doctor";
		}
	}
}
