using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class SkillController : MonoBehaviour
{
	const int ListWidth = 3;
	SampleSkill selected_skill = null;
	Transform selection_frame;
	Transform skill_list;
	Button cancel;
	Button confirm;
	GameObject skill_template;
	Text title;

	// SkillInfo
	Transform skill_icon;
	Transform skill_name;
	Transform skill_text;

	public void Init() {
		//General stuff
		title = transform.Find("Title").GetComponent<Text>();
		skill_list = transform.Find("ScrollView").transform.Find("SkillList");
		cancel = transform.Find("Cancel").GetComponent<Button>();
		confirm = transform.Find("Confirm").GetComponent<Button>();
		Transform skill_info = transform.Find ("SkillInfo");

		//Skill list stuff
		skill_template = skill_list.Find("SkillTemplate").gameObject;
		selection_frame = skill_list.Find("Selection");

		//Skill info stuff
		skill_icon = skill_info.Find("SkillIcon");
		skill_name = skill_info.Find ("SkillName");
		skill_text = skill_info.Find ("SkillText");
	}

	// NOTE: Action class refers to a callback function
	public void renderSkills(LeftRight leftright, Action<LeftRight,SampleSkill> onClose) {

		if (leftright == LeftRight.Left) title.text = "Defensive Edge";
		else title.text = "Offensive Edge";

		cancel.onClick.AddListener(()=>{onClose(leftright,null);});
		confirm.onClick.AddListener(()=>{onClose(leftright,selected_skill);});

//		NOTE: we cannot use foreach here as the variable in foreach is shared and the scope in the delegate function (onClick listener) below will 
//		share the same handle to skill

		//TODO: Link to real database
		for (int i = 0; i<SampleSkill.skill_list.Length; i++) {
			SampleSkill skill = SampleSkill.skill_list[i];
			GameObject skill_button = Instantiate<GameObject>(skill_template);
			skill_button.transform.SetParent(skill_list,false);
			int row = i / ListWidth;
			int col = i % ListWidth;
			Vector2 button_position = new Vector2(49 + 74 * (col) ,-49 - 74 * row);
			// You need to use RectTransform for canvas positions
			skill_button.GetComponent<RectTransform>().anchoredPosition = button_position;
			skill_button.GetComponent<Image>().sprite = Resources.Load<Sprite>(skill.skill_icon);
			skill_button.GetComponent<Button>().onClick.AddListener(()=>{
				selectSkill(skill,button_position);
			});
			if (i==0) selectSkill(skill,button_position);
			skill_button.SetActive(true);

		}
	}

	void selectSkill(SampleSkill skill, Vector2 position) {
		selection_frame.GetComponent<RectTransform>().anchoredPosition = position;
		selected_skill = skill;
		changeInfo(skill);
	}
	
	void changeInfo(SampleSkill skill) {
		//TODO: bad bad bad... need to store the sprite reference somewhere
		skill_icon.GetComponent<Image>().sprite = Resources.Load<Sprite>(skill.skill_icon);
		skill_name.GetComponent<Text>().text = skill.skill_name;
		skill_text.GetComponent<Text>().text = skill.skill_text;
	}
}

