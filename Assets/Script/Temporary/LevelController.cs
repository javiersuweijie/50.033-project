using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelController : MonoBehaviour {

	private DataController dataController;
	
	void Start () {
		dataController = GameObject.FindWithTag("Data").GetComponent<DataController>();

		if (!dataController.IsLoaded()){
			Application.LoadLevel("Main");
		}
		transform.GetChild(0).GetComponent<Button>().onClick.AddListener(()=>{ToMainScreen();});
		transform.GetChild(1).GetComponent<Button>().onClick.AddListener(()=>{ToDungeonScreen();});
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	private void ToMainScreen(){
		Application.LoadLevel("Main");
	}

	private void ToDungeonScreen(){
		Application.LoadLevel("Dungeon");
	}
}
