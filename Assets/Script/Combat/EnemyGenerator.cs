using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class EnemyGenerator {

	public static List<Unit> GenerateEnemies(int dungeon, int stage){
		int variation = (int) Random.Range(0,3);
		List<Unit> outputList = new List<Unit>();
		switch(dungeon){
		case 0:
			switch(stage){
			case 0:
				switch(variation){
				case 0:
					outputList.Add(new Poring());
					outputList.Add(new Poring());
					break;
				case 1:
					outputList.Add(new Poring());
					outputList.Add(new Poring());
					outputList.Add(new Poring());
					break;
				case 2:
					outputList.Add(new Poring());
					outputList.Add(new Poporing());
					break;
				default:
					break;
				}
				break;
			case 1:
				switch(variation){
				case 0:
					outputList.Add(new Poring());
					outputList.Add(new Poring());
					outputList.Add(new Poring());
					outputList.Add(new Poring());
					break;
				case 1:
					outputList.Add(new Poring());
					outputList.Add(new Poring());
					outputList.Add(new Poporing());
					outputList.Add(new Poring());
					break;
				case 2:
					outputList.Add(new Poring());
					outputList.Add(new Poporing());
					outputList.Add(new Miming());
					break;
				default:
					break;
				}
				break;
			case 2:
				switch(variation){
				case 0:
					outputList.Add(new Poporing());
					outputList.Add(new Poporing());
					outputList.Add(new Miming());
					outputList.Add(new Miming());
					break;
				case 1:
					outputList.Add(new Miming());
					outputList.Add(new RodaFrog());
					break;
				case 2:
					outputList.Add(new Poring());
					outputList.Add(new Poring());
					outputList.Add(new Menblatt());
					break;
				default:
					break;
				}
				break;
			case 3:
				switch(variation){
				case 0:
					outputList.Add(new Poring());
					outputList.Add(new Poring());
					outputList.Add(new Miming());
					outputList.Add(new Menblatt());
					break;
				case 1:
					outputList.Add(new Poring());
					outputList.Add(new Poring());
					outputList.Add(new Poring());
					outputList.Add(new RodaFrog());
					break;
				case 2:
					outputList.Add(new Menblatt());
					outputList.Add(new RodaFrog());
					break;
				default:
					break;
				}
				break;
			case 4:
				switch(variation){
				case 0:
				case 1:
				case 2:
					outputList.Add(new KingPoring());
					break;
				default:
					break;
				}
				break;
			default:
				break;
			}
			break;
		case 1:
			break;
		default:
			break;
		}
		return outputList;
	}
}
