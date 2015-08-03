using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SpawnButton : MonoBehaviour {

	// Use this for initialization
	public RectTransform parent;
	public GameObject buttonPrefab;
	public GameObject trailPrefab;
	public ArrayList vectors = new ArrayList ();
	public ArrayList trails = new ArrayList ();
	public float randomSeed = (float)0.1;
	private int radius = 8;
	void Start () {
		Vector2 start = new Vector2 (0, 0);
		SequentialGenerator (start, 3);
		Populate ();
	}
	public void SequentialGenerator(Vector2 start, int number)
	{
		vectors.Add (start);
		for (int i = 0; i<number; i++) 
		{
			ArrayList tempTrails = new ArrayList ();
			tempTrails.Add(start);
			for(int j = 0; j < 10; j++)
			{
				Vector2 previous = (Vector2)tempTrails[tempTrails.Count-1];
				int randomX = Random.Range (radius/2 + radius/4 ,radius);
				float randomY = (float)Mathf.Sqrt((float)(radius * radius - randomX * randomX));
				int prob = 0;
				if (i==0)
				{
					prob = 1;
				}
				else if (i==1)
				{
					int upDown = Random.Range(1,100);
					if (upDown<60)
					{
						prob = -1;
					}
					else
					{
						prob = 1;
					}
				}
				else if (i==2)
				{
					prob = -1;
				}
				Vector2 new_trail = new Vector2(previous.x+randomX,previous.y+(randomY)*prob);
				if (i==1){
					if(j<4){
						new_trail = new Vector2(previous.x+radius,previous.y);
					}
				}
				if (j==9)
				{
					vectors.Add (new_trail);
				}
				else
				{
					trails.Add (new_trail);
					tempTrails.Add (new_trail);
				}
			}
		}
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
//			Debug.Log (first_x);
//			Debug.Log (first_y);
//			Debug.Log (second_x);
//			Debug.Log (second_y);
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
				float var = ((first_y - second_y)/2)*randomSeed;
				third_y = Random.Range ((int)(val - var),(int)(val + var));
			}
			else
			{
				int val = (second_y - first_y)/2 + first_y;
				float var = ((second_y - first_y)/2)* randomSeed;
//				Debug.Log (randomSeed);
//				Debug.Log (((second_y-first_y)/2)*randomSeed);
//				Debug.Log (var);
				third_y = Random.Range ((int)(val - var),(int)(val + var));
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
