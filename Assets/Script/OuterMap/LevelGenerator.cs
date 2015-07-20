using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class LevelGenerator : MonoBehaviour {

	public int level_number = 0;
	public GameObject map_panel;
	private int enemy_max_level = 6;
	Level current_level;
	Dictionary<int,string> animals = new Dictionary<int,string>();

	public void populateDictionary(){
		animals.Add(1,"cat");
		animals.Add(2,"dog");
		animals.Add(3,"wolf");
		animals.Add(4,"rhino");
		animals.Add(5,"lion");
		animals.Add(6,"dragon");
	}

	public float fork_1 = 0.5f;
	public float fork_2 = 0.3f;

	public float treasure_prob = 0.5f;

	public Level[] nextLevels() {
		float fork_value = UnityEngine.Random.Range(0,1);
		int fork_num = 0;
		if (fork_value <= fork_1) fork_num = 1;
		else if (fork_value <= (fork_1+fork_2)) fork_num = 2;
		else fork_num = 3;

		Level[] levels = new Level[3];

		for (int i=0;i<fork_num;i++) {
			Level level_node = new Level(level_number);
			level_node.parent = current_level;
			float treasure_value = UnityEngine.Random.Range(0,1);
			if (treasure_value < treasure_prob) level_node.treasure = true;
			else {
				level_node.enemy = allocate(level_number);
			}
			levels[i] = level_node;
		}
		return levels;
	}

	private string[] allocate(int level) {
		List<string> list = new List<string>();
		while (level!=0) {
			int enemy_level = (int) UnityEngine.Random.Range(1,Mathf.Min(level,enemy_max_level));
			list.Add(animals[enemy_level]);
		}
		return list.ToArray();
	}

}


public class Level {
	public string[] enemy;
	public bool treasure = false;
	public int height = 0;
	public Vector3 button_position;
	public GameObject button;
	public Level parent;
	
	public Level(int level) {
		this.height = level;
	}

	
}