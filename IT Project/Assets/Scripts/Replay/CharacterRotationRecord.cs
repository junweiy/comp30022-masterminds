using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public class RotationRecord : Record {
    public int id;
    public float x;
    public float y;
    public float z;
    public float w;


    public void applyEffect(RecordHandler c) {
        c.SetRotation(id, new Quaternion(x, y, z, w));
    }

    public RotationRecord(int objId, Quaternion rotation) {
        this.id = objId;
        this.x = rotation.x;
        this.y = rotation.y;
        this.z = rotation.z;
        this.w = rotation.w;
    }

}
