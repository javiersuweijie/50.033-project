using UnityEngine;
using System.Collections;

public class EnemyUnit : Unit
{
	override public IEnumerator UseSkill(PartyController allies, PartyController enemy){ yield break;}
	override public void Attack(PartyController allies, PartyController enemies) {
		if (this.CanAttack()) {
			enemies.GetRandomTarget().TakeDamage(attack_power);
			next_attack_time = Time.time + attack_speed/100f;
		}
		else return;
	}

	void Start() {
		
		experience = 25;
		//growth stats
		max_health_growth = 200;
		attack_power_growth = 10;
		defence_power_growth = 1;
		attack_speed_growth = 0;
		critical_chance_growth = 1;
		critical_damage_growth = 0;
		
		//base stats
		
		max_health = 40000 + GetLevel() * max_health_growth;
		attack_power = 200 + GetLevel() * attack_power_growth;
		defence_power = 200 + GetLevel() * defence_power_growth;
		attack_speed = 100 + GetLevel() * attack_speed_growth;
		critical_chance = 10 + GetLevel() * critical_chance_growth;
		critical_damage = 50 + GetLevel() * critical_damage_growth;
		
		sprite_name = "CREEP";
		icon_name = "CREEP";
		
		current_health = max_health;
	}
}

