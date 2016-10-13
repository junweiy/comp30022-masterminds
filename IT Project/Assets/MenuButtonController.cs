﻿
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuButtonController : Photon.MonoBehaviour {

    public GameObject menuPanel;
    public GameObject menuButton;
    public GameObject pauseButton;
    public GameObject mainMenuButton;
	public GameObject joyStick;
	public GameObject spellButton;
    public GameObject pauseMessage;

	public void MenuButtonOnClick()
    {
        if (menuPanel.activeInHierarchy)
        {
            menuPanel.SetActive(false);
        }
        else
        {
            menuPanel.SetActive(true);
        }
    }

    public void MainMenuButtonOnClick()
    {
        StateController.SwitchToMainMenu();
		PhotonNetwork.Disconnect ();
    }

	[PunRPC]
	void Pause() {
		joyStick.SetActive(false);
		spellButton.SetActive (false);
		pauseButton.GetComponentInChildren<Text>().text = "Continue";
		pauseMessage.SetActive(true);
		Time.timeScale = 0;
	}

	[PunRPC]
	void Continue() {
		joyStick.SetActive(true);
		spellButton.SetActive (true);
		pauseButton.GetComponentInChildren<Text>().text = "Pause";
		pauseMessage.SetActive(false);
		Time.timeScale = 1;
	}


	public void PauseButtonOnClick() {
		
		if (Time.timeScale > 0) {
			photonView.RPC ("Pause", PhotonTargets.All);
		} else {
			photonView.RPC ("Continue", PhotonTargets.All);
		}
	}

    //void Update()
    //{
    //    if (menuPanel.activeInHierarchy)
    //    {
    //        if (EventSystem.current.gameObject != pauseButton && EventSystem.current.gameObject != mainMenuButton)
    //        {
    //            //menuPanel.SetActive(false);
    //        }

    //    }
    //}
}
