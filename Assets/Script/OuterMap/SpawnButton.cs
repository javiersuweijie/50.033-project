using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class SpawnButton : MonoBehaviour {

	public GameObject button;
	public Transform parent;
	public int level;

	public void Spawn()
	{
		level += 1;
		int fork = (int)Mathf.Round (Random.Range (1, 4));
		for (int i = 0;i < fork; i++){
			GameObject new_button = Instantiate (button, new Vector2 (level*30,100+i*-30), Quaternion.Euler (Vector3.zero)) as GameObject;
			new_button.transform.SetParent(parent, false);
			new_button.GetComponentInChildren<Text>().text = level.ToString();
			Debug.Log (i);
		}
	}
}
