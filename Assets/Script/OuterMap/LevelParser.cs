using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class LevelParser : MonoBehaviour {

	public GameObject button;
	public TextAsset level_data;
	private string[] line_delimiters = {"\r\n","\n"};
	private char[] token_delimiters = {'='};
	private char[] edge_delimiters = {','};
	int levels,difficulty,current_level;
	string name;
	Level[] level_tree;

	// Use this for initialization
	void Start () {
		Transform panel = transform.Find("Panel");
		string raw_data = level_data.ToString();
		string[] lines = raw_data.Split(line_delimiters,System.StringSplitOptions.RemoveEmptyEntries);
		foreach (string line in lines) {
			parse(line);
		}
		createButtons(level_tree[0],panel);
	}

	void createButtons(Level level, Transform parent) {
		Queue<Level> queue = new Queue<Level>();
		queue.Enqueue(level);
		int height = 0;
		while (queue.Count != 0) {
			Level current_level = queue.Dequeue();
			GameObject level_button = Instantiate(button, new Vector2(0,-96+96*height), Quaternion.Euler(Vector3.zero)) as GameObject;
			level_button.transform.SetParent(parent, false);
			foreach (int index in current_level.getChildrenIndex()) {
				queue.Enqueue(level_tree[index]);
			}
			height++;
		}
	}

	void parse(string line) {
		string[] tokens = line.Split(token_delimiters);
		switch(tokens[0]) {
		case "levels":
			int levels = int.Parse(tokens[1]);
			level_tree = new Level[levels];
			break;
		case "name":
			name = tokens[1];
			break;
		case "difficulty":
			difficulty = int.Parse(tokens[1]);
			break;
		case "level":
			int level = int.Parse(tokens[1]);
			current_level = level;
			level_tree[level] = new Level();
			break;
		case "waves":
			level_tree[current_level].waves = int.Parse(tokens[1]);
			break;
		case "edge":
			string[] split_index = tokens[1].Split(edge_delimiters);
			int[] node_index = new int[split_index.Length];
			int i = 0;
			foreach (string s in split_index) node_index[i++] = int.Parse(s);
			level_tree[node_index[0]].addChildren(node_index[1]);
			break;
		default:
			Debug.Log("Parse error: unknown token " + tokens[0]);
			break;
		}
	}

}

public class Level {
	public int level_id, waves;
	ArrayList children = new ArrayList();

	public void addChildren(int index) {
		children.Add(index);
	}

	public ArrayList getChildrenIndex() {
		return children;
	}

}