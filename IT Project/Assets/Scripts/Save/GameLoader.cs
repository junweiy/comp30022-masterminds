using UnityEngine;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

// Class that loads a game save file
public class GameLoader : RecordHandler {
    // Loads the GameSave object, applying all records in the save
    public void Load(GameSave save) {
        foreach (var record in save.Records) {
            record.ApplyEffect(this);
        }
    }

    // Reads the game save file in default position
    public GameSave ReadFile() {
        string saveFilePath = Application.persistentDataPath + "/SaveFiles/";
        Directory.CreateDirectory(saveFilePath);
        string filePath = saveFilePath + "/test.sav";
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        return (GameSave) formatter.Deserialize(stream);
    }
}