using UnityEngine;
using System.Collections;
using System;

public class Paladin : PlayerUnit
{

	public Paladin(int _experience) {

		name = "Paladin";
		experience = _experience;
		//growth stats
		max_health_growth = 12;
		attack_power_growth = 4;
		defence_power_growth = 5;
		attack_speed_growth = 3;
		critical_chance_growth = 1;
		critical_damage_growth = 0;
		icon_name = "Sprites/Icons/51325";

		skillanim = "PLD_Skill";
		sprite_name = "Sprites/CHR_PLDtest_0";
		runTimeAnimatorController = "Sprites/PLD_CTR";
//		spr.sprite = (Sprite)Resources.Load ("Sprites/CHR_PLDtest_0");
//		anim.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load ("Sprites/PLD_CTR");

		//base stats

		max_health = 230 + GetLevel() * max_health_growth;
		attack_power = 35 + GetLevel() * attack_power_growth;
		defence_power = 44 + GetLevel() * defence_power_growth;
		attack_speed = 90 + GetLevel() * attack_speed_growth;
		critical_chance = 10 + GetLevel() * critical_chance_growth;
		critical_damage = 50 + GetLevel() * critical_damage_growth;

		current_health = max_health;
	}

	public override bool Attack (PartyController allies, PartyController enemies, StaminaBar stambar)
	{
		//default attack is to hit random enemies
		if (this.CanAttack()) {

			UnitController enemy = enemies.GetFrontTarget();
			enemy.TakeDamage((int)(this.GetATKValue()*UnityEngine.Random.Range (0.85f,1.15f)));
			//Debug.Log(enemy.GetCurrentHealth());
			next_attack_time = Time.time + 150f/this.GetAGIValue();
			return true;
//			StartCoroutine (UseSkill(allies, enemies, stambar));
		}
		else return false;
	}
}

