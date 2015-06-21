using UnityEngine;
using System.Collections;

public class FloatDmgScript : MonoBehaviour {

	public GUIText myGUItext;
	public float guiTime = 0.5f;
	
	public void DisplayDamage(string damageMessage)
	{
		Debug.Log("damage message should be " + damageMessage);
		myGUItext.text = damageMessage;
		
		// destory after time is up
		StartCoroutine(GuiDisplayTimer());
	}

	void Update()
	{
		transform.Translate (0, 0.3f * Time.deltaTime, 0);
		Color color = myGUItext.material.color;
		color.a -= 1.25f * Time.deltaTime;
		myGUItext.material.color = color;
	}

	IEnumerator GuiDisplayTimer()
	{
		// Waits an amount of time
		yield return new WaitForSeconds(guiTime);
		
		// destory game object
		Destroy(gameObject);
	}
}
