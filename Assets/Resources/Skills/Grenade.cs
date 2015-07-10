using UnityEngine;
using System.Collections;

public class Grenade : MonoBehaviour {

	private int damage;
	private GameObject explosion = (GameObject)Resources.Load ("GFXAnim/GrenadeBarrage/GExFab", typeof(GameObject));

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		//transform.rotation = Quaternion.LookRotation(GetComponent<Rigidbody>().velocity);
	}

	public void Initialize(Vector3 vel, int dmg){
		GetComponent<Rigidbody>().velocity = vel;
		damage = dmg;
	}
	
	public void SetDamage(int dmg){
		damage = dmg;
	}

	void OnCollisionEnter(Collision collider){
		if (collider.gameObject.tag == "Ground"){
			GameObject exp = (GameObject)Instantiate (explosion, gameObject.transform.position, Quaternion.identity);
			exp.GetComponent<Explosion>().setDamage(damage);
			Destroy(this.gameObject);
		}
	}
}
