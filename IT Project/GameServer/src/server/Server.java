package server;


import java.io.IOException;
import java.net.*;

public class Server {	
	
	public static void main(String[] args) throws Exception {
				
		ServerSocket ss = null;
		Socket client = null;
		int i = 0;
		try {
			ss = new ServerSocket(9878);
		} catch (IOException e) {
			System.out.println("Error Listenning on port 9877");
			return;
		}
		try {
			System.out.println(ss.getInetAddress());
			/* connect with each client */
			while((client = ss.accept())!=null){
				System.out.println("Connection .. "+client.getInetAddress());
				Player newPlayer = new Player(client,i++);
				Room.addPlayer(newPlayer);
			}
			
		} catch (IOException e) {
			e.printStackTrace();
		}
		ss.close();
		
	}
	


}
