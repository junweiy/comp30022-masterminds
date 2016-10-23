using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;
using System;
using UnityEngine;

// Static class containing methods handling input/output of replay files
public static class ReplayIO {
    // The default folder path of replay files
    public static string FolderPath = Application.persistentDataPath + "/Replays/";

    // Saves the replay with the specified file path
    public static void SaveReplayAs(string filepath, GameReplay replay) {
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(filepath, FileMode.Create, FileAccess.Write, FileShare.None);
        formatter.Serialize(stream, replay);
    }

    // Saves the replay with current time as the file name in the default folder
    public static void SaveReplayWithTimeAsFilename(GameReplay replay) {
        DateTime now = DateTime.Now;
        string filename = string.Format("{0:HH_mm_dd_MM_yyyy}.rep", now);
        if (!Directory.Exists(FolderPath)) {
            Directory.CreateDirectory(FolderPath);
        }
        SaveReplayAs(FolderPath + filename, replay);
		Debug.Log ("Saved in " + FolderPath);
    }

    // Loads replay with a given file path
    public static GameReplay LoadReplayFromFilepath(string filepath) {
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.Read);
        return (GameReplay) formatter.Deserialize(stream);
    }

    // Gets an array of replay files in the default folder
    public static string[] GetReplayFilepaths() {
        if (!Directory.Exists(FolderPath)) {
            Directory.CreateDirectory(FolderPath);
        }
        return Directory.GetFiles(FolderPath, "*.rep");
    }
}