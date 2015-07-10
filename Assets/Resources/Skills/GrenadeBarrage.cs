using UnityEngine;
using System.Collections;


public class GrenadeBarrage : Skill {

	private GameObject anim;


	private PlayerUnit baseUnit;

	void Start(){
		potency = 4.5f;
		probability = 0.3f;
		name = "Grenade Barrage";
		cdtime = 4.5f;
		cooldown = true;
		cost = 70;
		baseUnit = gameObject.GetComponent<PlayerUnit> ();

		anim = (GameObject)Resources.Load ("GFXAnim/GrenadeBarrage/GrenadeFab", typeof(GameObject));
		skillbanner = "GFXAnim/GrenadeBarrage/GBBannerFab";

	}

	override public void Execute(PartyController friendly, PartyController enemy, StaminaBar stambar){

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
		Vector3 animLoc;
		yield return new WaitForSeconds (0.3f);
		//for number of grenades
		for (int i = 0; i < 4; i++)
		{
			animLoc = new Vector3(Random.Range (5f,10f),Random.Range (7f,9f));
			Vector3 pos = new Vector3(transform.position.x + 0.5f, transform.position.y-0.3f, 0);
			yield return new WaitForSeconds(0.15f);
			Debug.Log ("grenade out!");
			GameObject skillAnim = (GameObject)Instantiate (anim, pos, Quaternion.identity);
			skillAnim.GetComponent<Grenade>().Initialize(animLoc, (int)(baseUnit.GetATKValue() * potency));
		}

	}

}
