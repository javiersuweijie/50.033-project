using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SpawnButton : MonoBehaviour {

	// Use this for initialization
	public RectTransform parent;
	public GameObject buttonPrefab;
	public GameObject trailPrefab;
	private ArrayList vectors = new ArrayList ();
	private ArrayList trails = new ArrayList ();
	private ArrayList node0 = new ArrayList ();
	private ArrayList node1 = new ArrayList ();
	private ArrayList node2 = new ArrayList ();
	private ArrayList node3 = new ArrayList ();
	private int radius = 8;
	void Start () {
		Vector2 start = new Vector2 (-180, 0);
		Vector2 end = new Vector2 (180, 0);
		vectors.Add (start);
		vectors.Add (end);
		Queue level1 = GenerateLevels (-90);
		Queue level2 = GenerateLevels (0);
		Queue level3 = GenerateLevels (90);
		GenerateTrailStart (start, level1);
		GenerateTrailBetween (level1, level2);
		GenerateTrailBetween (level2, level3);
		GenerateTrailEnd (end, level3);
		Populate ();
	}
	public void GenerateTrailStart(Vector2 a, Queue bN)
	{
		Queue b = new Queue (bN);
		while (b.Count!=0)
		{
			Vector2 temp = (Vector2)b.Dequeue();
			GenerateTrail(temp,a);
			Node node = new Node();
			node.parent = a;
			node.children.Add(temp);
			node0.Add(node);
		}
	}
	public void GenerateTrailEnd(Vector2 a, Queue bN)
	{
		Queue b = new Queue (bN);
		while (b.Count!=0)
		{
			Vector2 temp = (Vector2)b.Dequeue();
			GenerateTrail(temp,a);
			Node node = new Node();
			node.parent = temp;
			node.children.Add (a);
			node3.Add (node);
		}
	}
	public void GenerateTrailBetween(Queue leftN, Queue rightN)
	{
		Queue left = new Queue (leftN);
		Queue right = new Queue(rightN);
		if (left.Count == right.Count) {
			while (left.Count!=0) {
				Vector2 l = (Vector2)left.Dequeue ();
				Vector2 r = (Vector2)right.Dequeue ();
				GenerateTrail (l, r);
			}
		} 
		else if (left.Count > right.Count) {
			Vector2 last = new Vector2();
			while (right.Count!=0) {
				Vector2 l = (Vector2)left.Dequeue ();
				last = (Vector2)right.Dequeue ();
				GenerateTrail (l, last);
			}
			while (left.Count!=0){
				Vector2 l = (Vector2)left.Dequeue();
				GenerateTrail(l,last);
			}
		} 
		else {
			Vector2 last = new Vector2();
			while (left.Count!=0) {
				Vector2 r = (Vector2)right.Dequeue ();
				last = (Vector2)left.Dequeue ();
				GenerateTrail (r, last);
			}
			while (right.Count!=0){
				Vector2 r = (Vector2)right.Dequeue();
				GenerateTrail(r,last);
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
			Vector2 node = new Vector2(level, Random.Range (90-i*interval,90-i*interval-interval));
			vectors.Add (node);
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
		foreach (Vector2 vector in vectors) 
		{
			GameObject instance = (GameObject)UnityEngine.Object.Instantiate(buttonPrefab);
			instance.transform.SetParent (parent, false);
			Vector2 button_position = vector;
			instance.GetComponent<RectTransform>().anchoredPosition = button_position;
		}
		foreach (Vector2 trail in trails) 
		{
			GameObject instance = (GameObject)UnityEngine.Object.Instantiate(trailPrefab);
			instance.transform.SetParent (parent, false);
			Vector2 button_position = trail;
			instance.GetComponent<RectTransform>().anchoredPosition = button_position;
		}
	}

	public void Save()
	{
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Open (Application.persistentDataPath + "/mapData.dat", FileMode.Open);
		MapData data = new MapData ();
		data.vectors = vectors;
		data.trails = trails;
		bf.Serialize (file, data);
		file.Close ();
	}

	public void Load()
	{
		if (File.Exists (Application.persistentDataPath + "/mapData.dat")) 
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open (Application.persistentDataPath + "/mapData.dat", FileMode.Open);
			MapData data = (MapData)bf.Deserialize(file);
			file.Close ();
			vectors = data.vectors;
			trails = data.trails;
		}
	}
}

[System.Serializable]
class MapData
{
	public ArrayList vectors;
	public ArrayList trails;
}

class Node
{
	public Vector2 parent = new Vector2();
	public ArrayList children = new ArrayList();
	public Node()
	{



	}
}
