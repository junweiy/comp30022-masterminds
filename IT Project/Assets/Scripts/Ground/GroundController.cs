using UnityEngine;
using System.Collections;

public class GroundController : Photon.MonoBehaviour {

	// Seconds between every shrink
	public float secondsBetweenShrinking;
	// The minimum size presented as a ratio to the initial size
	public float minScalableSizeRatio;
	// Size shrunk every time
	public float sizeShrunkPerTime;
	// Time passed since last shrink
	public float timePassed;
	// Initial scale
	public float initialScale;

	void Start () {
		initialScale = transform.localScale.x;
		timePassed = 0;
	}

	void Update() {
		timePassed += Time.deltaTime;
		if (timePassed >= secondsBetweenShrinking && transform.localScale.x > initialScale * minScalableSizeRatio) {
			timePassed -= secondsBetweenShrinking;
			Scale ();
		}
	}

	public void SetTimePassedForAll(float timePassed) {
		this.photonView.RPC ("SetTimePassedRPC", PhotonTargets.All, timePassed); 
	}

	[PunRPC]
	public void SetTimePassedRPC(float timePassed) {
		this.timePassed = timePassed;
	}

	public void SetScaleForAll(float scale) {
		this.photonView.RPC ("SetScaleRPC", PhotonTargets.All, scale); 
	}

	[PunRPC]
	public void SetScaleRPC(float scale) {
		this.transform.localScale = new Vector3(scale, 1, scale);
	}

	void Scale() {
		Vector3 tempSize = transform.localScale;
		tempSize.x = transform.localScale.x - sizeShrunkPerTime;
		tempSize.y = transform.localScale.y;
		tempSize.z = transform.localScale.z - sizeShrunkPerTime;
		transform.localScale = tempSize;
	}

}
