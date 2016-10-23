using UnityEngine;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

// Class that saves the game to a file
public class GameSaver : StateRecorder {
    // Reads all gamestate at this point and save to a file
    public void Save() {
        AddRecords();
        SaveInfo info = new SaveInfo();
        GameSave save = new GameSave(Pending, info);
        CreateFile(save);
    }

    // Create and write game save at the default location
    public void CreateFile(GameSave save) {
        string saveFilePath = Application.persistentDataPath + "/SaveFiles/";
        if (!Directory.Exists(saveFilePath)) {
            Directory.CreateDirectory(saveFilePath);
        }
        string filePath = saveFilePath + "/test.sav";
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
        formatter.Serialize(stream, save);
    }
}