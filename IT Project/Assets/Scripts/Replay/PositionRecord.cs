using UnityEngine;

// Record for the position of a GameObject
[System.Serializable]
public class PositionRecord : IRecord {
    public int Id;
    public float X;
    public float Y;
    public float Z;


    public void ApplyEffect(RecordHandler c) {
        c.SetPosition(Id, new Vector3(X, Y, Z));
    }

    public PositionRecord(int objId, Vector3 position) {
        this.Id = objId;
        this.X = position.x;
        this.Y = position.y;
        this.Z = position.z;
    }
}