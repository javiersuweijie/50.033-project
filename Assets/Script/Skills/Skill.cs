using UnityEngine;
using System.Collections;

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

	public abstract void Execute(PartyController friendly, PartyController enemy);

	protected IEnumerator SkillCooldown()
	{
		// Waits an amount of time
		yield return new WaitForSeconds(cdtime);
		
		// destory game object
		cooldown = true;
	}
}
