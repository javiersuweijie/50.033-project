using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VictorySplash : MonoBehaviour {
	public Sprite overrideSprite;
	// Update is called once per frame
	public void ShowWin(int[] itemsList, PartyController playerPartyController, int expGain, DataController dc){
		string displayText = "EXP Gain : " + expGain + "\n\n";		
		for (int i = 0; i < 3; i++){
			int level = playerPartyController.GetUnit(i).GetUnit().GetLevel();
			int exp = playerPartyController.GetUnit(i).GetUnit().experience;
			int exptnl = (level + 1) * (level + 1) * 100 - exp;
			displayText += playerPartyController.GetUnit(i).GetUnit().name + " (LVL" + level + ") EXP : " + exptnl + "TNL\n\n";
		}
		GetComponentInChildren<Text>().text = displayText;
		Button button = transform.GetComponentInChildren<Button>();
		if (dc.lastStage){
			button.image.overrideSprite = overrideSprite;
			button.onClick.AddListener(()=>{ToMainScreen(dc);});
		}
		else{
			button.image.overrideSprite = null;
			button.onClick.AddListener(()=>{ToDungeonScreen(dc);});
		}
	}

	public void ToMainScreen(DataController dc){
		dc.Save();
		Application.LoadLevel("Main");
	}
	
	public void ToDungeonScreen(DataController dc){
		dc.Save();
		dc.stage += 1;
		Application.LoadLevel("OuterMap");
	}
}
