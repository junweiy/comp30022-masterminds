using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoginPanelController : MonoBehaviour {

    public InputField userName_input;
    public string userName;
	
	// Update is called once per frame
	void Update () {
        if (this.gameObject.activeInHierarchy)
        {
            userName = userName_input.text.ToString();
        }

	}
}
