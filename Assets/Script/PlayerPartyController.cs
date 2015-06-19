using UnityEngine;
using System.Collections.Generic;

public class PlayerPartyController: MonoBehaviour {

	private List<PlayerUnit> playerUnitList;

	// Use this for initialization
	public PlayerPartyController () {
		playerUnitList = new List<PlayerUnit>();
	}

	public void AddPlayerUnit(PlayerUnit unit){
		playerUnitList.Add(unit);
	}

	// Update is called once per frame
	public PlayerUnit GetFrontTarget () {
		foreach (PlayerUnit unit in playerUnitList) {
			if (!unit.IsDead()){
				return unit;
			}
		}
		return null;
	}

	public PlayerUnit GetBackTarget () {
		for (int i = playerUnitList.Count - 1; i >= 0; i--) {
			if (!playerUnitList[i].IsDead()){
				return playerUnitList[i];
			}
		}
		return null;
	}
	
	public List<PlayerUnit> GetAllTargets () {
		List<PlayerUnit> aliveUnitList = new List<PlayerUnit>();
		foreach (PlayerUnit unit in playerUnitList) {
			if (!unit.IsDead()){
				aliveUnitList.Add(unit);
			}
		}
		return aliveUnitList;
	}
	
	public PlayerUnit GetRandomTarget () {
		List<PlayerUnit> aliveUnitList = new List<PlayerUnit>();
		foreach (PlayerUnit unit in playerUnitList) {
			if (!unit.IsDead()){
				aliveUnitList.Add(unit);
			}
		}
		int randNo = Random.Range(0, aliveUnitList.Count);
		return aliveUnitList[randNo];
	}
	
	public PlayerUnit GetPlayerUnit (int i) {
		return playerUnitList[i];
	}

	public void ChangeModeTo (int mode){
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

	public bool IsDead (int position){
		return playerUnitList[position].IsDead();
	}

	public bool AllDead () {
		return playerUnitList[0].IsDead() && playerUnitList[1].IsDead() && playerUnitList[2].IsDead();
	}
}
