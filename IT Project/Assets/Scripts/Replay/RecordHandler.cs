using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Replay;
using System;

public class RecordHandler : MonoBehaviour {

    Dictionary<int, GameObject> gameObjMap = new Dictionary<int, GameObject>();

    public void SetCharId(int objId, int charId) {
        var obj = gameObjMap[objId];
        obj.GetComponent<Character>().charID = charId;
    }

    public void SetPlayerHp(object playerId, int hp) {
        throw new NotImplementedException();
    }

    public void SetPosition(int objId, Vector3 position) {
        throw new System.NotImplementedException();
    }

    public void SetRotation(int objId, Quaternion rotation) {
        throw new System.NotImplementedException();
    }

    public void SetScale(int objId, Vector3 scale) {
        throw new System.NotImplementedException();
    }

    public void SetGround() {
        throw new System.NotImplementedException();
    }

    public void InstantiateSpellWith(SpellType type, int casterId, Vector3 position, Quaternion rotation) {
        throw new System.NotImplementedException();
    }

    public void ApplyRecord(Record record) {
        record.applyEffect(this);
    }


}
