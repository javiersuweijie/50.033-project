using UnityEngine;
using System.Collections;

public class StaminaBar : MonoBehaviour {

	public float shiftSpd = 2;

	private RectTransform stamfill;
	private float yVal, zVal, minXVal, maxXVal, tgtXVal, barLength, lengthvalueratio;
	private int maxStam, currentStam;
	// Use this for initialization
	void Start () {
		Init (1000, 1000);
	}
	
	// Update is called once per frame
	void Update () {
		HandleMovement();
	}

	public void Init(int maxStam, int currentStam){
		this.maxStam = maxStam;
		this.currentStam = currentStam;
		RectTransform stambg = (RectTransform) gameObject.transform.GetChild(0).GetChild(0).GetChild(0);
		stamfill = (RectTransform) stambg.GetChild(0);
		yVal = stamfill.position.y;
		zVal = stamfill.position.z;
		maxXVal = stamfill.position.x;
		barLength = stamfill.rect.width;
		minXVal = maxXVal - barLength;
		lengthvalueratio = barLength/maxStam;
		tgtXVal = minXVal + currentStam * lengthvalueratio;
		stamfill.position = new Vector3(tgtXVal, yVal, zVal);
	}
	
	public bool UseStamina(int value)
	{
		if (currentStam >= value){
			currentStam -= value;
			currentStam = Mathf.Clamp(currentStam, 0, maxStam);
			tgtXVal = minXVal + currentStam * lengthvalueratio;
			return true;
		}
		else{
			return false;
		}
	}

	public bool RecoverStamina(int value)
	{
		if (currentStam < maxStam){
			currentStam += value;
			currentStam = Mathf.Clamp(currentStam, 0, maxStam);
			tgtXVal = minXVal + currentStam * lengthvalueratio;
			return true;
		}
		else{
			return false;
		}
	}
	
	private void HandleMovement()
	{
		tgtXVal = Mathf.Clamp(tgtXVal, minXVal, maxXVal);
		if (stamfill.position.x > tgtXVal )
		{
			stamfill.Translate(new Vector3(-shiftSpd * Time.deltaTime, 0, 0));
			if (stamfill.position.x < tgtXVal)
			{
				stamfill.position = new Vector3(tgtXVal, yVal, zVal);
			}
		}
		else if (stamfill.position.x < tgtXVal )
		{
			stamfill.Translate(new Vector3(shiftSpd * Time.deltaTime, 0, 0));
			if (stamfill.position.x > tgtXVal)
			{
				stamfill.position = new Vector3(tgtXVal, yVal, zVal);
			}
		}
	}
}
