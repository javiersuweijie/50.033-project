using UnityEngine;
using System.Collections;

class DefaultPlayerUnit : PlayerUnit{
	
	protected bool jump = false;
	protected float jumpTime = 0.0f;
	protected float jumpHeight = 0.0f;
	protected float yPos;

	void Start(){
		Debug.Log ("Instantiated");
		Init(Time.time);
		yPos = transform.position.y;
	}

	public override void Attack(Unit target){
		if(Time.time > ( lastAtkTime + atkDelay) )
		{
			lastAtkTime = Time.time;
			target.DecreaseHealth(atkPower);
			jump = true;
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
