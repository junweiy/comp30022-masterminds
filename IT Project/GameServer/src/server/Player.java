package server;

import java.net.*;

public class Player {

	private Receiver receive;
	private Sender send;
	
	public int id;
	
	/* these are just spawn position */
	public int x;
	public int y;
	public int z;
	
	
	
	
	public enum PlayerState {
		InGame,
		Unready,
		Ready
	}
    
	public PlayerState state;
	
	
	
	public Player(Socket clientSocket,int pid) {
		receive = new Receiver(clientSocket, this);
		receive.start();
		send = new Sender(clientSocket);	
		this.id = pid;
		this.x = id*2;
		this.y = 1;
		this.z = id*2;
	}
	
	
	public void send(String s){
		send.send(s);
	}
	
    public String startInfo(){
    	String s = "";
    	s+=Integer.toString(id)+" "+Integer.toString(x)+" "+Integer.toString(y)+" "+Integer.toString(z)+" ";
    	return s;
    }
	
}
