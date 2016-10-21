using UnityEngine;

[System.Serializable]
public class ScaleRecord : IRecord {
    public int Id;
    public float X;
    public float Y;
    public float Z;


    public void ApplyEffect(RecordHandler c) {
        c.SetScale(Id, new Vector3(X, Y, Z));
    }

    public ScaleRecord(int objId, Vector3 scale) {
        this.Id = objId;
        this.X = scale.x;
        this.Y = scale.y;
        this.Z = scale.z;
    }
}