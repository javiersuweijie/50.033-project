using UnityEngine;
using System.Collections;

public class ScrollScript : MonoBehaviour {

	// Use this for initialization
	public float speed = 0;
	public float duration = 1.0f;
	private Renderer r;
	Color colors;
	Color colort;

	void Start(){
		r = gameObject.GetComponent<Renderer> ();
		colors = r.material.color;
		colort = new Color(colors.r, colors.g, colors.b, 0.0f);
		r.material.color = colort;
		StartCoroutine(FadeIn ());
	}
	
	// Update is called once per frame
	void Update () {
		r.material.mainTextureOffset = new Vector2((Time.time* speed)%1, 0f);

	}

	protected IEnumerator FadeIn(){

		for (float t = 0.0f; t < duration; t += Time.deltaTime) {
			r.material.color = Color.Lerp (colort, colors, t / duration);
			yield return new WaitForSeconds (0.01f);
		}
	}
}
