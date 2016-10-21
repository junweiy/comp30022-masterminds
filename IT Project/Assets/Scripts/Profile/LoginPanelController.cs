using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoginPanelController : MonoBehaviour {

    public InputField UserNameInput;
    public string UserName;
	
	// Update is called once per frame
	void Update () {
        if (this.gameObject.activeInHierarchy)
        {
            UserName = UserNameInput.text.ToString();
        }

	}
}
