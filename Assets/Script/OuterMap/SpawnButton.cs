using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SpawnButton : MonoBehaviour {

	// Use this for initialization
	public RectTransform parent;
	public GameObject buttonPrefab;
	public GameObject trailPrefab;
	public Sprite GraySprite;
	public Sprite GreenSprite;
	public Sprite BlueSprite;
	private ArrayList nodes = new ArrayList ();
	private ArrayList trails = new ArrayList ();
	private int radius = 8;
	
	private DataController dataController;
	
	
	void Awake(){
		dataController = GameObject.FindWithTag("Data").GetComponent<DataController>();
		
		if (!dataController.IsLoaded()){
			Application.LoadLevel("Main");
		}
	}
	void Start () {
		if (dataController.stage == 0){
			Vector2 start = new Vector2 (-180, 0);
			Vector2 end = new Vector2 (180, 0);
			Node startNode = new Node (start);
			Node endNode = new Node (end);
			startNode.available = true;
			startNode.level = 0;
			endNode.level = 4;
			nodes.Add (startNode);
			nodes.Add (endNode);
			Queue level1 = GenerateLevels (-90);
			Queue level2 = GenerateLevels (0);
			Queue level3 = GenerateLevels (90);
			GenerateTrailStart (startNode, level1);
			GenerateTrailBetween (level1, level2);
			GenerateTrailBetween (level2, level3);
			GenerateTrailEnd (endNode,level3);
			dataController.dungeonMapNodes = nodes;
			dataController.dungeonMapTrails = trails;
		}
		else{
			nodes = dataController.dungeonMapNodes;
			trails = dataController.dungeonMapTrails;
			UpdateMapProgress();
			dataController.dungeonMapNodes = nodes;
			dataController.dungeonMapTrails = trails;
		}
		Populate ();
	}
	public void GenerateTrailStart(Node a, Queue bN)
	{
		Queue b = new Queue (bN);
		while (b.Count!=0)
		{
			Node temp = (Node)b.Dequeue();
			GenerateTrail(a.me,temp.me);
			a.children.Add (temp.me);
		}
		nodes.Add (a);
	}
	public void GenerateTrailEnd(Node a, Queue bN)
	{
		Queue b = new Queue (bN);
		while (b.Count!=0) {
			Node temp = (Node)b.Dequeue ();
			GenerateTrail (temp.me, a.me);
			temp.children.Add (a.me);
			nodes.Add(temp);
		}
	}
	public void GenerateTrailBetween(Queue leftN, Queue rightN)
	{
		Queue left = new Queue (leftN);
		Queue right = new Queue(rightN);
		if (left.Count == right.Count) {
			while (left.Count!=0) {
				Node l = (Node)left.Dequeue ();
				Node r = (Node)right.Dequeue ();
				GenerateTrail (l.me, r.me);
				l.children.Add (r.me);
				nodes.Add (l);
			}
		} 
		else if (left.Count > right.Count) {
			Node last = new Node(new Vector2(0,0));
			while (right.Count!=0) {
				Node l = (Node)left.Dequeue ();
				last = (Node)right.Dequeue ();
				GenerateTrail (l.me, last.me);
				l.children.Add(last.me);
				nodes.Add (l);
			}
			while (left.Count!=0){
				Node l = (Node)left.Dequeue();
				GenerateTrail(l.me,last.me);
				l.children.Add(last.me);
				nodes.Add (l);
			}
		} 
		else {
			Node last = new Node(new Vector2(0,0));
			while (left.Count!=0) {
				Node r = (Node)right.Dequeue ();
				last = (Node)left.Dequeue ();
				GenerateTrail (last.me,r.me);
				last.children.Add(r.me);
				nodes.Add (last);
			}
			while (right.Count!=0){
				Node r = (Node)right.Dequeue();
				GenerateTrail(last.me,r.me);
				last.children.Add(r.me);
				nodes.Add (last);
			}
		}
	}
	public Queue GenerateLevels (int level)
	{
		Queue levelNodes = new Queue ();
		int numberOfNodes = Random.Range (1, 4);
		int interval = 180 / numberOfNodes;
		for (int i = 0; i<numberOfNodes; i++)
		{
			Vector2 coords = new Vector2(level, Random.Range (90-i*interval,90-i*interval-interval+20));
			Node node = new Node(coords);
			node.level = (level + 180)/90;
			levelNodes.Enqueue (node);
		}
		return levelNodes;
	}
	public void GenerateTrail(Vector2 a, Vector2 b)
	{
		Queue pairs = new Queue ();
		ArrayList pair = new ArrayList ();
		pair.Add (a);
		pair.Add (b);
		pairs.Enqueue (pair);
		int count = 0;
		while (pairs.Count!=0) {
			ArrayList pair_t = (ArrayList)pairs.Dequeue();
			Vector2 first = (Vector2)pair_t[0];
			Vector2 second = (Vector2)pair_t[1];
			int first_x = (int) first.x;
			int first_y = (int) first.y;
			int second_x = (int) second.x;
			int second_y = (int) second.y;
			int third_x;
			int third_y;

			if (first_x > second_x)
			{
				third_x = (first_x - second_x)/2 + second_x;
			}
			else
			{
				third_x = (second_x - first_x)/2 + first_x;
			}

			if (first_y > second_y)
			{
				int val = (first_y - second_y)/2 + second_y;
				third_y = val;
			}
			else
			{
				int val = (second_y - first_y)/2 + first_y;
				third_y = val;
			}
			Vector2 third = new Vector2(third_x,third_y);
			ArrayList pair1 = new ArrayList();
			pair1.Add (third);
			pair1.Add (first);
			ArrayList pair2 = new ArrayList();
			pair2.Add(third);
			pair2.Add(second);
			pairs.Enqueue (pair1);
			pairs.Enqueue (pair2);
			trails.Add(third);
			count+=1;
			if (count==15)
			{
				break;
			}
		}
	}
	public void Populate()
	{
		foreach (Node node in nodes) 
		{
			GameObject instance = (GameObject)UnityEngine.Object.Instantiate(buttonPrefab);
			instance.transform.SetParent (parent, false);
			Vector2 button_position = node.me;
			instance.GetComponent<RectTransform>().anchoredPosition = button_position;
			if (node.visited){
				instance.GetComponent<Image>().sprite = BlueSprite;
			}
			else if(node.available){
				float x = node.me.x;
				float y = node.me.y;
				instance.GetComponent<Button>().onClick.AddListener(()=>{ToCombatScreen(new Vector2(x,y));});
				instance.GetComponent<Image>().sprite = GreenSprite;
			}
			else if(node.invalid){
				instance.GetComponent<Image>().sprite = GraySprite;
			}
		}
		foreach (Vector2 trail in trails) 
		{
			GameObject instance = (GameObject)UnityEngine.Object.Instantiate(trailPrefab);
			instance.transform.SetParent (parent, false);
			Vector2 button_position = trail;
			instance.GetComponent<RectTransform>().anchoredPosition = button_position;
		}
	}

	public void UpdateMapProgress(){
		ArrayList prevNodeChildren = new ArrayList();
		foreach (Node node in nodes){
			if (node.level == dataController.stage - 1)
			{
				//Debug.Log (node.me);
				//Debug.Log (dataController.pressedStage);
				if (node.me.x == dataController.pressedStage.x && node.me.y == dataController.pressedStage.y){
					node.visited = true;
					node.available = false;
					node.invalid = false;
					prevNodeChildren = node.children;
				}
				else{
					node.visited = false;
					node.available = false;
					node.invalid = true;
				}
			}
		}
		foreach (Vector2 v2 in prevNodeChildren){
			Debug.Log (v2);
		}
		foreach (Node node in nodes){
			if (node.level == dataController.stage)
			{
				foreach (Vector2 v in prevNodeChildren){
					if (node.me.x == v.x && node.me.y == v.y){
						node.visited = false;
						node.available = true;
						node.invalid = false;
					}
				}
			}
		}
	}

	private void ToMainScreen(){
		Application.LoadLevel("Main");
	}
	
	private void ToCombatScreen(Vector2 me){
		dataController.pressedStage = me;
		Application.LoadLevel("Combat");
	}
}

[System.Serializable]
class MapData
{
	public ArrayList nodes;
	public ArrayList trails;
}

public class Node
{
	public Vector2 me = new Vector2 ();
	public ArrayList children = new ArrayList();
	public bool visited = false;
	public bool invalid = false;
	public bool available = false;
	public int level;

	public Node(Vector2 meC)
	{
		me = meC;
	}
}

