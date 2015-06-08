using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class UnitHandler : MonoBehaviour{
	public GameObject player = null;
	public GameObject enemy = null;

	private PlayerUnit[] playerUnitList = new PlayerUnit[3];
	private EnemyUnit[] enemyUnitList = new EnemyUnit[3];
	private GUIStyle currentStyle;

	void Start(){
		for (int i = 0; i < 3; i++){
			GameObject playerUnit = Instantiate(player, new Vector3(-(i * 2.0f + 4.0f), -(1.0f), 0), Quaternion.identity) as GameObject;
			playerUnitList[i] = playerUnit.GetComponent<DefaultPlayerUnit>();
			
			GameObject enemyUnit = Instantiate(enemy, new Vector3(i * 2.0f + 4.0f, -(1.0f), 0), Quaternion.identity) as GameObject;
			enemyUnitList[i] = enemyUnit.GetComponent<DefaultEnemyUnit>();
		//GameObject arrow = Instantiate(projectile, transform.position + transform.TransformDirection(new Vector3(1,0,0)), transform.rotation) as GameObject;
		}
	}

	void Update(){
		for (int i = 0; i < 3; i++) {
			if (!(playerUnitList[i] as DefaultPlayerUnit).IsDead()){
				if (!enemyUnitList[0].IsDead()){
					playerUnitList[i].Attack(enemyUnitList[0]);
				}
				else if (!enemyUnitList[1].IsDead()){
					playerUnitList[i].Attack (enemyUnitList[1]);
				}
				else if (!enemyUnitList[2].IsDead()){
					playerUnitList[i].Attack (enemyUnitList[2]);
				}
			}
			if (!enemyUnitList[i].IsDead()){
				if (!playerUnitList[0].IsDead()){
					enemyUnitList[i].Attack(playerUnitList[0]);
				}
				else if (!playerUnitList[1].IsDead()){
					enemyUnitList[i].Attack (playerUnitList[1]);
				}
				else if (!playerUnitList[2].IsDead()){
					enemyUnitList[i].Attack (playerUnitList[2]);
				}
			}
		}
	}

	public void PlayerInput(int mode){
		for (int i = 0; i < 3; i ++)
		{
			if (mode == 0){
				playerUnitList[i].NeutralMode();
			}
			else if (mode == 1){
				playerUnitList[i].OffensiveMode();
			}
			else if (mode == -1){
				playerUnitList[i].DefensiveMode();
			}
		}
	}

	void OnGUI () {
		SetHPContainerStyle();
		for (int i = 0; i < 3; i ++){
			if (!playerUnitList[i].IsDead()){
				GUI.Box(new Rect(10, Screen.height-80+i*20, 200 , 10), "", currentStyle);
			}
			if (!enemyUnitList[i].IsDead()){
				GUI.Box(new Rect(Screen.width - 210, Screen.height-80+i*20, 200 , 10), "", currentStyle);
			}
		}
		SetHPFillStyle();
		for (int i = 0; i < 3; i ++){
			if (!playerUnitList[i].IsDead()){
				GUI.Box(new Rect(10, Screen.height-80+i*20, (float)playerUnitList[i].GetCurrentHealth()/playerUnitList[i].GetMaxHealth() * 200 , 10), "", currentStyle);
			}
			if (!enemyUnitList[i].IsDead()){
				float barLength = (float)enemyUnitList[i].GetCurrentHealth()/enemyUnitList[i].GetMaxHealth() * 200;
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
