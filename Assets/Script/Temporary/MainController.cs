using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainController : MonoBehaviour {

	private DataController dataController;

	void Start () {
		dataController = GameObject.FindWithTag("Data").GetComponent<DataController>();
		if (!dataController.IsLoaded()){
			dataController.Load();
		}
		transform.GetChild(1).GetComponent<Button>().onClick.AddListener(()=>{ToCharacterScreen();});
		transform.GetChild(2).GetComponent<Button>().onClick.AddListener(()=>{ToLevelScreen();});

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private void ToCharacterScreen(){
		Application.LoadLevel("Character");
	}
	
	private void ToLevelScreen(){
		Application.LoadLevel("Level");
	}
}
