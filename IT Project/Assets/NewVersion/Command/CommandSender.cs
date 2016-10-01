using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NetworkManager;

using Manager;

namespace Command
{
    public static class CommandSender
    {

        public static Queue<ICommand> lastLockedCommand = new Queue<ICommand>();

        public static void SendLastLockedCommands()
        {
            if (lastLockedCommand.Count == 0)
            {
                ConnectionHandler.Send(new NoCommand(PlayerManager.LocalCharacterID).Show());
            }
            else
            {
                while (lastLockedCommand.Count != 0)
                {
                    ConnectionHandler.Send(lastLockedCommand.Dequeue().Show());
                }
            }
        }

        public static void AddCommand(ICommand a)
        {
            lastLockedCommand.Enqueue(a);
        }

    }
}