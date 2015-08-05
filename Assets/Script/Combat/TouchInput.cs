using UnityEngine;
using System.Collections;



public class TouchInput : MonoBehaviour {
	
	BattleController battleController;
	GameObject offenseEdge;
	GameObject defenseEdge;

	public GameObject oePrefab;
	public GameObject dePrefab;

	bool drain, firstClick;

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
		firstClick = false;
		drain = false;
	}
	
	// Update is called once per frame
	void Update () {
		
		Vector2 touchPos = new Vector2();
		if (!battleController.fighting){
			if (offenseEdge != null){
				Destroy(offenseEdge);
				offenseEdge = null;
			}
			if (defenseEdge != null){
				Destroy(defenseEdge);
				defenseEdge = null;
			}
		}
		#if UNITY_EDITOR
		if (Input.GetMouseButton(0)){
			if (drain == false){
				firstClick = true;
				drain = true;
			}
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
					if (battleController.fighting){
						defenseEdge = (GameObject)Instantiate(dePrefab, spLocation, Quaternion.identity);
					}
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
					if (battleController.fighting){
						offenseEdge = (GameObject)Instantiate(oePrefab, spLocation, Quaternion.identity);
					}
				}
				battleController.ChangePlayerModeTo(1);
			}
		}
		else{
			firstClick = false;
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
		#endif
		
		if (Input.touchCount > 0){
			if (drain == false){
				firstClick = true;
				drain = true;
			}
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
		
		if (firstClick){
			battleController.drainStam (20);
			firstClick = false;
		}
		if (drain){
			battleController.drainStam (1);
		} 
		else
		{ 
			battleController.incStam(1);
		}
	}

	void FixedUpdate(){
	}
}
