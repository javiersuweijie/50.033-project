using UnityEngine;
using System.Collections;

public class StaminaBar : MonoBehaviour {
	
	public float shiftSpd = 15;
	
	private RectTransform stamfill;
	private RectTransform mask;
	private float maskyVal, maskzVal, fillxVal, fillyVal, fillzVal, minXVal, maxXVal, tgtXVal, barLength, lengthvalueratio;
	private int maxStam, currentStam;
	// Use this for initialization
	void Start () {
		Init (1000, 500);
	}
	
	// Update is called once per frame
	void Update () {
		HandleMovement();
	}
	
	public void Init(int maxStam, int currentStam){
		this.maxStam = maxStam;
		this.currentStam = currentStam;
		mask = (RectTransform) gameObject.transform.GetChild(0).GetChild(1);
		stamfill = (RectTransform) mask.GetChild(0);
		maskyVal = mask.position.y;
		maskzVal = mask.position.z;
		fillxVal = stamfill.position.x;
		fillyVal = stamfill.position.y;
		fillzVal = stamfill.position.z;
		maxXVal = mask.position.x;
		barLength = mask.rect.width;
		minXVal = maxXVal - barLength;
		lengthvalueratio = barLength/maxStam;
		tgtXVal = minXVal + currentStam * lengthvalueratio;
		ShiftBar(tgtXVal - maxXVal);
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
		if (mask.position.x > tgtXVal )
		{
			ShiftBar(-shiftSpd * Time.deltaTime);
			if (mask.position.x < tgtXVal)
			{
				SetBarOffsetFromFull(tgtXVal - maxXVal);
			}
		}
		else if (mask.position.x < tgtXVal )
		{
			ShiftBar(shiftSpd * Time.deltaTime);
			if (mask.position.x > tgtXVal)
			{
				SetBarOffsetFromFull(tgtXVal - maxXVal);
			}
		}
	}

	private void ShiftBar(float value){
		mask.Translate(new Vector3(value, 0, 0));
		stamfill.position = new Vector3(fillxVal, fillyVal, fillzVal);
	}
	
	private void SetBarOffsetFromFull(float value){
		mask.position = new Vector3(value, maskyVal, maskzVal);
		stamfill.position = new Vector3(fillxVal, fillyVal, fillzVal);
	}
}
