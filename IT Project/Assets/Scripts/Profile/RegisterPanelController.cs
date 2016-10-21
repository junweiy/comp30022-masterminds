using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RegisterPanelController : MonoBehaviour {

    public InputField UserNameInput;
    public InputField EmailInput;
    public string UserName;
    public string Email;

	
	// Update is called once per frame
	void Update () {
        UserName = UserNameInput.text.ToString();
        Email = EmailInput.text.ToString();
	}
}
