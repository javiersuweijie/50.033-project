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
	private DataController dataController;

	public GameObject stambarobj = null;
	private StaminaBar stambar = null;
	//private GameObject[] playerhpbars = new GameObject[3];
	//private GameObject[] enemyhpbars = new GameObject[3];

	private SkillAnimController skillAnimController;

	private bool fighting, win, lose;


	void Start(){
		dataController = GameObject.FindWithTag("Data").GetComponent<DataController>();

		if (!dataController.IsLoaded()){
			Application.LoadLevel("Main");
		}

		skillAnimController = gameObject.GetComponent<SkillAnimController> ();
		stambar = (Instantiate (stambarobj, new Vector3(0, -4, -2), Quaternion.identity) as GameObject).GetComponent<StaminaBar>();

		stambar.Init(1000, dataController.stamRemaining);

		for (int i = 0; i < 3; i++){
//			if (i == 0){
//				playerObjects[i].AddComponent<Paladin>();
//			} else if (i == 1)
//			{
//				playerObjects[i].AddComponent<Cleric>();
//			}else
//			{
//				playerObjects[i].AddComponent<Gunner>();
//			}
			if (dataController.activeUnitsIndex[i] != -1){
				UnitInfo unitInfo = dataController.unitInfoList[dataController.activeUnitsIndex[i]];
				playerObjects[i] = Instantiate(player, new Vector3(-(i * 2.0f + 4.0f), -(2.2f), 0), Quaternion.identity) as GameObject;
				UnitController unit_controller = playerObjects[i].GetComponent<UnitController>();

				//TEMPORARY
				PlayerUnit pu = null;

				if (unitInfo.name == "Paladin"){
					pu = new Paladin();
				} 
				else if (unitInfo.name == "Cleric")	{
					pu = new Cleric();
				}
				else if (unitInfo.name == "Gunner")	{
					pu = new Gunner();
				}
				if (pu != null){
					pu.SetDefSkill(unitInfo.defSkill);
					pu.SetOffSkill(unitInfo.offSkill);
					pu.SetHPtoFraction(dataController.hpRemaining[i]);
				}

				unit_controller.AttachUnit(pu);

				//END

				playerPartyController.AddUnit(unit_controller);
				unit_controller.InitializeSAC(skillAnimController);
				playerObjects[i].AddComponent<FloatingHealthBar>();
			}
			//playerObjects[i].AddComponent<BuffManager>();
                                                                                                                 			//playerhpbars[i] = Instantiate(hpbarobj,new Vector3(-6,- (2 + i * 1),-2), Quaternion.identity) as GameObject;
			enemyObjects[i] = Instantiate(enemy, new Vector3(i * 2.0f + 4.0f, -(2.2f), 0), Quaternion.identity) as GameObject;
			UnitController enemy_controller = enemyObjects[i].GetComponent<UnitController>();
			enemy_controller.AttachUnit(new Poring());
			enemyPartyController.AddUnit(enemy_controller);
		}
		cgf = new CombatGraphicalFunction();
		fighting = true;
		win = false;
		lose = false;
		//Debug.Log(enemyPartyController.GetAllTargets().Count);
	}

	void Update(){
		if (fighting){
			for (int i = 0; i < 3; i++) {
				if (!playerPartyController.IsDead(i) && !enemyPartyController.AllDead()){
					//Attack is handled by the units class attack function
					UnitController player = playerPartyController.GetUnit(i);
					if (player != null){
						player.Attack(playerPartyController, enemyPartyController, stambar);
					}
				}
				else if (playerPartyController.IsDead(i) && playerObjects[i] != null){
					HandleUnitDestruction(playerObjects[i]);
					playerObjects[i] = null;
				}
				else if (enemyPartyController.AllDead()){
					ShowWinScreen();
				}

				if (!enemyPartyController.IsDead(i) && !playerPartyController.AllDead()){
					//Attack is handled by the units class attack function
					UnitController enemy = enemyPartyController.GetUnit(i);
					if (enemy != null){
						enemy.Attack(enemyPartyController, playerPartyController, null);
					}
				}
				else if (enemyPartyController.IsDead(i) && enemyObjects[i] != null){
					HandleUnitDestruction(enemyObjects[i]);
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
		//for (int i = 0; i < 3; i++){
		//	UpdateHealth(playerhpbars[i], playerPartyController.GetUnit(i));
		//	UpdateHealth(enemyhpbars[i], enemyPartyController.GetUnit(i));
		//}
	}

	void OnGUI () {

		if (win){
			for (int i = 0; i < 3; i++){
				dataController.hpRemaining[i] = playerPartyController.GetUnit(i).GetUnit().GetFractionalHealth();
			}
			dataController.stamRemaining = stambar.GetCurrentStamina();

			int[] itemsList = new int[3];

			int totalExp = 0;
			for (int i = 0; i < 3; i ++){
				if (enemyPartyController.IsDead(i)){
					totalExp += enemyPartyController.GetUnit(i).GetUnit().experience;
				}
			}

			cgf.ShowWin(dataController.lastStage, itemsList, playerPartyController, totalExp);
		}

		if (lose){
			cgf.ShowLose();
		}
	}

	public void ChangePlayerModeTo(int mode) {
		playerPartyController.ChangeModeTo(mode);
	}

	private void ShowWinScreen(){
		//Debug.Log ("Win!");
		win = true;
		fighting = false;
	}
	
	private void ShowLoseScreen(){
		//Debug.Log ("Lose!");
		lose = true;
		fighting = false;
	}

	private void HandleUnitDestruction(GameObject unitobj){
		if (unitobj != null)
		{
			FloatingHealthBar hpbar = unitobj.GetComponent<FloatingHealthBar>();
			if (hpbar != null){
				hpbar.DestroyHPBar();
			}
			Destroy(unitobj);
		}
	}

	public void drainStam(){
		if (!win && !lose){
			stambar.UseStamina (1);
		}
	}

	public void incStam(){
		if (!win && !lose){
			stambar.RecoverStamina (1);
		}
	}

//	private void UpdateHealth(GameObject hpbar, Unit unit)
//	{
//		if (hpbar != null)
//		{
//			if (unit.IsDead()){
//				Destroy(hpbar);
//			}
//			else{
//				RectTransform hpbg = (RectTransform) hpbar.GetComponent<Transform>().GetChild(0).GetChild(0).GetChild(0);
//				RectTransform hpfill = (RectTransform) hpbg.GetChild(0);float barLength = hpfill.rect.width;
//				float maxXVal = hpbg.position.x;
//				float minXVal = maxXVal - barLength;
//				float tgtXVal = minXVal + unit.GetFractionalHealth() * barLength;
//				tgtXVal = Mathf.Clamp(tgtXVal, minXVal, maxXVal);
//				hpfill.position = new Vector3(tgtXVal, hpfill.position.y, hpfill.position.z);
//			}
//		}
//	}
}
