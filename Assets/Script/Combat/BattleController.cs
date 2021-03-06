using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class BattleController : MonoBehaviour{
	public GameObject player = null;
	public GameObject enemy = null;
	public GameObject victorySplashObj = null;
	public GameObject defeatSplashObj = null;
	private GameObject[] playerObjects = new GameObject[3];
	private List<GameObject> enemyObjects = new List<GameObject>();
	private PartyController playerPartyController = new PartyController();
	private PartyController enemyPartyController = new PartyController();
	//private CombatGraphicalFunction cgf;
	private GameObject victorySplash = null;
	private GameObject defeatSplash = null;
	private DataController dataController;
	private AudioSource audios;
	public GameObject stambarobj = null;
	private StaminaBar stambar = null;
	//private GameObject[] playerhpbars = new GameObject[3];
	//private GameObject[] enemyhpbars = new GameObject[3];

	private AudioClip BGMWin;
	private AudioClip BGMBoss;

	private SkillAnimController skillAnimController;

	private bool win, lose, expAdded;
	public bool fighting;

	void Awake(){
		dataController = GameObject.FindWithTag("Data").GetComponent<DataController>();
		
		if (!dataController.IsLoaded()){
			Application.LoadLevel("Main");
		}
	}

	void Start(){

		audios =  Camera.main.GetComponent<AudioSource> ();

		if (dataController.lastStage) {
			BGMBoss = (AudioClip) Resources.Load ("BGMBoss",typeof(AudioClip));
			audios.clip = BGMBoss;
		}

		audios.Play();

		BGMWin = (AudioClip)Resources.Load ("BGMWin", typeof (AudioClip));
		skillAnimController = gameObject.GetComponent<SkillAnimController> ();
		stambar = (Instantiate (stambarobj, new Vector3(0, -4, -2), Quaternion.identity) as GameObject).GetComponent<StaminaBar>();
		stambar.Init(1000, dataController.stamRemaining);

		List<PlayerUnit> active_units = dataController.getActiveUnits();
		for (int i = 0; i < 3; i++) {
			playerObjects[i] = Instantiate(player, new Vector3(-(i * 2.0f + 4.0f), -(2.2f), 0), Quaternion.identity) as GameObject;
			UnitController unit_controller = playerObjects[i].GetComponent<UnitController>();
			PlayerUnit pu = active_units[i];
			unit_controller.AttachUnit(pu);
			playerPartyController.AddUnit(unit_controller);
			unit_controller.InitializeSAC(skillAnimController);
			playerObjects[i].AddComponent<FloatingHealthBar>();

			//playerObjects[i].AddComponent<BuffManager>();
                                                                                                                                                                                                                                                                                                                                                   			//playerhpbars[i] = Instantiate(hpbarobj,new Vector3(-6,- (2 + i * 1),-2), Quaternion.identity) as GameObject;
		}
	
		List<Unit> enemy_units = EnemyGenerator.GenerateEnemies(dataController.dungeon, dataController.stage);
		Debug.Log (enemy_units.Count);
		for (int i = 0; i < enemy_units.Count; i++) {
			GameObject enemyObj = Instantiate(enemy, new Vector3(i * 2.0f + 6.0f - enemy_units.Count, -(2.2f), 0), Quaternion.identity) as GameObject;
			enemyObj.transform.localScale = enemy_units[i].scale * enemyObj.transform.localScale;
			enemyObjects.Add(enemyObj);
			UnitController enemy_controller = enemyObjects[i].GetComponent<UnitController>();
			enemy_controller.AttachUnit(enemy_units[i]);
			enemyPartyController.AddUnit(enemy_controller);
		}
		//cgf = new CombatGraphicalFunction();
		fighting = false;
		win = false;
		lose = false;
		expAdded = false;
		StartCoroutine(startFight());
		//Debug.Log(enemyPartyController.GetAllTargets().Count);

	}

	IEnumerator startFight()
	{
		yield return new WaitForSeconds(1.0f);
		fighting = true;
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
			}

			for (int i = 0; i < enemyPartyController.Count(); i++) {
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
			for (int i = 0; i < enemyPartyController.Count(); i ++){
				if (enemyPartyController.IsDead(i)){
					totalExp += enemyPartyController.GetUnit(i).GetUnit().experience;
				}
			}
			if (!expAdded){
				for (int i = 0; i < 3; i ++){
					playerPartyController.GetUnit(i).GetUnit().experience += totalExp;
					dataController.unitInfoList[dataController.activeUnitsIndex[i]].exp += totalExp;	
				}
				expAdded = true;
			}
			if (victorySplash == null){
				victorySplash = Instantiate (victorySplashObj) as GameObject;
				victorySplash.GetComponent<VictorySplash>().ShowWin(itemsList, playerPartyController, totalExp, dataController);
			}
			//cgf.ShowWin(itemsList, playerPartyController, totalExp, dataController);
		}
		if (lose){
			if (defeatSplash == null){
				defeatSplash = Instantiate (defeatSplashObj) as GameObject;
				defeatSplash.GetComponent<DefeatSplash>().ShowLose(dataController);
			}
		}
	}

	public void ChangePlayerModeTo(int mode) {
		playerPartyController.ChangeModeTo(mode);
	}

	private void ShowWinScreen(){
		//Debug.Log ("Win!");

		audios.clip = BGMWin;
		audios.Play ();
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

	public void drainStam(int val){
		if (!win && !lose){
			stambar.UseStamina (val);
		}
	}

	public void incStam(int val){
		if (!win && !lose){
			stambar.RecoverStamina (val);
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
