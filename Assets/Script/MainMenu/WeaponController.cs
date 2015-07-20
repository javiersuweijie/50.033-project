using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class WeaponController : MonoBehaviour
{
	const int ListWidth = 3;
	SampleWeapon selected_weapon = null;
	Transform selection_frame;
	Transform weapon_list;
	Button cancel;
	Button confirm;
	GameObject weapon_template;
	Text title;
	
	// WeaponInfo
	Transform weapon_icon;
	Transform weapon_name;
	Transform weapon_text;
	
	public void Init() {
		//General stuff
		title = transform.Find("Title").GetComponent<Text>();
		weapon_list = transform.Find("ScrollView").transform.Find("WeaponList");
		cancel = transform.Find("Cancel").GetComponent<Button>();
		confirm = transform.Find("Confirm").GetComponent<Button>();
		Transform weapon_info = transform.Find ("WeaponInfo");
		
		//Weapon list stuff
		weapon_template = weapon_list.Find("WeaponTemplate").gameObject;
		selection_frame = weapon_list.Find("Selection");
		
		//Weapon info stuff
		weapon_icon = weapon_info.Find("WeaponIcon");
		weapon_name = weapon_info.Find ("WeaponName");
		weapon_text = weapon_info.Find ("WeaponText");
	}
	
	// NOTE: Action class refers to a callback function
	public void renderWeapons(Action<SampleWeapon> onClose) {
		
		cancel.onClick.AddListener(()=>{onClose(null);});
		confirm.onClick.AddListener(()=>{onClose(selected_weapon);});
		
		//		NOTE: we cannot use foreach here as the variable in foreach is shared and the scope in the delegate function (onClick listener) below will 
		//		share the same handle to weapon
		
		//TODO: Link to real database
		for (int i = 0; i<SampleWeapon.weapons_list.Length; i++) {
			SampleWeapon weapon = SampleWeapon.weapons_list[i];
			GameObject weapon_button = Instantiate<GameObject>(weapon_template);
			weapon_button.transform.SetParent(weapon_list,false);
			int row = i / ListWidth;
			int col = i % ListWidth;
			Vector2 button_position = new Vector2(49 + 74 * (col) ,-49 - 74 * row);
			// You need to use RectTransform for canvas positions
			weapon_button.GetComponent<RectTransform>().anchoredPosition = button_position;
			weapon_button.GetComponent<Image>().sprite = Resources.Load<Sprite>(weapon.weapon_icon);
			weapon_button.GetComponent<Button>().onClick.AddListener(()=>{
				selectWeapon(weapon,button_position);
			});
			if (i==0) selectWeapon(weapon,button_position);
			weapon_button.SetActive(true);
			
		}
	}
	
	void selectWeapon(SampleWeapon weapon, Vector2 position) {
		selection_frame.GetComponent<RectTransform>().anchoredPosition = position;
		selected_weapon = weapon;
		changeInfo(weapon);
	}
	
	void changeInfo(SampleWeapon weapon) {
		//TODO: bad bad bad... need to store the sprite reference somewhere
//		weapon_icon.GetComponent<Image>().sprite = Resources.Load<Sprite>(weapon.weapon_icon);
//		weapon_name.GetComponent<Text>().text = weapon.weapon_name;
//		weapon_text.GetComponent<Text>().text = weapon.weapon_text;
	}
}

