using Replay;

public static class ReplayTypeConverter {
    public static CharacterType GetTypeFromCharacter(Character character) {
        if (character.GetType() == typeof(Character)) {
            return CharacterType.Character;
        }

        throw new NonMatchedReplayTypeException();
    }

    public static Character GetCharacterFromType(CharacterType type) {
        if (type == CharacterType.Character) {
            return new Character();
        }

        throw new NonMatchedReplayTypeException();
    }

    public static SpellType GetTypeFromSpell(Spell s) {
        System.Type typ = s.GetType();
        if (typ == typeof(FireNova)) {
            return SpellType.FireNova;
        } else if (typ == typeof(FireBall)) {
            return SpellType.Fireball;
        }

        throw new NonMatchedReplayTypeException();
    }

    public static Spell GetSpellFromType(SpellType type) {
        if (type == SpellType.Fireball) {
            return new FireBall();
        } else if (type == SpellType.FireNova) {
            return new FireNova();
        }

        throw new NonMatchedReplayTypeException();
    }
}


public class NonMatchedReplayTypeException : System.Exception {}