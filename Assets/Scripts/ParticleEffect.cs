using UnityEngine;
using System.Collections;

public class ParticleEffect : MonoBehaviour {
	public float maxLifeTime = 5f;
	private float lifetime;

	void Start () {
		
	}
	
	void Update () {
		lifetime += Time.deltaTime;
		if (lifetime > maxLifeTime) {
			Destroy(gameObject);
		}
	}
}
