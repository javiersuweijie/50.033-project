using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class DataController : MonoBehaviour {

	public static DataController dataController;

	public List<UnitInfo> unitInfoList;

	//public List<ItemInfo> itemInfoList;

	public int[] activeUnitsIndex;

	public int openDungeons;

	protected bool loaded = false;

	protected bool saved = true;

	public float[] hpRemaining;

	public int stamRemaining;

	public bool lastStage;
	
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
		data.equipList = new string[data.unitsCount];

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
				}
				else if (i == 1){
					unitInfo.name = "Cleric";
					unitInfo.defSkill = "";
					unitInfo.offSkill = "";
				}
				else{
					unitInfo.name = "Gunner";
					unitInfo.defSkill = "";
					unitInfo.offSkill = "GrenadeBarrage";
				}
				unitInfo.exp = 0;
				unitInfo.equip = "";
				unitInfoList.Add(unitInfo);
				activeUnitsIndex[i] = i;
			}
			//End Testing
		}

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
}

public class UnitInfo
{
	public string name;
	public int exp;
	public string defSkill;
	public string offSkill;
	public string equip;
}

[Serializable]
class PlayerData
{
	public int unitsCount;
	public int[] activeUnits;
	public string[] unitList;
	public int[] expList;
	public string[] defSkillList;
	public string[] offSkillList;
	public string[] equipList;

	public int highestDungeonLevel;
}
