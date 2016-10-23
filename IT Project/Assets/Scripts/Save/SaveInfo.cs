using System.Collections.Generic;
using System;

// Class of the metadata in a save
[System.Serializable]
public class SaveInfo {
    public List<string> Usernames;
    public string Name;
    public DateTime Time;
}