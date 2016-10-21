using System;

public class ProfileMessagingException : Exception {
    public int Code { get; private set; }
    public string message { get; private set; }

    public ProfileMessagingException(int code, string message) {
        this.Code = code;
        this.message = message;
    }
}