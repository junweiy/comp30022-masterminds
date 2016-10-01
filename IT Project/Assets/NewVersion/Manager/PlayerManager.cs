using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace Manager
{
    public static class PlayerManager
    {

        private static List<Player> players = new List<Player>();
        public static int LocalCharacterID { get; set; }

        public static void AddPlayer(Player p)
        {
            foreach (Player ps in players)
            {
                if (ps.id == p.id)
                {
                    throw new System.Exception("Player Existed");
                }
            }
            players.Add(p);
        }

        public static Player getPlayerByID(int id)
        {
            foreach (Player p in players)
            {
                if (p.id == id)
                {
                    return p;
                }
            }
            throw new System.Exception("Player not found");
        }
    }
}
