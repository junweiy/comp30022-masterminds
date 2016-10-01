using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using NetworkManager;

namespace Manager
{
    public static class GameManager
    {
        public static NetworkManager.Network n;

        public static void ReceiveStart(string s)
        {
            
            string[] lis = s.Split('.');
            int numPlayer = int.Parse(lis[0]);
            int i;
            for (i = 0; i < numPlayer; i++)
            {
                string[] info = lis[i + 1].Split(' ');

                PlayerManager.AddPlayer(new Player(int.Parse(info[0]),new Vector3(int.Parse(info[1]), int.Parse(info[2]),int.Parse(info[3]))));
            }

            SceneManager.LoadScene("newversion/Game");

        }

        public static void SetMainChar(string s)
        {
            PlayerManager.LocalCharacterID = int.Parse(s.Split(' ')[1]);
            n.Joined();
        }

    }
}