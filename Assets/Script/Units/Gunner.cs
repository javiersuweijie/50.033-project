using UnityEngine;
using System.Collections;

public class Gunner : PlayerUnit
{

	public Gunner(int _experience) {
		
		name = "Gunner";
		experience = _experience;
		//growth stats
		max_health_growth = 9;
		attack_power_growth = 5;
		defence_power_growth = 4;
		attack_speed_growth = 4;
		critical_chance_growth = 1;
		critical_damage_growth = 0;

	
		skillanim = "MCH_Skill";
		sprite_name = "Sprites/CHR_MCH_Alpha_0";
		runTimeAnimatorController = "Sprites/MCH_CTR";
//		spr.sprite = (Sprite)Resources.Load ("Sprites/CHR_MCH_Alpha_0");
//		anim.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load ("Sprites/MCH_CTR");

		//base stats

		max_health = 145 + GetLevel() * max_health_growth;
		attack_power = 38 + GetLevel() * attack_power_growth;
		defence_power = 35 + GetLevel() * defence_power_growth;
		attack_speed = 93 + GetLevel() * attack_speed_growth;
		critical_chance = 10 + GetLevel() * critical_chance_growth;
		critical_damage = 50 + GetLevel() * critical_damage_growth;

		icon_name = "Sprites/Icons/22404";

		current_health = max_health;
	}

	public override bool Attack (PartyController allies, PartyController enemies, StaminaBar stambar)
	{
		//default attack is to hit random enemies
		if (this.CanAttack()) {
			UnitController enemy = enemies.GetRandomTarget();
			enemy.TakeDamage((int)(this.GetATKValue()*Random.Range (0.85f,1.15f)));
			//Debug.Log(enemy.GetCurrentHealth());
			next_attack_time = Time.time + 150f/this.GetAGIValue();
			return true;
		}
		else return false;
	}
}

