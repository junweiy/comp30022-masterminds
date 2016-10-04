﻿using System.Collections.Generic;

[System.Serializable]
public class GameReplay {

    [System.Serializable]
    public class Entry {
        public int frameTime;
        public ReplayRecord record;
    }

    public Queue<Entry> entries;
    public ReplayInfo info;

}