using UnityEngine;
using System.Collections;

public enum FightingMode {
	Neutral,Offensive,Defensive
}

//placeholders
public interface Spell {}
public interface Equipment {
	int GetAttackBonus(Unit unit);
	int GetCriticalChanceBonus(Unit unit);
	int GetDefenceBonus(Unit unit);
	int GetCriticalDamageBonus(Unit unit);
	int GetHealthBonus(Unit unit);
}

public abstract class Unit : MonoBehaviour {

	//base stats
	protected int max_health;
	protected int attack_power;
	protected int defence_power;
	protected int attack_speed;
	protected int critical_chance;
	protected int critical_damage;

	//growth stats
	public int max_health_growth;
	public int attack_power_growth;
	public int defence_power_growth;
	public int attack_speed_growth;
	public int critical_chance_growth;
	public int critical_damage_growth;

	//stats that change
	protected int current_health;
	public int experience;
	protected FightingMode mode;
	protected float next_attack_time;

	//for rendering
	protected string sprite_name;
	protected string icon_name;
	protected bool ally;

	public abstract void Attack(PartyController allies, PartyController enemies);
	public abstract void UseSkill(PartyController allies, PartyController enemies);

	public void TakeDamage(int value) {
		current_health -= (value * (1 - defence_power/1000)); // Assuming defence from 0 - 1000 where 1000 = takes no damage
		if (current_health < 0) current_health = 0;
	}

	public void ReceiveHeal(int value) {
		current_health += value;
		if (current_health > this.GetMaxHealth()) current_health = this.GetMaxHealth();
	}

	public bool IsDead() {
		return current_health <= 0;
	}

	public virtual int GetMaxHealth() { // this method is virtual because we want PlayerUnit to override this to account for bonus hp from weapons
		return max_health;
	}

	public int GetCurrentHealth() {
		return current_health;
	}

	public float GetFractionalHealth() {
		return current_health/(float)this.GetMaxHealth();
	}

	public int GetLevel() {
		return (int) Mathf.Sqrt(experience);
	}

	public void NeutralMode() {
		mode = FightingMode.Neutral;
	}
	public void OffensiveMode() {
		mode = FightingMode.Offensive;
	}
	public void DefensiveMode() {
		mode = FightingMode.Defensive;
	}	
	public bool CanAttack() {
		if (next_attack_time < Time.time) return true;
		else return false;
	}
}
