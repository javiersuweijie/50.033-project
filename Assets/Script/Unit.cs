using UnityEngine;
using System.Collections;

interface Unit {

	void Attack(Unit target);

	void UseSkill(Unit target);

	void IncreaseHealth(int value);
	
	void DecreaseHealth(int value);

	bool IsDead();

	int GetMaxHealth();

	int GetCurrentHealth();

	Vector3 GetPosition ();
}
