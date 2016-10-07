using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;
using System;
using UnityEngine;

public static class ReplayIO {
    public static string FolderPath = Application.dataPath + "/Replays/";

    public static void SaveReplayAs(string filepath, GameReplay replay) {
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(filepath, FileMode.Create, FileAccess.Write, FileShare.None);
        formatter.Serialize(stream, replay);
    }

    public static void SaveReplayWithTimeAsFilename(GameReplay replay) {
        DateTime now = DateTime.Now;
        string filename = string.Format("{0:HH_mm_dd_MM_yyyy}.rep", now);
        SaveReplayAs(FolderPath + filename, replay);
    }

    public static GameReplay LoadReplayFromFilepath(string filepath) {
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.Read);
        return (GameReplay)formatter.Deserialize(stream);
    }

    public static string[] GetReplayFilepaths() {
        return Directory.GetFiles(FolderPath, "*.rep");
    }
}
