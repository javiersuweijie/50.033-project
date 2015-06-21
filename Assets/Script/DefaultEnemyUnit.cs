//using UnityEngine;
//using System.Collections;
//
//public class DefaultEnemyUnit : EnemyUnit {
//
//	protected bool jump = false;
//	protected float jumpTime = 0.0f;
//	protected float jumpHeight = 0.0f;
//	protected float yPos;
//
//	protected PlayerPartyController playerPartyController;
//
//	public GameObject bullet;
//	
//	void Start(){
//		Init(Time.time);
//		yPos = transform.position.y;
//		GameObject controller = GameObject.FindWithTag("Controller");
//		if (controller != null){
//			playerPartyController = controller.GetComponent<PlayerPartyController>();
//		}
//		else{
//			Debug.LogError("Cannot find controller object.");
//		}
//	}
//
//	
//	public override void Attack(){
//		if(Time.time > ( lastAtkTime + atkDelay) )
//		{
//			lastAtkTime = Time.time;
//			//target.DecreaseHealth(atkPower);
//			Vector3 vec3 = new Vector3(transform.position.x - 1, transform.position.y + 1);
//			int angle = Random.Range (35,55);
//			GameObject instbull = (GameObject)Instantiate(bullet, vec3,Quaternion.Euler (0,0,angle));
//			jump = true;
//		}
//		Jump ();
//	}
//
//	public override void UseSkill(Unit target){
//	}
//
//	public void Jump(){
//		if (jump){
//			Debug.Log("Jumping");
//			if (jumpTime < 0.1f){
//				jumpTime += Time.deltaTime;
//				jumpHeight += 5.0f * Time.deltaTime;
//			}
//			else{
//				if (jumpHeight > 0.0f){
//					jumpHeight -= 5.0f * Time.deltaTime;
//				}
//				else{
//					jumpTime = 0.0f;
//					jumpHeight = 0.0f;
//					jump = false;
//				}
//			}
//		}
//		transform.position = new Vector3(transform.position.x, yPos + jumpHeight, transform.position.z);
//	}
//}
