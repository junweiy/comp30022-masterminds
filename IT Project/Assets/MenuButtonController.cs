using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuButtonController : Photon.MonoBehaviour {

    public GameObject menuPanel;
    public GameObject menuButton;
    public GameObject pauseButton;
    public GameObject mainMenuButton;
    public Image savedImage;
    private int saveCoolDown = 5;
    private float currentCoolDown = 0;

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

    public void SaveButtonOnClick()
    {
        currentCoolDown = saveCoolDown;
		GameSaver gs = GameObject.FindGameObjectWithTag ("Saver").GetComponent<GameSaver> ();
		gs.Save ();
    }


    void Update()
    {
        Color temp = savedImage.color;
        temp.a = currentCoolDown / saveCoolDown;
        savedImage.color = temp;
        if (currentCoolDown >= 0)
        {
            currentCoolDown -= Time.deltaTime;
        }
        else
        {
            currentCoolDown = 0;
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
