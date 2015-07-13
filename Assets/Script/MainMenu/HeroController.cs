using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class HeroController : MonoBehaviour {

	const int ListWidth = 3;
	SampleHero selected_hero = null;
	Transform selection_frame;
	Transform hero_list;
	Button confirm;
	GameObject hero_template;
	Text title;
	
	// HeroInfo
	Transform hero_icon;
	Transform hero_name;
	Transform hero_text;
	
	public void Init() {
		//General stuff
		title = transform.Find("Title").GetComponent<Text>();
		hero_list = transform.Find("ScrollView").transform.Find("HeroList");
		confirm = transform.Find("Confirm").GetComponent<Button>();
		Transform hero_info = transform.Find ("HeroInfo");
		
		//Hero list stuff
		hero_template = hero_list.Find("HeroTemplate").gameObject;
		selection_frame = hero_list.Find("Selection");
		
		//Hero info stuff
		hero_icon = hero_info.Find("HeroIcon");
		hero_name = hero_info.Find ("HeroName");
		hero_text = hero_info.Find ("HeroText");
	}
	
	// NOTE: Action class refers to a callback function
	public void renderHeroes(Action<SampleHero> onClose) {

		confirm.onClick.AddListener(()=>{onClose(selected_hero);});
		
		//		NOTE: we cannot use foreach here as the variable in foreach is shared and the scope in the delegate function (onClick listener) below will 
		//		share the same handle to hero
		
		//TODO: Link to real database
		for (int i = 0; i<SampleHero.hero_list.Length; i++) {
			SampleHero hero = SampleHero.hero_list[i];
			GameObject hero_button = Instantiate<GameObject>(hero_template);
			hero_button.transform.SetParent(hero_list,false);
			int row = i / ListWidth;
			int col = i % ListWidth;
			Vector2 button_position = new Vector2(49 + 74 * (col) ,-49 - 74 * row);
			// You need to use RectTransform for canvas positions
			hero_button.GetComponent<RectTransform>().anchoredPosition = button_position;
			hero_button.GetComponent<Image>().sprite = Resources.Load<Sprite>(hero.icon_name);
			hero_button.GetComponent<Button>().onClick.AddListener(()=>{
				selectHero(hero,button_position);
			});
			if (i==0) selectHero(hero,button_position);
			hero_button.SetActive(true);
			
		}
	}
	
	void selectHero(SampleHero hero, Vector2 position) {
		selection_frame.GetComponent<RectTransform>().anchoredPosition = position;
		selected_hero = hero;
//		changeInfo(hero);
	}
	
//	void changeInfo(SampleHero hero) {
//		//TODO: bad bad bad... need to store the sprite reference somewhere
//		hero_icon.GetComponent<Image>().sprite = Resources.Load<Sprite>(hero.icon_name);
//		hero_name.GetComponent<Text>().text = hero.hero_name;
//		hero_text.GetComponent<Text>().text = hero.hero_text;
//	}
}