using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public enum LeftRight {
	Left, Right
}

public class SampleSkill {
	static public SampleSkill[] skill_list = {
		new SampleSkill("Sprites/Icons/100008"),
		new SampleSkill("Sprites/Icons/100010"), 
		new SampleSkill("Sprites/Icons/200005"), 
		new SampleSkill("Sprites/Icons/200005"), 
		new SampleSkill("Sprites/Icons/200005"), 
		new SampleSkill("Sprites/Icons/200005"), 
		new SampleSkill("Sprites/Icons/200005")
	};
	static int skill_number = 0;
	public string skill_icon;
	public string skill_name;
	public string skill_text = "brain freeze. look ma i'm road kill i just heard about evans new position,good luck to you evan backstabber, bastard, i mean baxter. excuse me, i'd like to ass you a few questions.";
	public SampleSkill(string icon) {
		skill_icon = icon;
		skill_name = "Skill "+skill_number.ToString();
		skill_number++;
	}
}

public class HeroInfoController : MonoBehaviour
{

	private PlayerUnit selected_hero;
	public List<PlayerUnit> all_units;
	public List<Weapon> all_weapons;
	private int selected_hero_index;
	private GameObject unit_icon;
	private GameObject left_skill;
	private GameObject right_skill;
	private GameObject weapon;
	private GameObject unit_stats;
	private GameObject skill_selection;
	private GameObject weapon_selection;
	private GameObject hero_selection;
	private GameObject selected_heroes;
	private DataController data_controller;
	private Transform window;

	void Awake(){
		data_controller = GameObject.FindWithTag("Data").GetComponent<DataController>();
		
		if (!data_controller.IsLoaded()){
			Application.LoadLevel("Main");
		}
	}

	void Start() {

		gameObject.SetActive(false);
		window = transform.Find("Window");
		unit_icon = window.Find("UnitIcon").gameObject;
		left_skill = window.Find ("LeftSkill").gameObject;
		right_skill = window.Find("RightSkill").gameObject;
		weapon = window.Find("Weapon").gameObject;
		unit_stats = window.Find ("UnitStats").gameObject;
		selected_heroes = window.Find("SelectedHeroes").gameObject;
		Debug.Log(data_controller.IsLoaded());
		all_units = data_controller.getAllUnits();
		// this is called to setup the subject of the controller
		selected_hero = all_units[data_controller.activeUnitsIndex[0]];
		selected_hero_index = data_controller.activeUnitsIndex[0];

		all_weapons = data_controller.getAllWeapons();

		defaultHeroSelection();

		setupButtons();
	}

	public void renderSelectedHeroes() {
		RectTransform selection_frame = selected_heroes.transform.Find("Selection").GetComponent<RectTransform>();
		for (int i = 0; i<3 ; i++) {
			int index = i;
			PlayerUnit hero = all_units[data_controller.activeUnitsIndex[i]];
			Transform hero_button = selected_heroes.transform.GetChild(i);
			hero_button.GetComponent<Image>().sprite = Resources.Load<Sprite>(hero.icon_name);
			hero_button.GetComponent<Button>().onClick.RemoveAllListeners();
			hero_button.GetComponent<Button>().onClick.AddListener(()=>{
				selection_frame.anchoredPosition = hero_button.GetComponent<RectTransform>().anchoredPosition;
				selected_hero_index = index;
				selected_hero = hero;
				renderPlayerUnit();
			});
			selected_heroes.transform.GetChild(i).gameObject.SetActive(true);
		}
	}

	public void renderPlayerUnit() {
		//TODO: Load from data storage
		unit_icon.GetComponent<Image>().sprite = Resources.Load<Sprite>(selected_hero.getIconName());
		if (selected_hero.left_spell_type != null)
		left_skill.GetComponent<Image>().sprite = Resources.Load<Sprite>(Skill.skill_icons[selected_hero.left_spell_type.ToString()]);
		else left_skill.GetComponent<Image>().sprite = null;
		if (selected_hero.right_spell_type != null)
		right_skill.GetComponent<Image>().sprite = Resources.Load<Sprite>(Skill.skill_icons[selected_hero.right_spell_type.ToString()]);
		else right_skill.GetComponent<Image>().sprite = null;
		weapon.GetComponent<Image>().sprite = Resources.Load<Sprite>(all_weapons[data_controller.unitInfoList[selected_hero_index].equip].icon_name);

		renderStats();
		gameObject.SetActive(true);
	}

