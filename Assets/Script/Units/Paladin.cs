using UnityEngine;
using System.Collections;

public class Paladin : PlayerUnit
{

	void Start() {

		experience = 2500;
		//growth stats
		max_health_growth = 200;
		attack_power_growth = 10;
		defence_power_growth = 1;
		attack_speed_growth = 0;
		critical_chance_growth = 1;
		critical_damage_growth = 0;

		//base stats

		max_health = 1000 + GetLevel() * max_health_growth;
		attack_power = 200 + GetLevel() * attack_power_growth;
		defence_power = 200 + GetLevel() * defence_power_growth;
		attack_speed = 100 + GetLevel() * attack_speed_growth;
		critical_chance = 10 + GetLevel() * critical_chance_growth;
		critical_damage = 50 + GetLevel() * critical_damage_growth;

		sprite_name = "PALADIN";
		icon_name = "PALADIN";

		current_health = max_health;
	}

	public override void Attack (PartyController allies, PartyController enemies)
	{
		//default attack is to hit random enemies
		if (this.CanAttack()) {
			Unit enemy = enemies.GetRandomTarget();
			enemy.TakeDamage(this.GetAttackPower());
			Debug.Log(enemy.GetCurrentHealth());
			next_attack_time = Time.time + attack_speed/100f;

		}
		else return;
	}
}
