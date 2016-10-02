﻿using System.Collections.Generic;
using Command;

namespace Replay
{
    public static class Recording
    {

        private static Queue<ICommand> record = new Queue<ICommand>();
        private static string startState;

        public static void Record(ICommand a)
        {
            record.Enqueue(a);
        }

        public static void Finish(string path)
        {
            RecordIO.GenerateRecord(path, record, startState);
        }

        public static void Load(string path)
        {
            record = RecordIO.LoadRecord(path);
        }

        public static int GetCurrentFrame()
        {
            return record.Peek().GetFrame();
        }

        public static bool IsFinished()
        {
            return record.Count == 0;
        }

        public static ICommand Next()
        {
            return record.Dequeue();
        }

        public static void RecordStart(string s)
        {
            startState = s;
        }
        
    }
}
