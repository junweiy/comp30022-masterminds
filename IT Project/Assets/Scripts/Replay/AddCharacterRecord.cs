﻿using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class AddCharacterRecord : Record {

    public int ObjId;
    public int CharId;
	public string UserName;

    public void applyEffect(RecordHandler c) {
        c.InstantiateCharacterWith(ObjId, CharId, UserName);
    }

	public AddCharacterRecord(int objId, int charId, String userName) {
        this.ObjId = objId;
        this.CharId = charId;
		this.UserName = userName;
    }
}
