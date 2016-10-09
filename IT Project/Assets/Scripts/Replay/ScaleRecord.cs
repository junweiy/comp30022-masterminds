using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public class ScaleRecord : Record {
    public int id;
    public float x;
    public float y;
    public float z;


    public void applyEffect(RecordHandler c) {
        c.SetScale(id, new Vector3(x, y, z));
    }

    public ScaleRecord(int objId, Vector3 scale) {
        this.id = objId;
        this.x = scale.x;
        this.y = scale.y;
        this.z = scale.z;
    }

}
