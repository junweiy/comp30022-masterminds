using System.Collections.Generic;

[System.Serializable]
public class GameReplay {

    [System.Serializable]
    public class Entry {
        public int FrameTime;
        public IRecord Record;
    }

    public Queue<Entry> Entries;
    public ReplayInfo Info;

}
