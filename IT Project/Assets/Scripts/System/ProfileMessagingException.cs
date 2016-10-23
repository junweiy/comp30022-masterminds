using System;

// Exception for all errors regarding profile messaging with the server
public class ProfileMessagingException : Exception {
    public int Code { get; private set; }
    public string message { get; private set; }

    public ProfileMessagingException(int code, string message) {
        this.Code = code;
        this.message = message;
    }
}