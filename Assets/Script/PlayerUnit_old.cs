//using UnityEngine;
//using System.Collections;
//
//public abstract class PlayerUnit : MonoBehaviour, Unit {
//
//	protected float lastAtkTime;
//	protected float defaultAtkDelay;
//	protected int defaultAtkPower;
//	protected int maxHealth;
//	protected float atkDelay;
//	protected int atkPower;
//	protected int currentHealth;
//	protected bool dead;
//	protected bool atkMode;
//	protected bool defMode;
//	protected Transform uitxt;
//	
//	public void Init (float creationTime) {
//		uitxt = (Transform)Resources.Load ("Prefabs/FloatDmgText", typeof(Transform));
//		lastAtkTime = creationTime;
//		this.defaultAtkDelay = 1.0f;
//		this.defaultAtkPower = 5;
//		this.maxHealth = 100;
//		atkDelay = defaultAtkDelay;
//		atkPower = defaultAtkPower;
//		currentHealth = maxHealth;
//		dead = false;
//		atkMode = false;
//		defMode = false;
//	}
//
//	public void Init (float creationTime, float defaultAtkDelay, int defaultAtkPower, int maxHealth) {
//		uitxt = (Transform)Resources.Load ("Prefabs/FloatDmgText", typeof(Transform));
//		lastAtkTime = creationTime;
//		this.defaultAtkDelay = defaultAtkDelay;
//		this.defaultAtkPower = defaultAtkPower;
//		this.maxHealth = maxHealth;
//		atkDelay = defaultAtkDelay;
//		atkPower = defaultAtkPower;
//		currentHealth = maxHealth;
//		dead = false;
//		atkMode = false;
//		defMode = false;
//	}
//
//	public abstract void Attack();
//
//	public abstract void UseSkill(Unit target);
//	
//	public void IncreaseHealth(int value){
//		currentHealth += value;
//		if (currentHealth >= maxHealth){
//			currentHealth = maxHealth;
//		}
//	}
//	
//	public void DecreaseHealth(int value){
//		Vector3 textLocation = Camera.main.WorldToScreenPoint(transform.position);
//		textLocation.x /= Screen.width;
//		textLocation.x += Random.Range(-0.05f,0.02f);
//		textLocation.y /= Screen.height;
//		textLocation.y += Random.Range(0.05f,0.07f);
//		Transform tempFloatingDamage = (Transform)Instantiate(uitxt, textLocation, Quaternion.identity);
//		tempFloatingDamage.GetComponent<FloatDmgScript>().DisplayDamage("-" + atkPower.ToString());
//
//		currentHealth -= value;
//		if (currentHealth <= 0){
//			currentHealth = 0;
//			dead = true;
//			Destroy(gameObject);
//		}
//	}
//
//	public bool IsDead(){
//		return dead;
//	}
//
//	public Vector3 GetPosition()
//	{
//		return transform.position;
//	}
//
//	void OnTriggerEnter (Collider other) {
//		Destroy(other.gameObject);
//		DecreaseHealth (5);
//	}
//
//	public int GetMaxHealth(){
//		return maxHealth;
//	}
//	
//	public int GetCurrentHealth(){
//		return currentHealth;
//	}
//
//	public void OffensiveMode(){
//		atkDelay = defaultAtkDelay * 0.5f;
//		atkMode = true;
//		defMode = false;
//	}
//
//	public void DefensiveMode(){
//		atkDelay = defaultAtkDelay * 1.5f;
//		defMode = true;
//		atkMode = false;
//	}
//	
//	public void NeutralMode(){
//		atkDelay = defaultAtkDelay;
//		defMode = false;
//		atkMode = false;
//	}
//	
//	public bool IsAttacking(){
//		return atkMode;
//	}
//	
//	public bool IsDefending(){
//		return defMode;
//	}
//}
