using UnityEngine;
using System.Collections;
enum Rarity {
	Common,
	Rare,
	Epic,
	Unique
};
public abstract class Weapon : Item {
	protected int damage;
	Rarity rarity;

	public abstract void preAttack(Object attacker, Object target);
	public abstract int calculateDamage(Object attacker, Object target);
	public abstract void postAttack(Object attacker, Object target);
	
}

public class LameWeapons : Weapon {
	public LameWeapons(string name) {
		if (name == "Sword") {
			damage = 100;
			icon_name = "Sprites/Icons/3401";
		}
		else if (name == "Hammer") {
			damage = 200;
			icon_name = "Sprites/Icons/3402";
		}
		else if (name == "Staff") {
			damage = 50;
			icon_name = "Sprites/Icons/3403";
		}
		else if (name == "RareSword") {
			damage = 500;
			icon_name = "Sprites/Icons/4401";
		}
	}

	override public void preAttack(Object attacker, Object target) {
		return;
	}
	override public int calculateDamage(Object attacker, Object target) {
		return damage;
	}
	override public void postAttack(Object attacker, Object target) {
		return;
	}
}