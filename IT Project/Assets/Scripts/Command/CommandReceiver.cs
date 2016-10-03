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
			    //Case "M" for move
                case "M":
                    receivedCommands.Enqueue(MoveCommand.Read(s));
                    break;

				//Case "N" for no action
                case "N":
                    receivedCommands.Enqueue(new NoCommand(int.Parse(paras[1])));
                    break;


				//Case "P" for pause


				//Case "T" for txt message


				//Case "C" for continue

                default:
                    throw new System.Exception("Unknow Command");

            }

        }

    }
}
