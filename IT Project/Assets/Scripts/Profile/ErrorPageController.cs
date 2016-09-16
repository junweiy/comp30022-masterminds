using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ErrorPageController : MonoBehaviour {

    public Text errorMessage;

    public void showAlert(string message)
    {
        errorMessage.text = message;
    }

}
