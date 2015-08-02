using UnityEngine;
using System.Collections.Generic;


public class PartyController {

	private List<UnitController> unitList;

	// Use this for initialization
	public PartyController () {
		unitList = new List<UnitController>();
	}

	public void AddUnit(UnitController unit){
		unitList.Add(unit);
	}

	public UnitController GetFrontTarget () {
		foreach (UnitController unit in unitList) {
			if (!unit.unit.IsDead()){
				return unit;
			}
		}
		return null;
	}

	public UnitController GetBackTarget () {
		for (int i = unitList.Count - 1; i >= 0; i--) {
			if (!unitList[i].unit.IsDead()){
				return unitList[i];
			}
		}
		return null;
	}
	
	public List<UnitController> GetAllTargets () {
		List<UnitController> aliveUnitList = new List<UnitController>();
		foreach (UnitController unit in unitList) {
			if (!unit.unit.IsDead()){
				aliveUnitList.Add(unit);
			}
		}
		return aliveUnitList;
	}
	
	public UnitController GetRandomTarget () {
		List<UnitController> aliveUnitList = new List<UnitController>();
		foreach (UnitController unit in unitList) {
			if (!unit.unit.IsDead()){
				aliveUnitList.Add(unit);
			}
		}
		if (aliveUnitList.Count == 0) return null;
		int randNo = Random.Range(0, aliveUnitList.Count);
		return aliveUnitList[randNo];
	}
	
	public UnitController GetUnit (int i) {
		if (i < unitList.Count){
			return unitList[i];
		}
		return null;
	}

	public UnitController GetMostHurtUnit(){
		float minHP = 1.01f;
		UnitController mostHurtUnit = null;
		foreach (UnitController unit in unitList) {
			if (!unit.unit.IsDead()){
				if (unit.unit.GetFractionalHealth() < minHP) 
				{
					minHP = unit.unit.GetFractionalHealth();
					mostHurtUnit = unit;
				}
			}
		}
		return mostHurtUnit;
	}

	public void ChangeModeTo (int mode){
		for (int i = 0; i < 3; i ++)
		{
			if (mode == 0){
				unitList[i].unit.NeutralMode();
			}
			else if (mode == 1){
				unitList[i].unit.OffensiveMode();
			}
			else if (mode == -1){
				unitList[i].unit.DefensiveMode();
			}
		}
	}

	public bool IsDead (int position){
		return unitList[position].unit.IsDead();
	}

	public bool AllDead () {
		bool allDead = true;
		foreach (UnitController unit in unitList){
			if (!unit.unit.IsDead()){
				return false;
			}
		}
		return true;
	}
}
