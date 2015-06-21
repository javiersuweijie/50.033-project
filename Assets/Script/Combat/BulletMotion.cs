using UnityEngine;
using System.Collections;

public class BulletMotion : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody>().AddForce(transform.up * 500);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
