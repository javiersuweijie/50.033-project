using UnityEngine;
using System.Collections;

public class FloatDmgScript : MonoBehaviour {

	public GUIText myGUItext;
	public float guiTime = 0.5f;

	private bool moveup = true;
	
	public void DisplayDamage(string damageMessage)
	{
		//Debug.Log("damage message should be " + damageMessage);
		myGUItext.text = damageMessage;
		
		// destory after time is up
		StartCoroutine(GuiDisplayTimer());
		StartCoroutine(Stop ());
	}

	void Update()
	{
		if (moveup)
			transform.Translate (0, 0.15f * Time.deltaTime, 0);
		else {
			Color color = myGUItext.material.color;
			color.a -= 1.0f * Time.deltaTime;
			myGUItext.material.color = color;
		}
	}

	protected IEnumerator GuiDisplayTimer()
	{
		// Waits an amount of time
		yield return new WaitForSeconds(guiTime);
		
		// destory game object
		Destroy(gameObject);
	}

	protected IEnumerator Stop()
	{
		// Waits an amount of time
		yield return new WaitForSeconds(0.13f);
		moveup = false;
	}
}
