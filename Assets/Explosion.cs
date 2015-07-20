using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

	private int damage = 0;
	// Use this for initialization
	void Start () {
	
	}

	public void setDamage(int dmg)
	{
		damage = dmg;
	}
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
	{
		Debug.Log("Attackedddedededed");
		other.gameObject.GetComponent<UnitController> ().TakeDamage (damage);
	}
}
