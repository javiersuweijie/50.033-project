using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelController : MonoBehaviour {

	private DataController dataController;
	
	
	void Awake(){
		dataController = GameObject.FindWithTag("Data").GetComponent<DataController>();
		
		if (!dataController.IsLoaded()){
			Application.LoadLevel("Main");
		}
	}

	void Start () {
		transform.GetChild(1).GetComponent<Button>().onClick.AddListener(()=>{ToMainScreen();});
		transform.GetChild(2).GetComponent<Button>().onClick.AddListener(()=>{ToDungeonScreen();});
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	private void ToMainScreen(){
		Application.LoadLevel("Main");
	}

	private void ToDungeonScreen(){
		dataController.hpRemaining = new float[3];
		dataController.hpRemaining[0] = 1.0f;
		dataController.hpRemaining[1] = 1.0f;
		dataController.hpRemaining[2] = 1.0f;
		dataController.stamRemaining = 1000;
		dataController.lastStage = false;
		dataController.stage = 0;
		Application.LoadLevel("OuterMap");
	}
}
