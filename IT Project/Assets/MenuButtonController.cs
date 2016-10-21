using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuButtonController : Photon.MonoBehaviour {

    public GameObject MenuPanel;
    public GameObject MenuButton;
    public GameObject PauseButton;
    public GameObject MainMenuButton;
	public GameObject JoyStick;
	public GameObject SpellButton;
	public GameObject PauseMessage;
    public Image SavedImage;
    private int _saveCoolDown = 5;
    private float _currentCoolDown = 0;

	public void MenuButtonOnClick()
    {
        if (MenuPanel.activeInHierarchy)
        {
            MenuPanel.SetActive(false);
        }
        else
        {
            MenuPanel.SetActive(true);
        }
    }

    public void MainMenuButtonOnClick()
    {
        StateController.SwitchToMainMenu();
		PhotonNetwork.Disconnect ();
    }

    public void SaveButtonOnClick()
    {
        _currentCoolDown = _saveCoolDown;
		GameSaver gs = GameObject.FindGameObjectWithTag ("Saver").GetComponent<GameSaver> ();
		gs.Save ();
    }

	public void PauseButtonOnClick() {

		if (Time.timeScale > 0) {
			photonView.RPC ("Pause", PhotonTargets.All);
		} else {
			photonView.RPC ("Continue", PhotonTargets.All);
		}
	}

	[PunRPC]
	private void Pause() {
		JoyStick.SetActive(false);
		SpellButton.SetActive (false);
        PauseButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("Icons/Continue");
		PauseMessage.SetActive(true);
		Time.timeScale = 0;
	}

	[PunRPC]
	private void Continue() {
		JoyStick.SetActive(true);
		SpellButton.SetActive (true);
        PauseButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("Icons/Pause");
		PauseMessage.SetActive(false);
		Time.timeScale = 1;
	}


    private void Update()
    {
        Color temp = SavedImage.color;
        temp.a = _currentCoolDown / _saveCoolDown;
        SavedImage.color = temp;
        if (_currentCoolDown >= 0)
        {
            _currentCoolDown -= Time.deltaTime;
        }
        else
        {
            _currentCoolDown = 0;
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
