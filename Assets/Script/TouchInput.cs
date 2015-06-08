using UnityEngine;
using System.Collections;



public class TouchInput : MonoBehaviour {
	
	UnitHandler unitHandler;
	//public Player player = null;
	// Use this for initialization
	void Start () {
		unitHandler = gameObject.GetComponent<UnitHandler>();
		//Debug.Log ("Screen Width: " + Screen.width);
		//Debug.Log ("Screen Height: " + Screen.height);
	}
	
	// Update is called once per frame
	void Update () {
		
		Vector2 touchPos = new Vector2();
		
		#if UNITY_EDITOR
		if (Input.GetMouseButton(0)){
			touchPos = Input.mousePosition;
			if (touchPos.x < Screen.width/2.0){
				//Do Defense
				//Debug.Log ("Defending");
				//player.DefenseMode();
				unitHandler.PlayerInput(-1);

			}
			else{
				//Do Offense
				//Debug.Log ("Attacking");
				//player.AttackMode();
				unitHandler.PlayerInput(1);
			}
		}
		else{
			//Do Normal
			//Debug.Log ("Stoning");
			//player.NormalMode();
			unitHandler.PlayerInput(0);
		}
		#endif
		
		if (Input.touchCount > 0){
			touchPos = Input.GetTouch(0).position;
			if (touchPos.x < Screen.width/2.0){
				//Do Defense
			}
			else{
				//Do Offense
			}
		}
		else{
			//Do Normal
		}
	}
}
