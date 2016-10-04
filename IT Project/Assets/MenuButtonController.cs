using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MenuButtonController : MonoBehaviour {

    public GameObject menuPanel;
    public GameObject menuButton;
    public GameObject pauseButton;
    public GameObject mainMenuButton;

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
