using UnityEngine;
using System.Collections;

abstract class PlayerUnit : MonoBehaviour, Unit {

	protected float lastAtkTime;
	protected float defaultAtkDelay;
	protected int defaultAtkPower;
	protected int maxHealth;
	protected float atkDelay;
	protected int atkPower;
	protected int currentHealth;
	protected bool dead;
	protected bool atkMode;
	protected bool defMode;
	
	public void Init (float creationTime) {
		lastAtkTime = creationTime;
		this.defaultAtkDelay = 1.0f;
		this.defaultAtkPower = 5;
		this.maxHealth = 100;
		atkDelay = defaultAtkDelay;
		atkPower = defaultAtkPower;
		currentHealth = maxHealth;
		dead = false;
		atkMode = false;
		defMode = false;
	}

	public void Init (float creationTime, float defaultAtkDelay, int defaultAtkPower, int maxHealth) {
		lastAtkTime = creationTime;
		this.defaultAtkDelay = defaultAtkDelay;
		this.defaultAtkPower = defaultAtkPower;
		this.maxHealth = maxHealth;
		atkDelay = defaultAtkDelay;
		atkPower = defaultAtkPower;
		currentHealth = maxHealth;
		dead = false;
		atkMode = false;
		defMode = false;
	}

	public abstract void Attack(Unit target);

	public abstract void UseSkill(Unit target);
	
	public void IncreaseHealth(int value){
		currentHealth += value;
		if (currentHealth >= maxHealth){
			currentHealth = maxHealth;
		}
	}
	
	public void DecreaseHealth(int value){
		currentHealth -= value;
		if (currentHealth <= 0){
			currentHealth = 0;
			dead = true;
			Destroy(gameObject);
		}
	}

	public bool IsDead(){
		return dead;
	}
	
	public int GetMaxHealth(){
		return maxHealth;
	}
	
	public int GetCurrentHealth(){
		return currentHealth;
	}

	public void OffensiveMode(){
		atkDelay = defaultAtkDelay * 0.5f;
		atkMode = true;
		defMode = false;
	}

	public void DefensiveMode(){
		atkDelay = defaultAtkDelay * 1.5f;
		defMode = true;
		atkMode = false;
	}
	
	public void NeutralMode(){
		atkDelay = defaultAtkDelay;
		defMode = false;
		atkMode = false;
	}
	
	public bool IsAttacking(){
		return atkMode;
	}
	
	public bool IsDefending(){
		return defMode;
	}
}
