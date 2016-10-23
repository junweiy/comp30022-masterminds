﻿using UnityEngine;
using UnityEngine.UI;

// Controller for the register panel
public class RegisterPanelController : MonoBehaviour {
    public InputField UserNameInput;
    public InputField EmailInput;
    public string UserName;
    public string Email;


    // Update is called once per frame
    private void Update() {
        UserName = UserNameInput.text.ToString();
        Email = EmailInput.text.ToString();
    }
}