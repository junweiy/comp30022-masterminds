﻿using UnityEngine;
using UnityEngine.UI;

// Controller for the login panel
public class LoginPanelController : MonoBehaviour {
    public InputField UserNameInput;
    public string UserName;

    // Update is called once per frame
    private void Update() {
        if (this.gameObject.activeInHierarchy) {
            UserName = UserNameInput.text.ToString();
        }
    }
}