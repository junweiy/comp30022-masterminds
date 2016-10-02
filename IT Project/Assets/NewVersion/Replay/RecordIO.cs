﻿using System.Collections.Generic;
using Command;
using UnityEngine;
using Manager;

namespace Replay
{
    public static class RecordIO
    {

        public static void GenerateRecord(string path, Queue<ICommand> records,string startState)
        {
            string s = startState+"\n";
            while (records.Count > 0)
            {
                s += records.Dequeue().Show()+"\n";
            }
            System.IO.File.WriteAllText(path, s);
        }

        public static Queue<ICommand> LoadRecord(string path)
        {
            bool loadStart = false;
            string all = System.IO.File.ReadAllText(path);
            Queue<ICommand> record = new Queue<ICommand>();
            foreach (string s in all.Split('\n'))
            {
                if (!loadStart)
                {
                    GameManager.ParseStartInfo(s);
                    loadStart = true;
                    continue;
                }

                if (!s.Equals("")) {
                    record.Enqueue(MoveCommand.Read(s));
                }
            }
            return record;
        }
    }
}
