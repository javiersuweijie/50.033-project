using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideRight : MonoBehaviour {

	public float duration = 2;
	public float speed = 5;
	
	void Start() {
		Vector3 newPos = this.transform.position;
		newPos.x += this.GetComponentInChildren<SpriteRenderer> ().bounds.max.x - this.GetComponentInChildren<SpriteRenderer> ().bounds.min.x;
		StartCoroutine (MoveTo(this.transform, newPos, 0.2f)); 
	}
	
	IEnumerator MoveTo(Transform objectToMove, Vector3 targetPosition, float timeToTake)
	{
		float t = 0;
		Vector3 originalPosition = objectToMove.position;
		
		while (t < 1) {
			t += Time.deltaTime / timeToTake;
			objectToMove.position = Vector3.Lerp(originalPosition, targetPosition, t);
			yield return null;
		}
	}


}