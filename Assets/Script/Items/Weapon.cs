using UnityEngine;
using System.Collections;
enum Rarity {
	Common,
	Rare,
	Epic,
	Unique
};
public abstract class Weapon : Item {
	int damage;
	Rarity rarity;

	void preAttack(Object attacker, Object target);
	void calculateDamage(Object attacker, Object target);
	void postAttack(Object attacker, Object target);
}
