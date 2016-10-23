using UnityEngine;
using UnityEngine.UI;

// Controller of the error page, displaying error from the server
public class ErrorPageController : MonoBehaviour {
    public Text ErrorMessage;

    public void ShowAlert(string message) {
        ErrorMessage.text = message;
    }
}