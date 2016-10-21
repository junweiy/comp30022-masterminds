using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public class GroundRecord : IRecord {
	float _scale;
	float _timePassedSinceLastShrink;

    public void ApplyEffect(RecordHandler c) {
		c.SetGround (_scale, _timePassedSinceLastShrink);
    }

	public GroundRecord(float scale, float timePassedSinceLastShrink) {
		this._scale = scale;
		this._timePassedSinceLastShrink = timePassedSinceLastShrink;
	}
}
