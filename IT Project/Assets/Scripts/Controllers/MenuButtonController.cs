using UnityEngine;
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
	// Cool down time for saving game state
    private int _saveCoolDown = 5;
	// Current cool down time
    private float _currentCoolDown = 0;

    public void MenuButtonOnClick() {
        if (MenuPanel.activeInHierarchy) {
            MenuPanel.SetActive(false);
        } else {
            MenuPanel.SetActive(true);
        }
    }

	// Switch to main menu
    public void MainMenuButtonOnClick() {
        StateController.SwitchToMainMenu();
        PhotonNetwork.Disconnect();
    }

	// Save the game
    public void SaveButtonOnClick() {
        _currentCoolDown = _saveCoolDown;
		GameSaver gs = GameObjectFinder.FindGameSaver ();
        gs.Save();
    }

	// Handle pause function on all clients
    public void PauseButtonOnClick() {
        if (Time.timeScale > 0) {
            photonView.RPC("Pause", PhotonTargets.All);
        } else {
            photonView.RPC("Continue", PhotonTargets.All);
        }
    }

	// Disable UI and display pause message on all clients
    [PunRPC]
    private void Pause() {
        JoyStick.SetActive(false);
        SpellButton.SetActive(false);
        PauseButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("Icons/Continue");
        PauseMessage.SetActive(true);
        Time.timeScale = 0;
    }

	// Resume game on all clients
    [PunRPC]
    private void Continue() {
        JoyStick.SetActive(true);
        SpellButton.SetActive(true);
        PauseButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("Icons/Pause");
        PauseMessage.SetActive(false);
        Time.timeScale = 1;
    }

	// Handle the cool down of saving function
    private void Update() {
        Color temp = SavedImage.color;
        temp.a = _currentCoolDown/_saveCoolDown;
        SavedImage.color = temp;
        if (_currentCoolDown >= 0) {
            _currentCoolDown -= Time.deltaTime;
        } else {
            _currentCoolDown = 0;
        }
    }
		
}