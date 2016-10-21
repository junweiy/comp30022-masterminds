using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public class RotationRecord : IRecord {
    public int Id;
    public float X;
    public float Y;
    public float Z;
    public float W;


    public void ApplyEffect(RecordHandler c) {
        c.SetRotation(Id, new Quaternion(X, Y, Z, W));
    }

    public RotationRecord(int objId, Quaternion rotation) {
        this.Id = objId;
        this.X = rotation.x;
        this.Y = rotation.y;
        this.Z = rotation.z;
        this.W = rotation.w;
    }

}
