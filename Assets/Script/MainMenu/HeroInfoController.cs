using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum LeftRight {
	Left, Right
}

public class SampleHero {
	public string icon_name = "Sprites/Icons/51325";
	public SampleSkill left_skill = new SampleSkill("Sprites/Icons/100006");
	public SampleSkill right_skill = new SampleSkill("Sprites/Icons/100092");
	public SampleWeapon weapon = new SampleWeapon("Sprites/Icons/2401");
	public int exp = 1543;
	public int max_exp = 2000;
	public int max_health = 1000;
	public int attack_power = 200;
	public int defence_power = 300;
	public int attack_speed = 12;
	public int critical_chance = 24;
	public int critical_damage = 56;
	public int level = 10;

	public string getIconName() {
		return icon_name;
	}
}

public class SampleWeapon {
	static public SampleWeapon[] weapons_list = {
		new SampleWeapon("Sprites/Icons/3401"),
		new SampleWeapon("Sprites/Icons/3402"),
		new SampleWeapon("Sprites/Icons/3403")
	};
	public string weapon_icon;
	public SampleWeapon(string icon) {
		weapon_icon = icon;
	}
}

public class SampleSkill {
	static public SampleSkill[] skill_list = {
		new SampleSkill("Sprites/Icons/100008"),
		new SampleSkill("Sprites/Icons/100010"), 
		new SampleSkill("Sprites/Icons/200005")
	};
	public string skill_icon;
	public SampleSkill(string icon) {
		skill_icon = icon;
	}
}

public class HeroInfoController : MonoBehaviour
{
	private SampleHero selected_unit;
	private GameObject unit_icon;
	private GameObject left_skill;
	private GameObject right_skill;
	private GameObject weapon;
	private GameObject unit_stats;
	private GameObject skill_selection;
	private GameObject weapon_selection;
	private Transform window;

	void Start() {
		gameObject.SetActive(false);
		window = transform.GetChild(0).GetChild(0);
		unit_icon = window.Find("UnitIcon").gameObject;
		left_skill = window.Find ("LeftSkill").gameObject;
		right_skill = window.Find("RightSkill").gameObject;
		weapon = window.Find("Weapon").gameObject;
		unit_stats = window.Find ("UnitStats").gameObject;
		SampleHero unit = new SampleHero();
		renderPlayerUnit(unit);
		gameObject.SetActive(true);

		left_skill.GetComponent<Button>().onClick.AddListener(()=> {
			renderSkills(LeftRight.Left);
		});
		right_skill.GetComponent<Button>().onClick.AddListener(()=> {
			renderSkills(LeftRight.Right);
		});
		weapon.GetComponent<Button>().onClick.AddListener(()=> {
			renderWeapons();
		});
	}

	public void renderPlayerUnit(SampleHero unit) {
		//TODO: Load from data storage
		selected_unit = unit;
		unit_icon.GetComponent<Image>().sprite = Resources.Load<Sprite>(unit.getIconName());
		left_skill.GetComponent<Image>().sprite = Resources.Load<Sprite>(unit.left_skill.skill_icon);
		right_skill.GetComponent<Image>().sprite = Resources.Load<Sprite>(unit.right_skill.skill_icon);
		weapon.GetComponent<Image>().sprite = Resources.Load<Sprite>(unit.weapon.weapon_icon);

		renderStats();
//		renderSkills(LeftRight.Left);
	}

	void renderStats() {
		hideAllSidebar();
		Debug.Log (unit_stats);
		unit_stats.SetActive(true);
		unit_stats.transform.Find("Level").GetComponent<Text>().text = "Lv." + selected_unit.level;
		unit_stats.transform.Find ("AttPower").GetComponent<Text>().text = selected_unit.attack_power.ToString();
		unit_stats.transform.Find ("DefPower").GetComponent<Text>().text = selected_unit.defence_power.ToString();
		unit_stats.transform.Find ("Health").GetComponent<Text>().text = selected_unit.max_health.ToString();
		unit_stats.transform.Find ("AttackSpeed").GetComponent<Text>().text = selected_unit.attack_speed.ToString();
		unit_stats.transform.Find ("CriticalChance").GetComponent<Text>().text = selected_unit.critical_chance.ToString();
		unit_stats.transform.Find ("CriticalDamage").GetComponent<Text>().text = selected_unit.critical_damage.ToString();
		unit_stats.transform.Find ("Exp").GetComponent<Text>().text = selected_unit.exp.ToString() + " / " + selected_unit.max_exp.ToString();
		unit_stats.transform.Find ("ExpBar").localScale = new Vector3(selected_unit.exp/(float)(selected_unit.max_exp),1);
	}

//	bool UpdatePlayerUnit() {
//		return false;
//	}
	
