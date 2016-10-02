package server;

import java.util.*;

import server.Player.PlayerState;

public class Room {
	
	private static ArrayList<Player> players = new ArrayList<Player>();
	
	private static ArrayList<Player> temp = new ArrayList<Player>();
	private static int tempNum;
	
	public static int turn = 0;
	
	public static int numReady;
	public static int numPlayer;
	
	public static LinkedList<String> queue = new LinkedList<String>();
	
	public enum RoomState{
		Started,
		Waiting,
	}
	
	public static RoomState state = RoomState.Waiting;

	public static void addPlayer(Player newPlayer){
		numPlayer++;
		players.add(newPlayer);
		newPlayer.send("J "+Integer.toString(newPlayer.id));
		
	}

	public static ArrayList<Player> getPlayers(){
		
		return players;
		
	}
	
	
	public static String StartInfo(){
		String startState = "S." ;
		// first number is size
		startState += Integer.toString(players.size())+".";

		// middle are player list 
		for (Player p : players){
			startState += p.startInfo()+".";
		}
		return startState;
		
	}
	
	public static void syncCommand(String s){
		for (Player p : players){
			p.send(s);
		}
	}
	
	public static void handleMessage(String message, Player from){
		System.out.println(message);

		if(state==RoomState.Started){
			queue.addLast(message);
			if(!temp.contains(from)){
				temp.add(from);
				tempNum ++;
				if(tempNum == numPlayer){
					for(String cmd : queue){
						syncCommand(cmd);
					}
					temp.clear();
					queue.clear();
					tempNum = 0;
					turn ++;
				}
			}
			
		}else{
			
			if(message.equals("Ready")){
				from.send("Ready");
				from.state = PlayerState.Ready;
				numReady++;
				
				if(numReady>=2 && numReady == numPlayer){
					state = RoomState.Started;
					syncCommand(StartInfo());
					syncCommand("N 0");
				}
				
			}
		}
		
	}
	
}
