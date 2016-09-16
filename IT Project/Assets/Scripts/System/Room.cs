using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Room {
    
    // Name of the room
    public string roomName { set; get; }

    // List of player in the room
    public List<Player> playerList;

    // Player who host the room
    public Player host { set; get; }

    // Number of rounds, set by host
    public int roundNumber { set; get; }

    // Limitation of number of players in the room, set by host
    public int limitNumPlayer { set; get; }

    // Default number of players
    public const int maxNumPlayer = 8;

    //TODO game map list, character list

    // Initialise the room
    public Room(Player host, int limitNumPlayer, string roomName, int roundNumber)
    {
        this.host = host;
        this.playerList.Add(host);
        this.limitNumPlayer = limitNumPlayer;
        this.roomName = roomName;
        this.roundNumber = roundNumber;
    }

    // Add player to the room
    public void addPlayer(Player newPlayer)
    {
        this.playerList.Add(newPlayer);
    }

    // Remove player from the room
    public void removePlayer(Player player)
    {
        this.playerList.Remove(player);
    }




}
