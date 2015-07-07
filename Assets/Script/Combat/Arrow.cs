using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {

	private int damage;

	// Use this for initialization
	void Start () {
		InitMovement (new Vector3(-6, 2, 0), new Vector3 (7, 5, 0));
		damage = 10000;
	}
	
	// Update is called once per frame
	void Update () {
		transform.rotation = Quaternion.LookRotation(GetComponent<Rigidbody>().velocity);
	}

	public void InitMovement(Vector3 pos, Vector3 vel){
		this.transform.position = pos;
		GetComponent<Rigidbody>().velocity = vel;
	}
	
	public void SetDamage(int dmg){
		damage = dmg;
	}

	void OnCollisionEnter(Collision collider){
		if (collider.gameObject.tag == "Player" || collider.gameObject.tag == "Enemy"){
			collider.gameObject.GetComponent<Unit>().TakeDamage(damage);
		}
		Destroy(this.gameObject);
	}
}
