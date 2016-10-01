using UnityEngine;
using System.Collections.Generic;

namespace Command
{
    public static class CommandReceiver
    {

        public static Queue<ICommand> receivedCommands = new Queue<ICommand>();

        public static Queue<ICommand> GetReceivedCommands()
        {

            return receivedCommands;
        }

        public static void Receive(string s)
        {
            string[] paras = s.Split(' ');
            switch (paras[0])
            {
                case "M":
                    receivedCommands.Enqueue(MoveCommand.Read(s));
                    break;
                case "N":
                    receivedCommands.Enqueue(new NoCommand(int.Parse(paras[1])));
                    break;
                default:
                    throw new System.Exception("Unknow Command");

            }

        }

    }
}
