using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoginPanelController : MonoBehaviour {

    public Text userName_input;
    public string userName;
	
	// Update is called once per frame
	void Update () {
        userName = userName_input.text.ToString();
        Debug.Log(userName);
	}
}
