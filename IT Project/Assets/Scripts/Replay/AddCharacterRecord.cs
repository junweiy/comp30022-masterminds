using System;

[Serializable]
public class AddCharacterRecord : IRecord {
    public int ObjId;
    public int CharId;
    public string UserName;

    public void ApplyEffect(RecordHandler c) {
        c.InstantiateCharacterWith(ObjId, CharId, UserName);
    }

    public AddCharacterRecord(int objId, int charId, String userName) {
        this.ObjId = objId;
        this.CharId = charId;
        this.UserName = userName;
    }
}