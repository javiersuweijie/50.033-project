using UnityEngine;
using System.Collections.Generic;


public class SkillAnimController : MonoBehaviour {

	GameObject playerSkill;
	GameObject playerSkillInst;

	Vector3 playerSkillLoc;

	Camera mainCamera;


	void Start(){
		mainCamera = Camera.main;
		playerSkillLoc = mainCamera.ScreenToWorldPoint(new Vector3(0f, Screen.height * 0.7f,3f));
	}

	public void DisplayPlayerSkill(string s)
	{
		Destroy (playerSkillInst);
		playerSkill = (GameObject) Resources.Load (s, typeof(GameObject));
		playerSkillInst = (Instantiate (playerSkill, playerSkillLoc, Quaternion.identity) as GameObject);
		playerSkillInst.AddComponent<SlideRight> ();
		playerSkillInst.AddComponent<Temporary> ();
	}
}
