using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public class GroundRecord : Record {
	float scale;
	float timePassedSinceLastShrink;

    public void applyEffect(RecordHandler c) {
		c.SetGround (scale, timePassedSinceLastShrink);
    }

	public GroundRecord(float scale, float timePassedSinceLastShrink) {
		this.scale = scale;
		this.timePassedSinceLastShrink = timePassedSinceLastShrink;
	}
}
