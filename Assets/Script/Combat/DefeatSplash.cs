using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DefeatSplash : MonoBehaviour {
	// Update is called once per frame
	public void ShowLose(DataController dc){
		Button button = transform.GetComponentInChildren<Button>();
		button.onClick.AddListener(()=>{ToMainScreen(dc);});
	}

	public void ToMainScreen(DataController dc){
		dc.Save();
		Application.LoadLevel("Main");
	}
}
