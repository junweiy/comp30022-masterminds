using UnityEngine;
using System.Collections;

public class GroundController : MonoBehaviour {

	// Seconds between every shrink
	public float secondsBetweenShrinking;
	// The minimum size presented as a ratio to the initial size
	public float minScalableSizeRatio;
	// Size shrunk every time
	public float sizeShrunkPerTime;
	// Time passed since last shrink
	public float timePassed;

	void Start () {
		timePassed = 0;
	}

	void Update() {
		timePassed += Time.deltaTime;
		if (timePassed >= secondsBetweenShrinking && transform.localScale.x > minScalableSizeRatio) {
			timePassed -= secondsBetweenShrinking;
			Scale ();
		}
	}


	void Scale() {
		Vector3 tempSize = transform.localScale;
		tempSize.x = transform.localScale.x - sizeShrunkPerTime;
		tempSize.y = transform.localScale.y;
		tempSize.z = transform.localScale.z - sizeShrunkPerTime;
		transform.localScale = tempSize;
	}

}
