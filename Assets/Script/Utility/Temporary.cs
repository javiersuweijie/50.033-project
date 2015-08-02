using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temporary : MonoBehaviour {
	
	public float duration = 1.5f;
	
	void Start() {
		Destroy(gameObject, duration);
	}

}