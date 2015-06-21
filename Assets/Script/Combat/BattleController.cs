using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class BattleController : MonoBehaviour{
	public GameObject player = null;
	public GameObject enemy = null;

	private GameObject[] playerObjects = new GameObject[3];
	private GameObject[] enemyObjects = new GameObject[3];
	private PlayerPartyController playerPartyController = null;
	private EnemyPartyController enemyPartyController = null;
	private GUIStyle currentStyle;

	void Start(){
		playerPartyController = GetComponent<PlayerPartyController>();
		enemyPartyController = GetComponent<EnemyPartyController>();
		for (int i = 0; i < 3; i++){
			playerObjects[i] = Instantiate(player, new Vector3(-(i * 2.0f + 4.0f), -(1.0f), 0), Quaternion.identity) as GameObject;
			playerPartyController.AddPlayerUnit(playerObjects[i].GetComponent<PlayerUnit>());
			enemyObjects[i] = Instantiate(enemy, new Vector3(i * 2.0f + 4.0f, -(1.0f), 0), Quaternion.identity) as GameObject;
			enemyPartyController.AddEnemyUnit(enemyObjects[i].GetComponent<EnemyUnit>());
		//GameObject arrow = Instantiate(projectile, transform.position + transform.TransformDirection(new Vector3(1,0,0)), transform.rotation) as GameObject;
		}
	}

	void Update(){
		for (int i = 0; i < 3; i++) {
			if (!playerPartyController.IsDead(i)){
				//Attack is handled by the units class attack function
//				playerPartyController.GetPlayerUnit(i).Attack(playerPartyController, enemyPartyController);

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

			if (!enemyPartyController.IsDead(i)){
				//Attack is handled by the units class attack function
//				enemyPartyController.GetEnemyUnit(i).Attack(enemyPartyController, playerPartyController);
				
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
		}
	}

	void OnGUI () {
		SetHPContainerStyle();
		for (int i = 0; i < 3; i ++){
			if (!playerPartyController.IsDead(i)){
				GUI.Box(new Rect(10, Screen.height-80+i*20, 200 , 10), "", currentStyle);
			}
			if (!enemyPartyController.IsDead(i)){
				GUI.Box(new Rect(Screen.width - 210, Screen.height-80+i*20, 200 , 10), "", currentStyle);
			}
		}
		SetHPFillStyle();
		for (int i = 0; i < 3; i ++){
			if (!playerPartyController.IsDead(i)){
				float barLength = (float)playerPartyController.GetPlayerUnit(i).GetCurrentHealth()/playerPartyController.GetPlayerUnit(i).GetMaxHealth() * 200;
				GUI.Box(new Rect(10, Screen.height-80+i*20, barLength, 10), "", currentStyle);
			}
			if (!enemyPartyController.IsDead(i)){
				float barLength = (float)enemyPartyController.GetEnemyUnit(i).GetCurrentHealth()/enemyPartyController.GetEnemyUnit(i).GetMaxHealth() * 200;
				GUI.Box(new Rect(Screen.width - 10 - barLength, Screen.height-80+i*20, barLength , 10), "", currentStyle);
			}
		}
	}

	private void SetHPContainerStyle()
	{
		currentStyle = new GUIStyle( GUI.skin.box );
		currentStyle.normal.background = MakeTex(  1, 1, new Color( 0f, 0f, 0f, 1f ) );
	}
	
	private void SetHPFillStyle()
	{
		currentStyle = new GUIStyle( GUI.skin.box );
		currentStyle.normal.background = MakeTex( 1, 1, new Color( 255f, 0f, 0f, 1f ) );
	}
	
	private Texture2D MakeTex( int width, int height, Color col )
	{
		Color[] pix = new Color[width * height];
		for( int i = 0; i < pix.Length; ++i )
		{
			pix[ i ] = col;
		}
		Texture2D result = new Texture2D( width, height );
		result.SetPixels( pix );
		result.Apply();
		return result;
	}

}
