using UnityEngine;
using System.Collections;

public abstract class PlayerUnit : Unit
{
	//Equipables
	protected Spell left_spell;
	protected Spell right_spell;
	protected Equipment weapon;

	//Unit states
	protected long next_attack_time;
	//current_health

	public int GetAttackPower() {
		return this.attack_power + weapon.GetAttackBonus(this);
	}

	public int GetDefencePower() {
		return this.defence_power + weapon.GetDefenceBonus(this);
	}

	public int GetCriticalChance() {
		return this.critical_chance + weapon.GetCriticalChanceBonus(this);
	}

	public int GetCriticalDamage() {
		return this.critical_chance + weapon.GetCriticalDamageBonus(this);
	}

	public override int GetMaxHealth() {
		return this.max_health + weapon.GetHealthBonus(this);
	}

	override public void Attack(PartyController allies, PartyController enemies) {
		enemies.GetRandomTarget().TakeDamage(this.GetAttackPower());
	}

}
