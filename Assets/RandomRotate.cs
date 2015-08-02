using UnityEngine;
using System.Collections;

public class RandomRotate : MonoBehaviour {

	private float speed;
	// Use this for initialization
	void Start () {
		speed = Random.Range (-280f, 280f);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(0f, 0f, Time.deltaTime * speed, Space.Self);
	}
}
