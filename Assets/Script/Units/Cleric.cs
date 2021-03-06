using UnityEngine;
using System.Collections;

public class Cleric : PlayerUnit
{

	public Cleric(int _experience) {
		
		name = "Cleric";
		experience = _experience;
		//growth stats
		max_health_growth = 10;
		attack_power_growth = 2;
		defence_power_growth = 3;
		attack_speed_growth = 3;
		critical_chance_growth = 1;
		critical_damage_growth = 0;
		attack_prefab_name = "GFXAnim/Priest_Heal/Priest_Heal";
		runTimeAnimatorController = "Sprites/CLR_CTR";

		//right_spell = gameObject.GetComponent("LightningSlash") as Skill;
//		attackprefab = (Transform)Resources.Load ("GFXAnim/Priest_Heal/Priest_Heal", typeof(Transform));

		skillanim = "CLR_Skill";

//		spr.sprite = (Sprite)Resources.Load ("Sprites/CHR_Priest_Alpha_0");
//		anim.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load ("Sprites/CLR_CTR");

		//base stats

		max_health = 175 + GetLevel() * max_health_growth;
		attack_power = 20 + GetLevel() * attack_power_growth;
		defence_power = 32 + GetLevel() * defence_power_growth;
		attack_speed = 70 + GetLevel() * attack_speed_growth;
		critical_chance = 10 + GetLevel() * critical_chance_growth;
		critical_damage = 50 + GetLevel() * critical_damage_growth;

		sprite_name = "Sprites/CHR_Priest_Alpha_0";
		icon_name = "Sprites/Icons/22405";

		current_health = max_health;
	}

	public override bool Attack (PartyController allies, PartyController enemies, StaminaBar stambar)
	{
		//default attack is to hit random enemies
		if (this.CanAttack()) {
//			anim.SetInteger("animController", 1);
			UnitController healtarget = allies.GetMostHurtUnit();
			if (healtarget != null){
				healtarget.ReceiveHeal((int)(this.GetATKValueHeal()* Random.Range (0.9f,1.1f)),attack_prefab_name);
			}
//			}

			//Debug.Log(enemy.GetCurrentHealth());
			next_attack_time = Time.time + 100f/GetAGIValue();
//			UseSkill(allies, enemies, stambar);
			return true;
		}
		else return false;
	}
}

