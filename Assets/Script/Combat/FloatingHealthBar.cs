using UnityEngine;
using System.Collections;

public class FloatingHealthBar : MonoBehaviour {

	public GameObject hpbarobj = (GameObject)Resources.Load ("Prefabs/FloatingHPBar", typeof(GameObject));
	public float shiftSpd = 1;

	private GameObject hpbarinstance;
	private Transform hpbar;
	private Unit unit;
	private RectTransform hpfill;
	private float yVal, zVal, minXVal, maxXVal, tgtXVal;
	private float maxHealth, currentHealth, barLength;

	// Use this for initialization
	void Start () {
		hpbarinstance = Instantiate(hpbarobj);
		hpbar = hpbarinstance.GetComponent<Transform>();
		hpfill = (RectTransform) hpbar.GetChild(0).GetChild(0).GetChild(0).GetChild(0);
		unit = this.gameObject.GetComponent<UnitController>().unit;
		InitHPBar(unit.GetMaxHealth(), unit.GetCurrentHealth(), transform.position.x, transform.position.y + 1.1f);
	}
	
	// Update is called once per frame
	void Update () {
		UpdateHealth();
		HandleMovement();
	}

	private void InitHPBar(float maxHP, float currentHP, float xpos, float ypos)
	{
		hpbar.position = new Vector3(xpos, ypos, 0);
		yVal = hpfill.position.y;
		zVal = hpfill.position.z;
		maxXVal = hpfill.position.x;
		barLength = hpfill.rect.width * hpbarinstance.transform.localScale.x;
		minXVal = maxXVal - barLength;
		maxHealth = maxHP;
		currentHealth = currentHP;
	}

	public void UpdateHealth()
	{
		currentHealth = unit.GetCurrentHealth();
		tgtXVal = minXVal + currentHealth/maxHealth * barLength;
	}

	private void HandleMovement()
	{
		tgtXVal = Mathf.Clamp(tgtXVal, minXVal, maxXVal);
		if (hpfill.position.x > tgtXVal )
		{
			hpfill.Translate(new Vector3(-shiftSpd * Time.deltaTime, 0, 0));
			if (hpfill.position.x < tgtXVal)
			{
				hpfill.position = new Vector3(tgtXVal, yVal, zVal);
			}
		}
		else if (hpfill.position.x < tgtXVal )
		{
			hpfill.Translate(new Vector3(shiftSpd * Time.deltaTime, 0, 0));
			if (hpfill.position.x > tgtXVal)
			{
				hpfill.position = new Vector3(tgtXVal, yVal, zVal);
			}
		}
	}
	
	public void DestroyHPBar()
	{
		Destroy(hpbarinstance);
	}
}
