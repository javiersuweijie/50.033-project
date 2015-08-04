using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour {
	
	private DataController dataController;
	
	void Start () {
		dataController = GameObject.FindWithTag("Data").GetComponent<DataController>();
		
		if (!dataController.IsLoaded()){
			Application.LoadLevel("Main");
		}
	}
	
	private void ToMainScreen(){
		Application.LoadLevel("Main");
	}
}
