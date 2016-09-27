using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RegisterPanelController : MonoBehaviour {

    public InputField userName_input;
    public InputField email_input;
    public string userName;
    public string email;

	
	// Update is called once per frame
	void Update () {
        userName = userName_input.text.ToString();
        email = email_input.text.ToString();
	}
}
