// Record for a character's HP
[System.Serializable]
public class CharacterHpRecord : IRecord {
    public int Id;
    public int Hp;

    public void ApplyEffect(RecordHandler c) {
        c.SetCharacterHp(Id, Hp);
    }

    public CharacterHpRecord(int objId, int hp) {
        this.Hp = hp;
        this.Id = objId;
    }
}