	void renderSkills(LeftRight leftright) {
		hideAllSidebar();
		skill_selection = Instantiate(Resources.Load <GameObject>("Prefabs/MenuUIPrefabs/SkillSelection"));
		skill_selection.transform.SetParent(window,false);
		skill_selection.SetActive(true);
		int i = 1;
		skill_selection.transform.Find("Close").GetComponent<Button>().onClick.AddListener(()=>{renderStats();});
		GameObject skill_template = skill_selection.transform.Find("SkillTemplate").gameObject;
		foreach (SampleSkill skill in SampleSkill.skill_list) {
			GameObject skill_button = Instantiate<GameObject>(skill_template);
			skill_button.transform.SetParent(skill_selection.transform,false);
			// TODO: Better way of arranging the skills. Might have to wait for finalised UI design
			skill_button.GetComponent<RectTransform>().anchoredPosition = new Vector2(49 + 64 * (i-1) + 10 * (i-1),-110);
			skill_button.GetComponent<Image>().sprite = Resources.Load<Sprite>(skill.skill_icon);
			skill_button.GetComponent<Button>().onClick.AddListener(()=>{changeSkill(leftright,skill);});
			skill_button.SetActive(true);
			i++;
		}
	}

	void renderWeapons() {
		hideAllSidebar();
		weapon_selection = Instantiate(Resources.Load <GameObject>("Prefabs/MenuUIPrefabs/WeaponSelection"));
		weapon_selection.transform.SetParent(window,false);
		weapon_selection.SetActive(true);
		int i = 1;
		weapon_selection.transform.Find("Close").GetComponent<Button>().onClick.AddListener(()=>{renderStats();});
		GameObject weapon_template = weapon_selection.transform.Find("WeaponTemplate").gameObject;
		foreach (SampleWeapon weapon in SampleWeapon.weapons_list) {
			GameObject weapon_button = Instantiate<GameObject>(weapon_template);
			weapon_button.transform.SetParent(weapon_selection.transform,false);
			// TODO: Better way of arranging the weapons. Might have to wait for finalised UI design
			weapon_button.GetComponent<RectTransform>().anchoredPosition = new Vector2(49 + 64 * (i-1) + 10 * (i-1),-110);
			weapon_button.GetComponent<Image>().sprite = Resources.Load<Sprite>(weapon.weapon_icon);
			weapon_button.GetComponent<Button>().onClick.AddListener(()=>{ changeWeapon(weapon); });
			weapon_button.SetActive(true);
			i++;
		}
		
	}

	void hideAllSidebar() {
		unit_stats.SetActive(false);
		Destroy(weapon_selection);
		Destroy(skill_selection);
	}

	void changeSkill(LeftRight leftright, SampleSkill skill) {
		if 		(leftright == LeftRight.Left) {
			selected_unit.left_skill = skill;
			left_skill.GetComponent<Image>().sprite = Resources.Load<Sprite>(skill.skill_icon);
			//TODO: Integrate with data storage
		}
		else if (leftright == LeftRight.Right) {
			selected_unit.right_skill = skill;
			right_skill.GetComponent<Image>().sprite = Resources.Load<Sprite>(skill.skill_icon);
			//TODO: Integrate with data storage
		}
	}

	void changeWeapon(SampleWeapon weapon) {
		selected_unit.weapon = weapon;
		this.weapon.GetComponent<Image>().sprite = Resources.Load<Sprite>(weapon.weapon_icon);
	}
}

