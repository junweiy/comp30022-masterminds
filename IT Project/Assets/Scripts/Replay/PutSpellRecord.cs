using UnityEngine;
using System.Collections;
using System;
using Replay;

[System.Serializable]
public class PutSpellRecord : IRecord {
    public SpellType SpellType;

    public float PositionX;
    public float PositionY;
    public float PositionZ;

    public float QuaternionX;
    public float QuaternionY;
    public float QuaternionZ;
    public float QuaternionW;

    public int CasterId;

    public void ApplyEffect(RecordHandler c) {
        Vector3 position = new Vector3(PositionX, PositionY, PositionZ);
        Quaternion rotation = new Quaternion(QuaternionX, QuaternionY, QuaternionZ, QuaternionW);
        c.InstantiateSpellWith(SpellType, CasterId, position, rotation);
    }

    public PutSpellRecord(Spell s, Transform trans, int casterId) {
        this.SpellType = ReplayTypeConverter.GetTypeFromSpell(s);
        this.PositionX = trans.position.x;
        this.PositionY = trans.position.y;
        this.PositionZ = trans.position.z;
        this.QuaternionX = trans.rotation.x;
        this.QuaternionY = trans.rotation.y;
        this.QuaternionZ = trans.rotation.z;
        this.QuaternionW = trans.rotation.w;
        this.CasterId = casterId;
    }
}