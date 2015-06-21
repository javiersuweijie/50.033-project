using UnityEngine;
using System.Collections.Generic;

public class EnemyPartyController: MonoBehaviour {
	
	private List<EnemyUnit> enemyUnitList;
	
	// Use this for initialization
	public EnemyPartyController () {
		enemyUnitList = new List<EnemyUnit>();
	}

	public void AddEnemyUnit(EnemyUnit unit){
		enemyUnitList.Add(unit);
	}

	// Update is called once per frame
	public EnemyUnit GetFrontTarget () {
		foreach (EnemyUnit unit in enemyUnitList) {
			if (!unit.IsDead()){
				return unit;
			}
		}
		return null;
	}
	
	public EnemyUnit GetBackTarget () {
		for (int i = enemyUnitList.Count - 1; i >= 0; i--) {
			if (!enemyUnitList[i].IsDead()){
				return enemyUnitList[i];
			}
		}
		return null;
	}
	
	public List<EnemyUnit> GetAllTargets () {
		List<EnemyUnit> aliveUnitList = new List<EnemyUnit>();
		foreach (EnemyUnit unit in enemyUnitList) {
			if (!unit.IsDead()){
				aliveUnitList.Add(unit);
			}
		}
		return aliveUnitList;
	}
	
	public EnemyUnit GetRandomTarget () {
		List<EnemyUnit> aliveUnitList = new List<EnemyUnit>();
		foreach (EnemyUnit unit in enemyUnitList) {
			if (!unit.IsDead()){
				aliveUnitList.Add(unit);
			}
		}
		int randNo = Random.Range(0, aliveUnitList.Count);
		return aliveUnitList[randNo];
	}
	
	public EnemyUnit GetEnemyUnit (int i) {
		return enemyUnitList[i];
	}
	
	public bool IsDead (int position){
		return enemyUnitList[position].IsDead();
	}
	
	public bool AllDead () {
		return enemyUnitList[0].IsDead() && enemyUnitList[1].IsDead() && enemyUnitList[2].IsDead();
	}
}