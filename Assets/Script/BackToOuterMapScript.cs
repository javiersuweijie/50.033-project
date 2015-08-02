using UnityEngine;
using System.Collections;

public class BackToOuterMapScript : MonoBehaviour {

	public GameObject combatObject;
	// Use this for initialization
	public void Back(){
		Destroy (combatObject);
	}
}
