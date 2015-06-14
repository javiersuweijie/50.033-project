using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;

public class LevelParser : MonoBehaviour {

	public GameObject button_prefab;
	public GameObject arrow_prefab;
	public GameObject level_info_prefab;

	public TextAsset level_data;
	public int level_width = 228;
	public int button_width = 96;
	private string[] line_delimiters = {"\r\n","\n"};
	private char[] token_delimiters = {'='};
	private char[] edge_delimiters = {','};
	int levels,difficulty,current_level;
	string name;
	Level[] level_tree;
	List<string> map_layout = new List<string>();

	// Use this for initialization
	void Start () {
		Transform panel = transform.GetComponentsInChildren<Transform>().FirstOrDefault(t => t.name == "MapPanel");
		string raw_data = level_data.ToString();
		string[] lines = raw_data.Split(line_delimiters,System.StringSplitOptions.RemoveEmptyEntries);
		foreach (string line in lines) {
			parse(line);
		}
		createLayout(panel);
	}

//	void createButtons(Level level, Transform parent) {
//		Queue<Level> queue = new Queue<Level>();
//		queue.Enqueue(level);
//		List<int> nodes_per_level = new List<int> {1};
//		while (queue.Count != 0) {
//			Level current_level = queue.Dequeue();
//			int height = current_level.height;
//			GameObject level_button = Instantiate(button, new Vector2(0,-96+96*height), Quaternion.Euler(Vector3.zero)) as GameObject;
//			level_button.transform.SetParent(parent, false);
//			foreach (int index in current_level.getChildrenIndex()) {
//				GameObject level_arrow = Instantiate(arrow, new Vector2(0,-96+96*height), Quaternion.Euler(Vector3.zero)) as GameObject;
//				level_arrow.transform.SetParent(parent, false);
//				level_tree[index].height = current_level.height+1;
//				queue.Enqueue(level_tree[index]);
//			}
//			if (nodes_per_level.Count <= height+1)
//				nodes_per_level.Add(current_level.getChildrenIndex().Count);
//			else
//				nodes_per_level[height+1] += current_level.getChildrenIndex().Count;
//		}
//		foreach (int i in nodes_per_level) Debug.Log(i);
//	}

	void createLayout(Transform parent) {
		int height = map_layout.Count;
		int width = map_layout[0].Count();
		Rect parent_rect = parent.gameObject.GetComponent<RectTransform>().rect;
		float grid_width = parent_rect.width / width;
		float grid_height = parent_rect.height / height;
		for (int col = 0; col < map_layout.Count; col++) {
			for (int row = 0; row < map_layout[0].Count(); row++) {
				char grid = map_layout[col][row];
				if (grid == 'x' || grid == ' ' || grid == '\0') continue;
				else {
					Level level = level_tree[(int)(grid-'0')];
					GameObject level_button = Instantiate(button_prefab) as GameObject;
					level.button = level_button;
					level_button.transform.position = new Vector3(row * grid_width, (height-col) * grid_height,0);
					level_button.transform.SetParent(parent,false);
					level_button.GetComponentInChildren<Text>().text = "Level "+level.level_id;
					level_button.GetComponent<Button>().onClick.AddListener(()=>{printTest(level);});
				}
			}
		}
//		foreach (Level current_level in level_tree) {
//			foreach (int child_index in current_level.getChildrenIndex()) {
//				GameObject level_arrow = Instantiate(arrow) as GameObject;
//				level_arrow.transform.SetParent(parent, false);
//			}
//		}
	}

	void printTest(Level level) {
		Debug.Log("clicked on level: "+level.level_id);
		GameObject level_info = Instantiate(level_info_prefab) as GameObject;
		Transform parent = transform.GetComponentsInChildren<Transform>().FirstOrDefault(t => t.name == "Panel");
		Debug.Log(parent);
		level_info.transform.SetParent(parent,false);
		level_info.transform.FindChild("LevelTitle").GetComponent<Text>().text = "Level: " + level.level_id;
		level_info.transform.FindChild("CloseButton").GetComponent<Button>().onClick.AddListener(
			()=>{Destroy(level_info);}
		);
			
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
			level_tree[level] = new Level(level);
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
		case "map":
			map_layout.Add(tokens[1]);
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
	public int height = 0;
	public Vector3 button_position;
	public GameObject button;

	public Level(int level_id) {
		this.level_id = level_id;
	}

	public void addChildren(int index) {
		children.Add(index);
	}

	public ArrayList getChildrenIndex() {
		return children;
	}

}