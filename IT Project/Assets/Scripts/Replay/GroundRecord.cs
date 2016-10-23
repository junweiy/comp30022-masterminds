// Record representing state of the ground, including its scale and time to the next shrink
[System.Serializable]
public class GroundRecord : IRecord {
    private float _scale;
    private float _timePassedSinceLastShrink;

    public void ApplyEffect(RecordHandler c) {
        c.SetGround(_scale, _timePassedSinceLastShrink);
    }

    public GroundRecord(float scale, float timePassedSinceLastShrink) {
        this._scale = scale;
        this._timePassedSinceLastShrink = timePassedSinceLastShrink;
    }
}