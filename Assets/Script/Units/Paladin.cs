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
		right_spell = gameObject.GetComponent("LightningSlash") as Skill;

		anim = gameObject.GetComponent<Animator> ();
		spr = gameObject.GetComponent<SpriteRenderer> ();

		spr.sprite = (Sprite)Resources.Load ("Sprites/PalaSprite_3_2");
		anim.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load ("Sprites/PaladinController");

		//base stats

		max_health = 1000 + GetLevel() * max_health_growth;
		attack_power = 100 + GetLevel() * attack_power_growth;
		defence_power = 200 + GetLevel() * defence_power_growth;
		attack_speed = 100 + GetLevel() * attack_speed_growth;
		critical_chance = 10 + GetLevel() * critical_chance_growth;
		critical_damage = 50 + GetLevel() * critical_damage_growth;

		sprite_name = "PALADIN";
		icon_name = "PALADIN";

		current_health = max_health;
	}

	public override void Attack (PartyController allies, PartyController enemies, StaminaBar stambar)
	{
		//default attack is to hit random enemies
		if (this.CanAttack()) {
			anim.Play("PaladinAtk");
			Unit enemy = enemies.GetRandomTarget();
			enemy.TakeDamage(this.GetAttackPower());
			//Debug.Log(enemy.GetCurrentHealth());
			next_attack_time = Time.time + attack_speed/100f;
			StartCoroutine (UseSkill(allies, enemies, stambar));
		}
		else return;
	}
}

