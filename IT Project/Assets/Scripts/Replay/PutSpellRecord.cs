using UnityEngine;
using System.Collections;
using System;
using Replay;

[System.Serializable]
public class PutSpellRecord : ReplayRecord {

    public SpellType spellType;

    public float positionX;
    public float positionY;
    public float positionZ;

    public float quaternionX;
    public float quaternionY;
    public float quaternionZ;
    public float quaternionW;

    public void applyEffect(ReplaySceneController c) {
        Vector3 position = new Vector3(positionX, positionY, positionZ);
        Quaternion rotation = new Quaternion(quaternionX, quaternionY, quaternionZ, quaternionW);
        c.IntantiateSpellWithTransform(spellType, position, rotation);
    }

    public PutSpellRecord(Spell s, Transform trans) {
        this.spellType = ReplayTypeConverter.GetTypeFromSpell(s);
        this.positionX = trans.position.x;
        this.positionY = trans.position.y;
        this.positionZ = trans.position.z;
        this.quaternionX = trans.rotation.x;
        this.quaternionY = trans.rotation.y;
        this.quaternionZ = trans.rotation.z;
        this.quaternionW = trans.rotation.w;
    }


}
