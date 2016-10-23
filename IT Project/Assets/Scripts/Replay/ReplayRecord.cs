// The interface for a record, representing a part of the game state
public interface IRecord {
    // Defines what action should be done when the record is applied
    void ApplyEffect(RecordHandler c);
}