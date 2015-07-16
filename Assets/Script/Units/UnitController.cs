using UnityEngine;
using System.Collections;

public class UnitController : MonoBehaviour {


	protected Transform uitxt = (Transform)Resources.Load ("Prefabs/FloatDmgText", typeof(Transform));
	protected Transform uitxto = (Transform)Resources.Load ("Prefabs/FloatDmgTextOutline", typeof(Transform));
	public Transform skillflashO = (Transform)Resources.Load ("GFXAnim/SkillGlow/skill1", typeof(Transform));
	protected GameObject attackprefab;
	public Unit unit;
	protected Animator anim;
	protected SpriteRenderer spr;
	public string skillanim;
	public SkillAnimController sac;

	//for damage text
	protected float yoffset = 0.05f;
	protected int yoffsetr = 0;

	public bool setup_ready = false;

	public void InitializeSAC(SkillAnimController in_sac){
		sac = in_sac;
	}


	public void TakeDamage(int value, string attack_particle = null) {
		int dmg = unit.TakeDamage(value);

		Vector3 textLocation = Camera.main.WorldToScreenPoint(transform.position);
		textLocation.x /= Screen.width;
		textLocation.x += Random.Range(-0.03f,0.03f);
		textLocation.y /= Screen.height;
		textLocation.y += yoffset;
		
		yoffset += (0.03f);
		yoffsetr = 0;

		if (yoffset > 0.18f) yoffset = 0.04f;
		
		Transform tempFloatingDamage = (Transform)Instantiate(uitxt, textLocation, Quaternion.identity);
		tempFloatingDamage.GetComponent<FloatDmgScript>().DisplayDamage(dmg.ToString());
		
		textLocation.x += 0.003f;
		textLocation.y -= 0.004f;
		
		Transform tempFloatingDamageOutline = (Transform)Instantiate(uitxto, textLocation, Quaternion.identity);
		tempFloatingDamageOutline.GetComponent<FloatDmgScript>().DisplayDamage(dmg.ToString());
		StartCoroutine (damageFlash ());

		if (attack_particle != null) {
			// According to Unity, Resouces.Load is cached. So loading multiple times should be fine.
			GameObject particle = Resources.Load<GameObject>(attack_particle);
			Instantiate(particle, this.transform.position, Quaternion.Euler (0,0,270));
		}
	}

	public void ReceiveHeal(int value, string attack_particle = null) {
		int heal = unit.ReceiveHeal(value);
		Vector3 textLocation = Camera.main.WorldToScreenPoint(transform.position);
		textLocation.x /= Screen.width;
		textLocation.x += Random.Range(-0.03f,0.03f);
		textLocation.y /= Screen.height;
		textLocation.y += yoffset;
		
		yoffset += (0.03f);
		yoffsetr = 0;
		
		if (yoffset > 0.18f) yoffset = 0.04f;
		
		Transform tempFloatingDamage = (Transform)Instantiate(uitxt, textLocation, Quaternion.identity);
		tempFloatingDamage.GetComponent<FloatDmgScript>().DisplayDamage(value.ToString());
		tempFloatingDamage.GetComponent<GUIText> ().color = new Color (0.3f, 1f, 0.3f);
		
		textLocation.x += 0.003f;
		textLocation.y -= 0.004f;
		
		Transform tempFloatingDamageOutline = (Transform)Instantiate(uitxto, textLocation, Quaternion.identity);
		tempFloatingDamageOutline.GetComponent<FloatDmgScript>().DisplayDamage(heal.ToString());

		if (attack_particle != null) {
			GameObject particle = Resources.Load<GameObject>(attack_particle);
			Instantiate(particle, this.transform.position, Quaternion.Euler (0,0,270));
		}
	}

	public void Attack(PartyController allies, PartyController enemies, StaminaBar stambar) {

		if (unit.Attack(allies,enemies,stambar)) {
			anim.SetInteger("animController", 1);
			StartCoroutine (UseSkill(allies, enemies, stambar));
		}

	}

	public IEnumerator UseSkill(PartyController allies, PartyController enemies, StaminaBar stambar) {
		yield return new WaitForSeconds (0.1f);
		
		anim.SetInteger("animController", 0);
		yield return new WaitForSeconds(0.4f);
		unit.UseSkill(allies,enemies,stambar);
	}

	private IEnumerator yoffsetReset()
	{
		while (true) {
			yield return new WaitForSeconds(0.1f);
			yoffsetr += 1;
			
			if (yoffsetr >= 4) yoffset = 0.05f;
		}
	}
	
	private IEnumerator damageFlash()
	{
		
		spr.material.SetFloat ("_FlashAmount", 0.8f);
		yield return new WaitForSeconds(0.1f);
		spr.material.SetFloat ("_FlashAmount", 0.0f);
		
	}

	public void AttachUnit(Unit _unit) {
		unit = _unit;
		anim = this.GetComponent<Animator> ();
		spr = this.GetComponent<SpriteRenderer> ();
		StartCoroutine (yoffsetReset ());
		spr.sprite = Resources.Load<Sprite>(unit.sprite_name);
		anim.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load (unit.runTimeAnimatorController);
		setup_ready = true;
		gameObject.AddComponent<BuffManager>();
		unit.buffManager = this.GetComponent<BuffManager>();
		if (unit.right_spell_type != null) {
			gameObject.AddComponent(unit.right_spell_type);
			unit.right_spell = (Skill) gameObject.GetComponent(unit.right_spell_type);
		}
		if (unit.left_spell_type != null) {
			gameObject.AddComponent(unit.left_spell_type);
			unit.left_spell = (Skill) gameObject.GetComponent(unit.left_spell_type);
		}
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
