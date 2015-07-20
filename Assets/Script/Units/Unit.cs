using UnityEngine;
using System.Collections;

public enum FightingMode {
	Neutral,Offensive,Defensive
}

//placeholders
public interface Equipment {
	int GetAttackBonus(Unit unit);
	int GetCriticalChanceBonus(Unit unit);
	int GetDefenceBonus(Unit unit);
	int GetCriticalDamageBonus(Unit unit);
	int GetHealthBonus(Unit unit);
}

public abstract class Unit {

//	protected Transform uitxt = (Transform)Resources.Load ("Prefabs/FloatDmgText", typeof(Transform));
//	protected Transform uitxto = (Transform)Resources.Load ("Prefabs/FloatDmgTextOutline", typeof(Transform));
//	public Transform skillflashO = (Transform)Resources.Load ("GFXAnim/SkillGlow/skill1", typeof(Transform));
//	protected Transform attackprefab;

	//base stats

	protected int max_health;
	protected int attack_power;
	protected int defence_power;
	protected int attack_speed;
	protected int critical_chance;
	protected int critical_damage;

	public BuffManager buffManager;

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
	protected float OEMod = 1.0f;
	protected float DEMod = 1.0f;

	//for rendering
	public string sprite_name;
	protected string icon_name;
	protected bool ally;
	public string attack_prefab_name;
	public string runTimeAnimatorController;
//	protected Animator anim;
//	protected SpriteRenderer spr;
	public string skillanim;

	public System.Type right_spell_type;
	public System.Type left_spell_type;
	public Skill left_spell;
	public Skill right_spell;
//	public SkillAnimController sac;

	//for damage text
	protected float yoffset = 0.05f;
	protected int yoffsetr = 0;

	public abstract bool Attack(PartyController allies, PartyController enemies, StaminaBar stambar);
	public abstract bool UseSkill(PartyController allies, PartyController enemies, StaminaBar stambar);

//	public void InitializeSAC(SkillAnimController in_sac){
//		sac = in_sac;
//	}

	public int TakeDamage(int value) {

		int dmg = (int)(value - Mathf.Pow (this.GetDEFValue(), 0.7f)); 
		current_health -= (int)(dmg); // Assuming defence from 0 - 1000 where 1000 = takes no damage
		if (current_health < 0) current_health = 0;
		return dmg;

	}

	public int ReceiveHeal(int value) {

		current_health += value;
		if (current_health > this.GetMaxHealth()) current_health = this.GetMaxHealth();
		return value;
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
		OEMod = 1.0f;
		DEMod = 1.0f;
	}
	public void OffensiveMode() {
		mode = FightingMode.Offensive;
		OEMod = 1.25f;
		DEMod = 0.9f;
	}
	public void DefensiveMode() {
		mode = FightingMode.Defensive;
		DEMod = 1.25f;
		OEMod = 0.9f;
	}

	public bool CanAttack() {
		if (next_attack_time < Time.time) return true;
		else return false;
	}

	public string getIconName() {
		return icon_name;
	//Stats After Buffs/EDGES (Use These Only);
	}
	public int GetATKValue(){
		return (int)(attack_power * OEMod * buffManager.GetATKMod ());
	}

	public int GetATKValueHeal(){
		return (int)(attack_power * DEMod * buffManager.GetATKMod ());
	}

	public int GetDEFValue(){
		return (int)(defence_power * DEMod * buffManager.GetDEFMod ());
	}

	public int GetAGIValue(){
		return (int)(attack_speed * buffManager.GetAGIMod ());
	}
}
