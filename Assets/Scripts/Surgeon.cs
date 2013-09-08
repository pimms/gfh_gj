using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Surgeon : Person {
	const int MAXEXP = 200;
    const int OPERATIONMAXTIME = 5;

	public double exp = 10;

    protected bool patientInBedLastFrame;

    // Probability and stuff
    public float surgeryPerformance = 0f;
    float xpCoeff;
    protected float operationTimer;
    float survivalRate;

	protected override void Start () {
		base.Start();
	}
	
	void Update () {
		base.Update();
		energyUpdate();

		Vector3 pos = transform.position;
		pos.y = 1f;
		transform.position = pos;

        SurgeryUpdate();
	}
	
	public float efficieny(){
		return (0.75f+health/200f);
		
	}

	/* FUCK THE POLICE THIS IS GOOD CODE */
	public void energyUpdate() {

		float delta = Time.deltaTime;

		Sofa sofaPtr = currentBed as Sofa;

		if (sofaPtr != null) {
			if (health < 100f) {
				health += (100f / 20f) * Time.deltaTime;
			}
			return;
		}

		if (health <= 0f) return;

		float emptyTime = 180f;

		if (currentBed != null
		&& currentBed.patient != null
		&& currentBed.patient.IsInBed) {
			if (health > 0f) {
				// Roughly two surgeries
				emptyTime = 20f;
			}

		} else {
			if (health > 0f) {
				emptyTime = 90f;
			}
		}

		health -= Time.deltaTime * (100f / emptyTime);
	}
	
	void OnGUI() {

		Rect rect = new Rect(0f, 0f, Screen.width, Screen.height);
		Vector2 pos = Camera.main.WorldToScreenPoint(transform.position);

		if (rect.Contains(pos)) {
			float distance = (Camera.main.transform.position - transform.position).magnitude;
			if (distance < 35f) {
				GUI.color = Color.white;
				GUI.backgroundColor = new Color(1f, 1f, 1f, 1f);

				Rect box = new Rect(pos.x - 400 / distance, Screen.height - pos.y - 1000 / distance, health / 2f, 10f);
				GUI.Button(box, "");
			}
		}
	}
	
	public override void OnMouseClick(int mouseButton, InputOrder inOrder) {
		inOrder.AddAsActor(this);
		Debug.Log("BITCH DON'T CLICK ME");
	}

	public override void BeginPerform(Order order) {
		shouldGoToBed = false;
		RemoveFromSurgery();

		currentBed = order.objectAction as Bed;

		Vector3 pos = transform.position;
		Vector3 objectPos = order.objectAction.transform.position;

		AStar astar = new AStar();
		currentPath = astar.FindPath(pos, objectPos);
	}

	public bool IsActor() {
		return true;
	}
	
	public bool OperationProbability(Patient Bob) {
        int random = Random.Range(0, 100);
        Debug.Log(random);
        
        if (random < Patient.patOutRates[Bob.sickness, 0]) {
            //return Patient.patSurgRates[Bob.sickness, 0];
            return true;
        } else {
            //return Patient.patSurgRates[Bob.sickness, 1];
            return false;
        }
	}

	protected override void OnBedReached(Bed bed) {
		transform.position = bed.GetPrimaryPosition();
        operationTimer = 5;
        //if (OperationProbability(currentBed.patient)) {
        //    currentBed.nurse.exp -= 5;
        //    currentBed.patient.Kill();
        //} else {
        //    currentBed.nurse.exp += 3;
        //    exp += 10;
        //}
	}

    private void SurgeryUpdate() {
        bool patInBed = false;

        if (currentBed != null && currentBed.patient != null) {
            patInBed = currentBed.patient.IsInBed;
        } else {
            patientInBedLastFrame = false;
            return;
        }

        if (patInBed) {
            // Start the surgery
            if (!patientInBedLastFrame) {
                operationTimer = OPERATIONMAXTIME;
                surgeryPerformance = 0f;
            }

            operationTimer -= Time.deltaTime;

            if (operationTimer > 0f) {
                // Find the delta performance step
                float deltaPerf = efficieny();
                if (currentBed.nurse != null) {
                    deltaPerf += currentBed.nurse.efficieny();
                }
                deltaPerf *= Time.deltaTime / OPERATIONMAXTIME;
                surgeryPerformance += deltaPerf;
            } else {
                FinalizeOperation();
            }
        } else {
            // Scratch dick until patient arrives
        }



        patientInBedLastFrame = patInBed;
    }

    protected float XpFactor()
    {
        return 1f + (1f - (1f / Mathf.Sqrt((float)exp)));
    }

    private void FinalizeOperation() {
        float finalRate = 1; // surgeryPerformance;

        float baseRate = (float)Patient.patSurgRates[currentBed.patient.sickness, 0] / 100f;
        float healthRate = currentBed.patient.health / 100f;

        finalRate *= baseRate;
        finalRate *= healthRate;

        finalRate *= XpFactor() / 2f;

        if (currentBed.nurse != null) {
            finalRate *= currentBed.nurse.XpFactor() / 2f;
        }

        float dice = Random.Range(0f, 99f);
        Debug.Log("baseRate: " + baseRate);
        Debug.Log("healthRate: " + healthRate);
        Debug.Log("XpCoefficient: " + XpFactor());
        Debug.Log("Nurse ExPerience" + currentBed.nurse.XpFactor());
        Debug.Log("Dice :" + dice + " < " + finalRate * 100f + " = DEATH!");
        if (dice < (finalRate * 100f)) {
            currentBed.patient.Kill();
        } else {
            currentBed.patient.SendHome();
        }
    }
}
