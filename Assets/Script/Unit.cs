using UnityEngine;
using System.Collections;

public interface Unit {

	void Attack();

	void UseSkill(Unit target);

	void IncreaseHealth(int value);
	
	void DecreaseHealth(int value);

	bool IsDead();

	int GetMaxHealth();

	int GetCurrentHealth();

	Vector3 GetPosition ();
}
