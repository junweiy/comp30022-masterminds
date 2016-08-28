using UnityEngine;
using System.Collections;

public class GroundController : MonoBehaviour {

	// 
	public float secondsBetweenShrinking;
	// The minimum size presented as a ratio to the initial size
	public float minScalableSizeRatio;
	// Size shrunk every time
	public float sizeShrunkPerTime;
	// The initial scale
	private Vector3 initialScale;

	void Start () {
		StartCoroutine (Scale ());
	}

	IEnumerator Scale() {
		initialScale = transform.localScale;
		Vector3 tempSize = transform.localScale;

		while (tempSize.x > initialScale.x * minScalableSizeRatio) {
			yield return new WaitForSecondsRealtime(secondsBetweenShrinking);
			tempSize.x = transform.localScale.x - sizeShrunkPerTime;
			tempSize.y = transform.localScale.y;
			tempSize.z = transform.localScale.z - sizeShrunkPerTime;
			transform.localScale = tempSize;
		}


	}

}
