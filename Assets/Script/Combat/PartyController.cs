using UnityEngine;
using System.Collections.Generic;


public class PartyController {

	private List<Unit> unitList;

	// Use this for initialization
	public PartyController () {
		unitList = new List<Unit>();
	}

	public void AddUnit(Unit unit){
		unitList.Add(unit);
	}

	public Unit GetFrontTarget () {
		foreach (Unit unit in unitList) {
			if (!unit.IsDead()){
				return unit;
			}
		}
		return null;
	}

	public Unit GetBackTarget () {
		for (int i = unitList.Count - 1; i >= 0; i--) {
			if (!unitList[i].IsDead()){
				return unitList[i];
			}
		}
		return null;
	}
	
	public List<Unit> GetAllTargets () {
		List<Unit> aliveUnitList = new List<Unit>();
		foreach (Unit unit in unitList) {
			if (!unit.IsDead()){
				aliveUnitList.Add(unit);
			}
		}
		return aliveUnitList;
	}
	
	public Unit GetRandomTarget () {
		List<Unit> aliveUnitList = new List<Unit>();
		foreach (Unit unit in unitList) {
			if (!unit.IsDead()){
				aliveUnitList.Add(unit);
			}
		}
		if (aliveUnitList.Count == 0) return null;
		int randNo = Random.Range(0, aliveUnitList.Count);
		return aliveUnitList[randNo];
	}
	
	public Unit GetUnit (int i) {
		if (i < unitList.Count){
			return unitList[i];
		}
		return null;
	}

	public void ChangeModeTo (int mode){
		for (int i = 0; i < 3; i ++)
		{
			if (mode == 0){
				unitList[i].NeutralMode();
			}
			else if (mode == 1){
				unitList[i].OffensiveMode();
			}
			else if (mode == -1){
				unitList[i].DefensiveMode();
			}
		}
	}

	public bool IsDead (int position){
		return unitList[position].IsDead();
	}

	public bool AllDead () {
		bool allDead = true;
		foreach (Unit unit in unitList){
			if (!unit.IsDead()){
				return false;
			}
		}
		return true;
	}
}
