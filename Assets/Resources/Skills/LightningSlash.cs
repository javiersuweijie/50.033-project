using UnityEngine;
using System.Collections;


public class LightningSlash : Skill {

	private Transform anim;
	private float x = 5.0f;
	private float y = -2.2f;
	private UnitController baseUnit;

	void Start(){
		potency = 1.5f;
		probability = 0.5f;
		name = "Lightning Slash";
		cdtime = 2.5f;
		cooldown = true;
		cost = 60;
		baseUnit = gameObject.GetComponent<UnitController> ();

		anim = (Transform)Resources.Load ("GFXAnim/LightningSlash/LSPrefab", typeof(Transform));
		skillbanner = "GFXAnim/LightningSlash/LSBannerPivot";

	}

	override public void Execute(PartyController friendly, PartyController enemy, StaminaBar stambar){
		Debug.Log("Lightning skill");
		float chance = Random.Range (0.0f, 1.0f);

		if (cooldown == true && chance < probability && stambar.UseStamina(cost)) {
			Vector3 skillflashLoc = gameObject.transform.position;
			Instantiate (baseUnit.skillflashO, skillflashLoc, Quaternion.identity);
			baseUnit.sac.DisplayPlayerSkill(skillbanner);
			cooldown = false;

			StartCoroutine (SkillCooldown());
			StartCoroutine(dmgAnim(enemy));
		}
	}

	IEnumerator dmgAnim(PartyController enemy)
	{
		baseUnit.GetComponent<Animator> ().Play (baseUnit.skillanim);
		yield return new WaitForSeconds(0.45f);
		Vector3 animLoc = new Vector3 (x, y, -1);
		Transform skillAnim = (Transform)Instantiate (anim, animLoc, Quaternion.identity);

		yield return new WaitForSeconds(0.25f);
		foreach (UnitController targets in enemy.GetAllTargets()) {
			targets.TakeDamage ((int)(potency * baseUnit.unit.GetATKValue()));
		}
	}
}