	void renderStats() {
		unit_stats.SetActive(true);
		unit_stats.transform.Find("Level").GetComponent<Text>().text = "Lv." + selected_hero.GetLevel();
		unit_stats.transform.Find ("AttPower").GetComponent<Text>().text = selected_hero.attack_power.ToString();
		unit_stats.transform.Find ("DefPower").GetComponent<Text>().text = selected_hero.defence_power.ToString();
		unit_stats.transform.Find ("Health").GetComponent<Text>().text = selected_hero.max_health.ToString();
		unit_stats.transform.Find ("AttackSpeed").GetComponent<Text>().text = selected_hero.attack_speed.ToString();
		unit_stats.transform.Find ("CriticalChance").GetComponent<Text>().text = selected_hero.critical_chance.ToString();
		unit_stats.transform.Find ("CriticalDamage").GetComponent<Text>().text = selected_hero.critical_damage.ToString();
		unit_stats.transform.Find ("Exp").GetComponent<Text>().text = selected_hero.experience.ToString() + " / " + selected_hero.GetTNL().ToString();
		unit_stats.transform.Find ("ExpBar").localScale = new Vector3(selected_hero.experience/(float)(selected_hero.GetTNL()),1);
	}

	void defaultHeroSelection() {
		renderSelectedHeroes();
		renderPlayerUnit();
		hero_selection = Instantiate(Resources.Load<GameObject>("Prefabs/MenuUIPrefabs/HeroSelection"));
		hero_selection.SetActive(true);
		hero_selection.transform.SetParent(window,false);
		HeroController hero_controller = hero_selection.GetComponent<HeroController>();
		hero_controller.Init();
		hero_controller.renderHeroes(changeHero,all_units,data_controller.activeUnitsIndex);
	}

	void hideDefault() {
		hero_selection.SetActive(false);
		Destroy (skill_selection);
		Destroy (weapon_selection);
	}

	void changeSkill(LeftRight leftright, SampleSkill skill) {
		Destroy (skill_selection);
		hero_selection.SetActive(true);
//		if (skill == null) return;
//		if 		(leftright == LeftRight.Left) {
//			selected_hero.left_skill = skill;
//			left_skill.GetComponent<Image>().sprite = Resources.Load<Sprite>(skill.skill_icon);
//			//TODO: Integrate with data storage
//		}
//		else if (leftright == LeftRight.Right) {
//			selected_hero.right_skill = skill;
//			right_skill.GetComponent<Image>().sprite = Resources.Load<Sprite>(skill.skill_icon);
//			//TODO: Integrate with data storage
//		}
	}

	void changeWeapon(int _weapon_index) {
		Destroy(weapon_selection);
		hero_selection.SetActive(true);
		if (_weapon_index == -1) return;
		data_controller.unitInfoList[selected_hero_index].equip = _weapon_index;
		weapon.GetComponent<Image>().sprite = Resources.Load<Sprite>(all_weapons[data_controller.unitInfoList[selected_hero_index].equip].icon_name);
	}

	public void changeHero(int hero_index) {
		//TODO: Cannot select two of the same hero

		Destroy(hero_selection);
		data_controller.activeUnitsIndex[selected_hero_index] = hero_index;
		selected_hero = all_units[hero_index];
		renderSelectedHeroes();
		defaultHeroSelection();
	}

	void setupButtons() {		
		left_skill.GetComponent<Button>().onClick.AddListener(()=> {
			hideDefault();
			skill_selection = Instantiate(Resources.Load <GameObject>("Prefabs/MenuUIPrefabs/SkillSelection"));
			skill_selection.SetActive(true);
			SkillController controller = skill_selection.GetComponent<SkillController>();
			controller.Init();
			// renderSkills(LeftRight, Action) Action is a callback function that is called when the skill controller is done
			controller.renderSkills(LeftRight.Left,changeSkill);
			skill_selection.transform.SetParent(window,false);
		});
		right_skill.GetComponent<Button>().onClick.AddListener(()=> {
			hideDefault();
			skill_selection = Instantiate(Resources.Load <GameObject>("Prefabs/MenuUIPrefabs/SkillSelection"));
			skill_selection.SetActive(true);
			SkillController controller = skill_selection.GetComponent<SkillController>();
			controller.Init();
			// renderSkills(LeftRight, Action) Action is a callback function that is called when the skill controller is done
			controller.renderSkills(LeftRight.Right,changeSkill);
			skill_selection.transform.SetParent(window,false);
		});
		weapon.GetComponent<Button>().onClick.AddListener(()=> {
			hideDefault();
			weapon_selection = Instantiate(Resources.Load <GameObject>("Prefabs/MenuUIPrefabs/WeaponSelection"));
			weapon_selection.SetActive(true);
			WeaponController controller = weapon_selection.GetComponent<WeaponController>();
			controller.Init();
			controller.renderWeapons(changeWeapon,all_weapons);
			weapon_selection.transform.SetParent(window,false);
		});
	}

	
	public void ToMainScreen(){
		Application.LoadLevel("Main");
	}
}

