﻿using UnityEngine;
using UnityEngine.UI;

public class StartButtonUIController : MonoBehaviour {
    private static float _coolDown = 0.5f;
    private float _currentCoolDown = 0.5f;
    private Image _startImage;
    private bool _increaseCoolDown = false;

    // Use this for initialization
    private void Start() {
        _startImage = this.GetComponent<Image>();
		Screen.orientation = ScreenOrientation.LandscapeLeft;
    }

	// Update on effects to allow start button effect on welcome page
    private void Update() {
        Color temp = _startImage.color;
        temp.a = _currentCoolDown/_coolDown;
        _startImage.color = temp;
        if (_currentCoolDown > 0 && !_increaseCoolDown) {
            _currentCoolDown -= Time.deltaTime;
        } else if (_currentCoolDown <= 0 && !_increaseCoolDown) {
            _currentCoolDown += Time.deltaTime;
            _increaseCoolDown = true;
        } else if (_currentCoolDown < _coolDown + 0.3f && _increaseCoolDown) {
            _currentCoolDown += Time.deltaTime;
        } else {
            _currentCoolDown -= Time.deltaTime;
            _increaseCoolDown = false;
        }
    }

    public void ToMainMenu() {
        StateController.SwitchToMainMenu();
    }
}