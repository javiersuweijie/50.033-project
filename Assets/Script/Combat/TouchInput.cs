using UnityEngine;
using System.Collections;



public class TouchInput : MonoBehaviour {
	
	BattleController battleController;
	GameObject offenseEdge;
	GameObject defenseEdge;

	public GameObject oePrefab;
	public GameObject dePrefab;

	bool drain;

	void Start () {
		GameObject controller = GameObject.FindWithTag("Controller");
		if (controller != null){
			battleController = controller.GetComponent<BattleController>();
		}
		else{
			Debug.LogError("Cannot find controller object.");
		}
		//Debug.Log ("Screen Width: " + Screen.width);
		//Debug.Log ("Screen Height: " + Screen.height);
		drain = false;
	}
	
	// Update is called once per frame
	void Update () {
		
		Vector2 touchPos = new Vector2();
		
		#if UNITY_EDITOR
		if (Input.GetMouseButton(0)){

			drain = true;
			touchPos = Input.mousePosition;
			if (touchPos.x < Screen.width/2.0){
				//Do Defense
				//Debug.Log ("Defending");
				//player.DefenseMode();
				if (offenseEdge != null)
				{
					Destroy(offenseEdge);
					offenseEdge = null;
				}

				if (defenseEdge == null)
				{
					Vector3 spLocation = new Vector3(3.9f, 3.06f);
					defenseEdge = (GameObject)Instantiate(dePrefab, spLocation, Quaternion.identity);
				}

				battleController.ChangePlayerModeTo(-1);

			}
			else{
				//Do Offense
				//Debug.Log ("Attacking");
				//player.AttackMode();
				if (defenseEdge != null)
				{
					Destroy(defenseEdge);
					defenseEdge = null;
				}


				if (offenseEdge == null)
				{
					Vector3 spLocation = new Vector3(3.9f, 3.06f);
					offenseEdge = (GameObject)Instantiate(oePrefab, spLocation, Quaternion.identity);
				}
				battleController.ChangePlayerModeTo(1);
			}
		}
		else{

			drain = false;
			if (offenseEdge != null)
			{
				Destroy(offenseEdge);
				offenseEdge = null;
			}

			if (defenseEdge != null)
			{
				Destroy(defenseEdge);
				defenseEdge = null;
			}

			battleController.ChangePlayerModeTo(0);


		}
		
		if (drain){
			battleController.drainStam ();
		} 
		else
		{ 
			battleController.incStam();
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

	void FixedUpdate(){
	}
}
