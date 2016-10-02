package server;

import java.io.DataOutputStream;
import java.io.IOException;

import java.net.Socket;

public class Sender {

	DataOutputStream out = null;
	
	Socket client;
	
	Sender(Socket clientSocket) {
		client = clientSocket;
		try {
			out = new DataOutputStream(clientSocket.getOutputStream());
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}
	
	public void send(String s) {
		try {
			System.out.println(s+" send");
			out.writeBytes(s+"\n");
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}
	
}
