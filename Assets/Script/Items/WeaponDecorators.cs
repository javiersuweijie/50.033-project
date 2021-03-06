﻿using UnityEngine;
using System.Collections;

public class BonusDamangeDecorator : Weapon {

	int bonus_damage = 0;

	override public void preAttack(Object attacker, Object target) {
		this.preAttack(attacker, target);
	}

	override public int calculateDamage(Object attacker, Object target) {
		return bonus_damage + this.calculateDamage(attacker, target);
	}

	override public void postAttack(Object attacker, Object target) {
		this.postAttack(attacker, target);
	}
}
