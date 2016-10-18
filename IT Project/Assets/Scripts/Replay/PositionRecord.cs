using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public class PositionRecord : Record {
    public int id;
    public float x;
    public float y;
    public float z;


    public void applyEffect(RecordHandler c) {
        c.SetPosition(id, new Vector3(x, y, z));
    }

    public PositionRecord(int objId, Vector3 position) {
        this.id = objId;
        this.x = position.x;
        this.y = position.y;
        this.z = position.z;
    }

}
