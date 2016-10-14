using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class AddCharacterRecord : Record {

    public int ObjId;
    public int CharId;

    public void applyEffect(RecordHandler c) {
        c.InstantiateCharacterWith(ObjId, CharId);
    }

    public AddCharacterRecord(int objId, int charId) {
        this.ObjId = objId;
        this.CharId = charId;
    }
}
