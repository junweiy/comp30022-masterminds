using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using NetworkManager;

namespace Manager
{
    public static class GameManager
    {
        public static NetworkManager.Network network;
        
        public static void SetMainChar(string s)
        {
            PlayerManager.LocalCharacterID = int.Parse(s.Split(' ')[1]);
            network.Joined();
        }

        public static void GetReady()
        {
            ConnectionHandler.Send("Ready");
        }

        public static void Ready()
        {
            network.Ready();
        }

        public static void StartGame(string s)
        {
            string[] info = s.Split('.');


            int numPlayer = int.Parse(info[1]);
            int i;
            for (i = 0; i < numPlayer; i++)
            {
                string[] player = info[i + 2].Split(' ');
                PlayerManager.AddPlayer(new Player(int.Parse(player[0]), new Vector3(int.Parse(player[1]), int.Parse(player[2]), int.Parse(player[3]))));
            }

            network.StartGame();
        }

    }
}