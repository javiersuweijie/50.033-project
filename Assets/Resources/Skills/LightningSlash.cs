using UnityEngine;
using System.Collections;


public class LightningSlash : Skill {

	private Transform anim;
	private float x = 5.0f;
	private float y = -1.0f;
	private PlayerUnit baseUnit;

	void Start(){
		potency = 3.0f;
		probability = 0.5f;
		name = "Lightning Slash";
		cdtime = 2.5f;
		cooldown = true;
		baseUnit = gameObject.GetComponent<PlayerUnit> ();

		anim = (Transform)Resources.Load ("GFXAnim/LightningSlash/LSPrefab", typeof(Transform));

	}

	override public void Execute(PartyController friendly, PartyController enemy){

		float chance = Random.Range (0.0f, 1.0f);

		if (cooldown == true && chance < probability) {

			//Transform skillGlow = (Transform)Instantiate (baseUnit.skillflashO, 
			Vector3 animLoc = new Vector3 (x, y, -1);
			Transform skillAnim = (Transform)Instantiate (anim, animLoc, Quaternion.identity);

			foreach (Unit targets in enemy.GetAllTargets()) {
				targets.TakeDamage ((int)(potency * baseUnit.GetAttackPower ()));
			}
			cooldown = false;
			StartCoroutine (SkillCooldown());
		}
	}



}
