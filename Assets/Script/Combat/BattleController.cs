using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class BattleController : MonoBehaviour{
	public GameObject player = null;
	public GameObject enemy = null;

	private GameObject[] playerObjects = new GameObject[3];
	private GameObject[] enemyObjects = new GameObject[3];
	private PartyController playerPartyController = new PartyController();
	private PartyController enemyPartyController = new PartyController();
	private CombatGraphicalFunction cgf;

	private bool fighting, win, lose;

	void Start(){
		for (int i = 0; i < 3; i++){
			playerObjects[i] = Instantiate(player, new Vector3(-(i * 2.0f + 4.0f), -(1.0f), 0), Quaternion.identity) as GameObject;
			playerPartyController.AddUnit(playerObjects[i].GetComponent<Unit>());
			enemyObjects[i] = Instantiate(enemy, new Vector3(i * 2.0f + 4.0f, -(1.0f), 0), Quaternion.identity) as GameObject;
			enemyPartyController.AddUnit(enemyObjects[i].GetComponent<Unit>());
		//GameObject arrow = Instantiate(projectile, transform.position + transform.TransformDirection(new Vector3(1,0,0)), transform.rotation) as GameObject;
		}
		cgf = new CombatGraphicalFunction();
		fighting = true;
		win = false;
		lose = false;
		Debug.Log(enemyPartyController.GetAllTargets().Count);
	}

	void Update(){
		if (fighting){
			for (int i = 0; i < 3; i++) {
				if (!playerPartyController.IsDead(i) && !enemyPartyController.AllDead()){
					//Attack is handled by the units class attack function
					Unit player = playerPartyController.GetUnit(i);
					if (player != null){
						player.Attack(playerPartyController, enemyPartyController);
					}

					//Backup code
	//				if (!enemyUnitList[0].IsDead()){
	//					playerUnitList[i].Attack(enemyUnitList[0]);
	//				}
	//				else if (!enemyUnitList[1].IsDead()){
	//					playerUnitList[i].Attack (enemyUnitList[1]);
	//				}
	//				else if (!enemyUnitList[2].IsDead()){
	//					playerUnitList[i].Attack (enemyUnitList[2]);
	//				}
				}
				else if (playerPartyController.IsDead(i) && playerObjects[i] != null){
					Destroy(playerObjects[i]);
					playerObjects[i] = null;
				}
				else if (enemyPartyController.AllDead()){
					ShowWinScreen();
				}

				if (!enemyPartyController.IsDead(i) && !playerPartyController.AllDead()){
					//Attack is handled by the units class attack function
	//				enemyPartyController.GetEnemyUnit(i).Attack(enemyPartyController, playerPartyController);
					Unit enemy = enemyPartyController.GetUnit(i);
					if (enemy != null){
						enemy.Attack(enemyPartyController, playerPartyController);
					}
					//Backup code
	//				if (!playerUnitList[0].IsDead()){
	//					enemyUnitList[i].Attack(playerUnitList[0]);
	//				}
	//				else if (!playerUnitList[1].IsDead()){
	//					enemyUnitList[i].Attack (playerUnitList[1]);
	//				}
	//				else if (!playerUnitList[2].IsDead()){
	//					enemyUnitList[i].Attack (playerUnitList[2]);
	//				}
				}
				else if (enemyPartyController.IsDead(i) && enemyObjects[i] != null){
					Destroy(enemyObjects[i]);
					enemyObjects[i] = null;
				}
				else if (playerPartyController.AllDead()){
					ShowLoseScreen();
				}
			}
		}
		else{
			//Do some idle thing
		}
	}

	//A really rudimentary way of doing health bar.
	void OnGUI () {
		for (int i = 0; i < 3; i++){
			if (!playerPartyController.IsDead(i)){
				float playerFractionalHealth = Mathf.Clamp (playerPartyController.GetUnit(i).GetFractionalHealth(), 0.0f , 1.0f);
				cgf.DrawHealthBar(playerFractionalHealth, 10, 200, 10, Screen.height - 80 + i * 20);
			}
			if (!enemyPartyController.IsDead(i)){
				float enemyFractionalHealth = Mathf.Clamp (enemyPartyController.GetUnit(i).GetFractionalHealth(), 0.0f , 1.0f);
				cgf.DrawReversedHealthBar(enemyFractionalHealth, 10, 200, Screen.width - 10, Screen.height - 80 + i * 20);
				Debug.Log ("Enemy " + i + " has " + (enemyFractionalHealth * 100) + "% of HP left.");
			}

		}

		if (win){
			cgf.ShowWin();
		}

		if (lose){
			cgf.ShowLose();
		}
	}

	public void ChangePlayerModeTo(int mode) {
		playerPartyController.ChangeModeTo(mode);
	}

	private void ShowWinScreen(){
		Debug.Log ("Win!");
		win = true;
		fighting = false;
	}
	
	private void ShowLoseScreen(){
		Debug.Log ("Lose!");
		lose = true;
		fighting = false;
	}

}
