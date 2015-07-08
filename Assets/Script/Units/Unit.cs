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

public abstract class Unit : MonoBehaviour {

	protected Transform uitxt = (Transform)Resources.Load ("Prefabs/FloatDmgText", typeof(Transform));
	protected Transform uitxto = (Transform)Resources.Load ("Prefabs/FloatDmgTextOutline", typeof(Transform));
	public Transform skillflashO = (Transform)Resources.Load ("GFXAnim/SkillGlow/skill1", typeof(Transform));
	protected Transform attackprefab;

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
	protected Animator anim;
	protected SpriteRenderer spr;
	public string skillanim;

	//for damage text
	protected float yoffset = 0.05f;
	protected int yoffsetr = 0;

	public abstract void Attack(PartyController allies, PartyController enemies, StaminaBar stambar);
	public abstract IEnumerator UseSkill(PartyController allies, PartyController enemies, StaminaBar stambar);

	protected void Start(){
		anim = this.GetComponent<Animator> ();
		spr = this.GetComponent<SpriteRenderer> ();

		StartCoroutine (yoffsetReset ());
		Debug.Log("Started!");
	}

	public void TakeDamage(int value) {

		float dmg = (value * (1 - defence_power/1000)); 

		Vector3 textLocation = Camera.main.WorldToScreenPoint(transform.position);
		textLocation.x /= Screen.width;
		textLocation.x += Random.Range(-0.03f,0.03f);
		textLocation.y /= Screen.height;
		textLocation.y += yoffset;

		yoffset += (0.03f);
		yoffsetr = 0;

		if (yoffset > 0.18f) yoffset = 0.04f;

		Transform tempFloatingDamage = (Transform)Instantiate(uitxt, textLocation, Quaternion.identity);
		tempFloatingDamage.GetComponent<FloatDmgScript>().DisplayDamage(dmg.ToString());

		textLocation.x += 0.003f;
		textLocation.y -= 0.004f;

		Transform tempFloatingDamageOutline = (Transform)Instantiate(uitxto, textLocation, Quaternion.identity);
		tempFloatingDamageOutline.GetComponent<FloatDmgScript>().DisplayDamage(dmg.ToString());

		current_health -= (int)(dmg); // Assuming defence from 0 - 1000 where 1000 = takes no damage
		if (current_health < 0) current_health = 0;

		StartCoroutine (damageFlash ());
	}

	public void ReceiveHeal(int value) {

		Vector3 textLocation = Camera.main.WorldToScreenPoint(transform.position);
		textLocation.x /= Screen.width;
		textLocation.x += Random.Range(-0.03f,0.03f);
		textLocation.y /= Screen.height;
		textLocation.y += yoffset;
		
		yoffset += (0.03f);
		yoffsetr = 0;
		
		if (yoffset > 0.18f) yoffset = 0.04f;
		
		Transform tempFloatingDamage = (Transform)Instantiate(uitxt, textLocation, Quaternion.identity);
		tempFloatingDamage.GetComponent<FloatDmgScript>().DisplayDamage(value.ToString());
		tempFloatingDamage.GetComponent<GUIText> ().color = new Color (0.3f, 1f, 0.3f);
		
		textLocation.x += 0.003f;
		textLocation.y -= 0.004f;
		
		Transform tempFloatingDamageOutline = (Transform)Instantiate(uitxto, textLocation, Quaternion.identity);
		tempFloatingDamageOutline.GetComponent<FloatDmgScript>().DisplayDamage(value.ToString());

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

	private IEnumerator yoffsetReset()
	{
		while (true) {
			yield return new WaitForSeconds(0.1f);
			yoffsetr += 1;

			if (yoffsetr >= 4) yoffset = 0.05f;
		}
	}

	private IEnumerator damageFlash()
	{

		spr.material.SetFloat ("_FlashAmount", 0.8f);
		yield return new WaitForSeconds(0.1f);
		spr.material.SetFloat ("_FlashAmount", 0.0f);

	}
}
