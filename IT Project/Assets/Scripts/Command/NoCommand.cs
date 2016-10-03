using UnityEngine;
using System.Collections;
using System;

namespace Command
{
	[System.Serializable]
    public class NoCommand : ICommand
    {
        private int pid;

        public NoCommand(int id)
        {
            pid = id;
        }

        public int GetFrame()
        {
            throw new NotImplementedException();
        }

        public void ProcessCommand()
        {
            /*no Command do nothing*/
        }

        public string Show()
        {
            return "N " + pid.ToString();
        }

        public CommandType GetType()
        {
            return CommandType.None;
        }
        
    }
}