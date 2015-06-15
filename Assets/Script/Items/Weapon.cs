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

	public abstract void preAttack(Object attacker, Object target);
	public abstract int calculateDamage(Object attacker, Object target);
	public abstract void postAttack(Object attacker, Object target);
}
