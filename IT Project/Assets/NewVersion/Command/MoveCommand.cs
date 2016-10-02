using UnityEngine;
using System.Collections;
using Manager;

namespace Command
{ 
    [System.Serializable]
    public class MoveCommand : ICommand
    {

        private Vector3 destination;
        private int pid;
        private int FrameNumber;

        public MoveCommand(int frameNumber, int pid, Vector3 dest)
        {
            this.pid = pid;
            this.destination = dest;
            this.FrameNumber = frameNumber;
        }

        public void ProcessCommand()
        {
            PlayerManager.GetPlayerByID(pid).Move(destination);
        }

        public string Show()
        {
            string s = "M " + FrameNumber.ToString() + " " + pid.ToString() + " " + destination.x.ToString() + " " + destination.y.ToString() + " " + destination.z.ToString();
            return s;
        }

        public static MoveCommand Read(string s)
        {
            string[] paras = s.Split();
            int frameNumber = int.Parse(paras[1]);
            int pid = int.Parse(paras[2]);
            float x = float.Parse(paras[3]);
            float y = float.Parse(paras[4]);
            float z = float.Parse(paras[5]);
            return new MoveCommand(frameNumber, pid, new Vector3(x, y, z));
        }

        public int GetFrame()
        {
            return FrameNumber;
        }

        public CommandType GetType()
        {
            return CommandType.Move;
        }

    }
}