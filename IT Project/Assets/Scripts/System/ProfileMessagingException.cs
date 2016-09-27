using UnityEngine;
using System.Collections;
using System;

public class ProfileMessagingException : Exception {
	public int code {get; private set;}
	public string message {get; private set;}

	public ProfileMessagingException(int code, string message) {
		this.code = code;
		this.message = message;
	}

}
