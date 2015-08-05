using UnityEngine;
using System.Collections;

public class Menblatt : Unit {

	override public bool UseSkill(PartyController allies, PartyController enemy, StaminaBar stambar){ return false;}
	
	
	override public bool Attack(PartyController allies, PartyController enemies, StaminaBar stambar) {
		if (this.CanAttack()) {
			//			Debug.Log ("Poporing attack!");
			//			anim.Play("PRN_Attack");
			next_attack_time = Time.time + 150f/attack_speed;
			enemies.GetRandomTarget().TakeDamage(GetATKValue());
			return true;
		}
		else return false;
	}
	
	
	public Menblatt() {
		
		scale = 1.1f;
		name = "Menblatt";
		experience = 45;
		//growth stats
		max_health_growth = 200;
		attack_power_growth = 10;
		defence_power_growth = 1;
		attack_speed_growth = 0;
		critical_chance_growth = 1;
		critical_damage_growth = 0;
		
		//base stats
		
		max_health = 640 + GetLevel() * max_health_growth;
		attack_power = 38 + GetLevel() * attack_power_growth;
		defence_power = 26 + GetLevel() * defence_power_growth;
		attack_speed = 75 + GetLevel() * attack_speed_growth;
		critical_chance = 10 + GetLevel() * critical_chance_growth;
		critical_damage = 50 + GetLevel() * critical_damage_growth;
		
		sprite_name = "Sprites/Monster/Menblatt_noBG";
		runTimeAnimatorController = "Sprites/MOnster/Menblatt_noBG_0";
		icon_name = "CREEP";
		
		current_health = max_health;
		
		
		//render
	}
}
