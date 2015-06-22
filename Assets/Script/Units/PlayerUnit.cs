using UnityEngine;
using System.Collections;

public abstract class PlayerUnit : Unit
{
	//Equipables
	protected Spell left_spell;
	protected Spell right_spell;
	protected Equipment weapon = null;

	//Unit states
	//current_health

	public int GetAttackPower() {
		if (weapon != null)
			return this.attack_power + weapon.GetAttackBonus(this);
		else
			return this.attack_power;
	}

	public int GetDefencePower() {
		if (weapon != null)
			return this.defence_power + weapon.GetDefenceBonus(this);
		else
			return this.defence_power;
	}

	public int GetCriticalChance() {
		if (weapon != null)
			return this.critical_chance + weapon.GetCriticalChanceBonus(this);
		else
			return this.critical_chance;
	}

	public int GetCriticalDamage() {
		if (weapon != null)
			return this.critical_chance + weapon.GetCriticalDamageBonus(this);
		else
			return this.critical_damage;
	}

	public override int GetMaxHealth() {
		if (weapon !=  null)
			return this.max_health + weapon.GetHealthBonus(this);
		else
			return this.max_health;
	}

	override public void Attack(PartyController allies, PartyController enemies) {
		//default attack is to hit random enemies
		if (this.CanAttack()) {
			enemies.GetRandomTarget().TakeDamage(this.GetAttackPower());
			next_attack_time = Time.time + attack_speed/100f;
		}
		else return;
	}

	override public void UseSkill(PartyController allies, PartyController enemies) {
		return;
	}

}