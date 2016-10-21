using UnityEngine;
using UnityEngine.UI;

public class ErrorPageController : MonoBehaviour {
    public Text ErrorMessage;

    public void ShowAlert(string message) {
        ErrorMessage.text = message;
    }
}