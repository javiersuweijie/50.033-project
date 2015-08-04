using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class DataController : MonoBehaviour {

	public static DataController dataController;

	public List<UnitInfo> unitInfoList;

	public List<string> itemInfoList;

	public int[] activeUnitsIndex;

	public int openDungeons;

	protected bool loaded = false;

	protected bool saved = true;

	public float[] hpRemaining;

	public int stamRemaining;

	public bool lastStage;

	public int stage;
	public int dungeon;

	public Vector2 pressedStage;

	public ArrayList dungeonMapNodes;
	public ArrayList dungeonMapTrails;
	
	//public List<Item> itemDropList;

	//public int goldDrop;

	void Awake () {
		if (dataController == null){
			DontDestroyOnLoad(gameObject);
			dataController = this;
		}
		else if (dataController != this){
			Destroy(gameObject);
		}
	}

	public void Save(){
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/saveinfo.dat");

		PlayerData data = new PlayerData();
		data.unitsCount = unitInfoList.Count;
		data.activeUnits = activeUnitsIndex;
		data.unitList = new string[data.unitsCount];
		data.expList = new int[data.unitsCount];
		data.defSkillList = new string[data.unitsCount];
		data.offSkillList = new string[data.unitsCount];
		data.equipList = new int[data.unitsCount];
		data.itemList = itemInfoList.ToArray();

		for (int i = 0; i < data.unitsCount; i++){
			data.unitList[i] = unitInfoList[i].name;
			data.expList[i] = unitInfoList[i].exp;
			data.defSkillList[i] = unitInfoList[i].defSkill;
			data.offSkillList[i] = unitInfoList[i].offSkill;
			data.equipList[i] = unitInfoList[i].equip;
		}

		data.highestDungeonLevel = openDungeons;

		bf.Serialize(file, data);
		file.Close();
		saved = true;
	}
	
	public void Load(){
		Debug.Log("Data is stored in " + Application.persistentDataPath);
		if (File.Exists(Application.persistentDataPath + "/saveinfo.dat")){
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.OpenRead(Application.persistentDataPath + "/saveinfo.dat");
			PlayerData data = (PlayerData)bf.Deserialize(file);
			file.Close();

			unitInfoList = new List<UnitInfo>();

			for (int i = 0; i < data.unitsCount; i++){
				UnitInfo currentUnit = new UnitInfo();
				currentUnit.name = data.unitList[i];
				currentUnit.exp = data.expList[i];
				currentUnit.defSkill = data.defSkillList[i];
				currentUnit.offSkill = data.offSkillList[i];
				currentUnit.equip = data.equipList[i];
				unitInfoList.Add(currentUnit);
			}
			itemInfoList = new List<string>(data.itemList);
			activeUnitsIndex = data.activeUnits;

			openDungeons = data.highestDungeonLevel;

		}
		else{
			unitInfoList = new List<UnitInfo>();
			activeUnitsIndex = new int[3];
			activeUnitsIndex[0] = -1;
			activeUnitsIndex[1] = -1;
			activeUnitsIndex[2] = -1;
			
			openDungeons = 0;

			//For Testing Purposes
			unitInfoList = new List<UnitInfo>();
			activeUnitsIndex = new int[3];
			for (int i = 0; i < 3; i++){
				UnitInfo unitInfo = new UnitInfo();
				if (i == 0){
					unitInfo.name = "Paladin";
					unitInfo.defSkill = "HolyBarrier";
					unitInfo.offSkill = "LightningSlash";
					unitInfo.equip = 0;
				}
				else if (i == 1){
					unitInfo.name = "Cleric";
					unitInfo.defSkill = "";
					unitInfo.offSkill = "";
					unitInfo.equip = 1;
				}
				else {
					unitInfo.name = "Gunner";
					unitInfo.defSkill = "";
					unitInfo.offSkill = "GrenadeBarrage";
					unitInfo.equip = 2;
				}
				unitInfo.exp = 0;
				unitInfoList.Add(unitInfo);
				activeUnitsIndex[i] = i;
			}

			UnitInfo new_unit = new UnitInfo();
			new_unit.name = "Paladin";
			new_unit.defSkill = "GrenadeBarrage";
			new_unit.offSkill = "LightningSlash";
			unitInfoList.Add(new_unit);
			itemInfoList = new List<string>{"Hammer","Staff","Sword","RareSword"};
			//End Testing
		}
		Save();
		this.loaded = true;
	}

	public bool IsLoaded(){
		return loaded;
	}
	
	public bool IsSaved(){
		return saved;
	}
	
	public void Modified(){
		saved = false;
	}

	public List<PlayerUnit> getActiveUnits() {
		List<PlayerUnit> units = new List<PlayerUnit>();
		for (int i = 0; i < 3; i++){
			UnitInfo unitInfo = unitInfoList[dataController.activeUnitsIndex[i]];
				
			PlayerUnit pu = MakePlayerUnit(unitInfo);
			if (pu != null){
				pu.SetHPtoFraction(dataController.hpRemaining[i]);
			}
			units.Add(pu);
		}
		return units;
	}

	public List<PlayerUnit> getAllUnits() {
		List<PlayerUnit> units = new List<PlayerUnit>();
		foreach (UnitInfo unitInfo in unitInfoList) {
			PlayerUnit pu = MakePlayerUnit(unitInfo);
			units.Add(pu);
		}
		return units;
	}

	public List<Weapon> getAllWeapons() {
		List<Weapon> weapons = new List<Weapon>();
		foreach (string weapon_name in itemInfoList) {
			LameWeapons weapon = new LameWeapons(weapon_name);
			weapons.Add(weapon);
		}
		return weapons;
	}

	private PlayerUnit MakePlayerUnit(UnitInfo uf) {
		PlayerUnit pu = null;
		if (uf.name == "Paladin"){
			pu = new Paladin(uf.exp);
		} 
		else if (uf.name == "Cleric")	{
			pu = new Cleric(uf.exp);
		}
		else if (uf.name == "Gunner")	{
			pu = new Gunner(uf.exp);
		}
		if (pu != null){
			pu.SetDefSkill(uf.defSkill);
			pu.SetOffSkill(uf.offSkill);
			if (uf.equip != -1)
			pu.SetWeapon(itemInfoList[uf.equip]);
		}
		return pu;
	}
}

public class UnitInfo
{
	public string name;
	public int exp;
	public string defSkill;
	public string offSkill;
	public int equip = -1;
}

[Serializable]
class PlayerData
{
	public int unitsCount;
	public int[] activeUnits;
	public int[] equipList;
	public string[] unitList;
	public int[] expList;
	public string[] defSkillList;
	public string[] offSkillList;
	public string[] itemList;

	public int highestDungeonLevel;
}
