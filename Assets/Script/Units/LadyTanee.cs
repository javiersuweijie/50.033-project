using UnityEngine;
using System.Collections;

public class LadyTanee : Unit {

	private int attackCount;

	override public bool UseSkill(PartyController allies, PartyController enemy, StaminaBar stambar){ return false;}
	
	
	override public bool Attack(PartyController allies, PartyController enemies, StaminaBar stambar) {
		if (this.CanAttack()) {

			attackCount += 1;
			if (attackCount == 2){

				next_attack_time = Time.time + 225f/attack_speed;
				enemies.GetRandomTarget().TakeDamage((int)(GetATKValue() * 1.0));
				return true;

			} else if (attackCount == 3)
			{

				next_attack_time = Time.time + 60f/attack_speed;
				enemies.GetRandomTarget().TakeDamage((int)(GetATKValue() * 1.25), "GFXAnim/GRPrefab");
				return true;

			} else if (attackCount == 4)
			{
				
				next_attack_time = Time.time + 175f/attack_speed;
				enemies.GetRandomTarget().TakeDamage((int)(GetATKValue() * 1.6), "GFXAnim/GRPrefab");
				attackCount = 0;
				return true;
			} else
			//			Debug.Log ("Poporing attack!");
			//			anim.Play("PRN_Attack");
			next_attack_time = Time.time + 150f/attack_speed;
			enemies.GetRandomTarget().TakeDamage(GetATKValue());
			return true;
		}
		else return false;
	}
	
	
	public LadyTanee() {

		attackCount = -1;

		scale = 1.7f;
		name = "Lady Tanee";
		experience = 100;
		//growth stats
		max_health_growth = 200;
		attack_power_growth = 10;
		defence_power_growth = 1;
		attack_speed_growth = 0;
		critical_chance_growth = 1;
		critical_damage_growth = 0;
		
		//base stats
		
		max_health = 2300 + GetLevel() * max_health_growth;
		attack_power = 89 + GetLevel() * attack_power_growth;
		defence_power = 35 + GetLevel() * defence_power_growth;
		attack_speed = 60 + GetLevel() * attack_speed_growth;
		critical_chance = 10 + GetLevel() * critical_chance_growth;
		critical_damage = 50 + GetLevel() * critical_damage_growth;
		
		sprite_name = "Sprites/Monster/Lady_Tanee_noBG";
		runTimeAnimatorController = "Sprites/MOnster/Lady_Tanee_noBG_0";
		icon_name = "CREEP";
		
		current_health = max_health;
		
		
		//render
	}
}
