using UnityEngine;
using System.Collections;


public class HolyBarrier : Skill {

	private Transform anim;

	private UnitController baseUnit;

	void Start(){
		potency = 1.25f;
		probability = 0.40f;
		name = "Holy Barrier";
		cdtime = 6.5f;
		cooldown = true;
		cost = 100;
		baseUnit = gameObject.GetComponent<UnitController> ();

		anim = (Transform)Resources.Load ("GFXAnim/HolyDef/HS6", typeof(Transform));
		skillbanner = "GFXAnim/HolyDef/HGBannerPrefab";

	}

	override public void Execute(PartyController friendly, PartyController enemy, StaminaBar stambar){

		float chance = Random.Range (0.0f, 1.0f);

		if (cooldown == true && chance < probability && stambar.UseStamina(cost)) {
			Vector3 skillflashLoc = gameObject.transform.position;
			Instantiate (baseUnit.skillflashO, skillflashLoc, Quaternion.identity);
			baseUnit.sac.DisplayPlayerSkill(skillbanner);
			cooldown = false;

			StartCoroutine (SkillCooldown());
			StartCoroutine(skillAnim(friendly));
		}
	}

	IEnumerator skillAnim(PartyController friendly)
	{
		baseUnit.GetComponent<Animator> ().Play (baseUnit.skillanim);
		yield return new WaitForSeconds(0.45f);
	
		foreach (UnitController targets in friendly.GetAllTargets()) {
			targets.GetComponent<BuffManager>().ApplyBuff(1,potency);
			Instantiate (anim, targets.transform.position, Quaternion.identity);
		}
	}
}
