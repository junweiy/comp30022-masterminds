package server;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.Socket;

public class Receiver extends Thread {

	protected Socket client;
	protected Player player;

	InputStream inp = null;
	BufferedReader brinp = null;

	Receiver(Socket clientSocket, Player p) {
		client = clientSocket;
		player = p;
		try {
			inp = clientSocket.getInputStream();
			brinp = new BufferedReader(new InputStreamReader(inp));
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}

	public void run() {
  		while (true) {
			try {
				String message = brinp.readLine();
				
				Room.handleMessage(message,player);
				
			} catch (Exception e) {
			}
		}

	}

}
