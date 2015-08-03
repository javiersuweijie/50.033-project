using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Type
{
	SwordArt,Holy,Strength,Elemental
}

public abstract class Skill : MonoBehaviour {
	protected float potency;
	protected float probability;
	protected string name;
	protected Type type;
	protected bool cooldown = false;
	protected float cdtime;
	protected int cost;
	protected string skillbanner;
	static public Dictionary<string, string> skill_icons = new Dictionary<string, string>{
		{"HolyBarrier","Sprites/Icons/100008"},
		{"LightningSlash","Sprites/Icons/100010"},
		{"GrenadeBarrage","Sprites/Icons/200005"}
	};

	public abstract void Execute(PartyController friendly, PartyController enemy, StaminaBar stambar);

	public int GetStamCost(){return cost;}

	protected IEnumerator SkillCooldown()
	{
		// Waits an amount of time
		yield return new WaitForSeconds(cdtime);
		
		// destory game object
		cooldown = true;
	}
}
