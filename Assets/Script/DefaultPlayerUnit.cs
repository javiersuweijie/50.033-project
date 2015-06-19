using UnityEngine;
using System.Collections;

public class DefaultPlayerUnit : PlayerUnit{
	
	protected bool jump = false;
	protected float jumpTime = 0.0f;
	protected float jumpHeight = 0.0f;
	protected float yPos;
	public Transform uitxt;
	
	protected EnemyPartyController enemyPartyController;

	void Start(){
		Debug.Log ("Instantiated");
		Init(Time.time);
		yPos = transform.position.y;
		GameObject controller = GameObject.FindWithTag("Controller");
		if (controller != null){
			enemyPartyController = controller.GetComponent<EnemyPartyController>();
		}
		else{
			Debug.LogError("Cannot find controller object.");
		}
	}

	public override void Attack(){
		if(Time.time > ( lastAtkTime + atkDelay) )
		{
			lastAtkTime = Time.time;
			enemyPartyController.GetFrontTarget().DecreaseHealth(atkPower);
			jump = true;

			//float dmg
		}
		Jump ();
	}
	
	public override void UseSkill(Unit target){}
	
	public void Jump(){
		if (jump){
			Debug.Log("Jumping");
			if (jumpTime < 0.1f){
				jumpTime += Time.deltaTime;
				jumpHeight += 5.0f * Time.deltaTime;
			}
			else{
				if (jumpHeight > 0.0f){
					jumpHeight -= 5.0f * Time.deltaTime;
				}
				else{
					jumpTime = 0.0f;
					jumpHeight = 0.0f;
					jump = false;
				}
			}
		}
		transform.position = new Vector3(transform.position.x, yPos + jumpHeight, transform.position.z);
	}
}
