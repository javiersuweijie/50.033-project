using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//PBuffIDs:
// 0 : atk
// 1 : def
// 2 : aspd

public class BuffManager : MonoBehaviour {

	protected GameObject atkU = (GameObject)Resources.Load ("Sprites/ATK_UpFab", typeof(GameObject));
	protected  GameObject atkD =  (GameObject)Resources.Load ("Sprites/ATK_DownFab", typeof(GameObject));
	protected GameObject defU = (GameObject)Resources.Load ("Sprites/DEF_UpFab", typeof(GameObject));
	protected GameObject defD = (GameObject)Resources.Load ("Sprites/DEF_DownFab", typeof(GameObject));
	protected GameObject agiU = (GameObject)Resources.Load ("Sprites/AGI_UpFab", typeof(GameObject));
	protected GameObject agiD = (GameObject)Resources.Load ("Sprites/AGI_DownFab", typeof(GameObject));
	public Dictionary<int,GameObject> dict;

	private List<int> ActivePBuffs = new List<int>();
	private float[] PBuffArray = new float[3]{1.0f,1.0f,1.0f};
	private List<GameObject> icons = new List<GameObject>();

	void Start(){
		dict = new Dictionary<int,GameObject>();
		dict.Add (1, atkU);
		dict.Add (2, defU);
		dict.Add (3, agiU);
		dict.Add (-1, atkU);
		dict.Add (-2, defU);
		dict.Add (-3, agiU);
		StartCoroutine (reducePotency ());
	}

	void Update(){

	}

	public float GetATKMod()
	{
		return PBuffArray [0];
	}

	public float GetDEFMod()
	{
		return PBuffArray [1];
	}

	public float GetAGIMod()
	{
		return PBuffArray [2];
	}

	public void ApplyBuff(int id, float potency)
	{
		PBuffArray [id] += potency;
		UpdateBuffs ();
	}
	
	private bool CheckBuff(int id)
	{
		return ActivePBuffs.Contains (id);
	}

	private void UpdateBuffs()
	{
		foreach (GameObject icon in icons) {
			Destroy (icon);
		}

		icons.Clear ();

		ActivePBuffs.Clear ();
		for (int i =0; i < 3; i++) {
			if ((PBuffArray[i] - 1.0f) > 0.04f)
			{
				ActivePBuffs.Add(i+1);
			} else  if ((PBuffArray[i] - 1.0f) < -0.04f)
			{
				ActivePBuffs.Add (-i-1);
			}
		}

		RenderBuffs ();
	}

	private void RenderBuffs()
	{
		float x = -0.5f;
		float y = 1.5f;

		foreach (int buff in ActivePBuffs) {
			Vector3 pos = new Vector3(gameObject.transform.position.x + x, gameObject.transform.position.y + y, 0.0f);
			icons.Add (Instantiate(dict[buff] , pos ,Quaternion.identity) as GameObject);
				x += 0.3f;
		}
	}

	IEnumerator reducePotency()
	{
		while (true) {
			yield return new WaitForSeconds(5.0f);
			for (int i =0; i < 3; i++)
			{
					PBuffArray[i] = 1.0f + (PBuffArray[i] - 1.0f )* 0.50f;
				if ( Mathf.Abs(PBuffArray[i] - 1.0f) < 0.04f)
				{
					PBuffArray[i] = 1.0f;
					UpdateBuffs ();
				}
			}
		}
	}


}