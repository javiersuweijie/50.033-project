using UnityEngine;
using System.Collections;


public class HolyInfusion : Skill {

	private Transform anim;

	private UnitController baseUnit;

	void Start(){
		potency = 4.0f;
		probability = 0.9f;
		name = "Holy Infusion";
		cdtime = 7.0f;
		cooldown = true;
		cost = 75;
		baseUnit = gameObject.GetComponent<UnitController> ();

		anim = (Transform)Resources.Load ("GFXAnim/Infusion/Infusion", typeof(Transform));
		skillbanner = "GFXAnim/Infusion/HIBannerPrefab";

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
		yield return new WaitForSeconds(0.35f);
	
		UnitController healtarget = friendly.GetMostHurtUnit();
		if (healtarget != null){
			healtarget.ReceiveHeal((int)(baseUnit.unit.GetATKValueHeal()* Random.Range (0.9f,1.1f) * potency));
			Transform skillAnim = (Transform)Instantiate (anim, healtarget.transform.position, Quaternion.identity);
		}

	}
}
