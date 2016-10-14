using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

	public InputField registerUserNameField;
	public InputField registerEmailField;

	public GameObject mainMenuPage;
	public GameObject singleModePage;
	public GameObject multiModePage;
	public GameObject registerPage;
	public GameObject loginPage;
	public GameObject alertPage;

	// Use this for initialization
	void Start () {
		Screen.orientation = ScreenOrientation.LandscapeLeft;
	}
	
	// Update is called once per frame
	void Update () {
		// TODO Keybord action for testing, shuold probably provide a feature to switch user properly
		if (Input.GetKeyDown (KeyCode.S) && mainMenuPage.activeSelf) {
			switchTo (loginPage);
		}
	}

	private void DisableAllPages() {
		mainMenuPage.SetActive(false);
		singleModePage.SetActive(false);
		multiModePage.SetActive(false);
		registerPage.SetActive(false);
		loginPage.SetActive(false);
	}

	private void switchTo(GameObject page) {
		DisableAllPages ();
		page.SetActive (true);
	}

	private void displayError(ProfileMessagingException exception) {
		alertPage.SetActive (true);
		alertPage.GetComponent<ErrorPageController> ().showAlert (exception.message);
	}

	public void registerSubmit() {
		string userName = registerUserNameField.text;
		string email = registerEmailField.text;
		try {
			int? uid = ProfileMessenger.createNewUser (userName, email);
			if (uid == null) {
				Debug.LogWarning ("register failed");
			} else {
				GlobalState.loadProfileWithUid ( (int) uid);
				GameObject.FindGameObjectWithTag("ProfileHandler").GetComponent<ProfileHandler>().LoggedIn(email);
				gotoMainMenu ();
			}
		} catch (ProfileMessagingException e) {
			displayError (e);
		}
	}

	public void registerCancel() {
		switchTo (loginPage);
	}

	public void login(GameObject emailTextField) {
		string email = emailTextField.GetComponent<Text> ().text;
		try {
			bool outcome = GlobalState.loadProfileWithEmail (email);
			if (outcome == false) {
				Debug.LogWarning ("login failed");
			} else {
				GameObject.FindGameObjectWithTag("ProfileHandler").GetComponent<ProfileHandler>().LoggedIn(email);
				gotoMainMenu ();
			}
		} catch (ProfileMessagingException e) {
			displayError (e);
		}
	}

	public void gotoMainMenu() {
		switchTo (mainMenuPage);
	}

	public void gotoRegister() {
		switchTo (registerPage);
	}

	public void gotoProfile() {
		StateController.switchToProfile ();
	}

    public void gotoreplay() {
        StateController.SwitchToReplaySelection();
    }


    public void gotoSingleModePage() {
		switchTo (singleModePage);
	}

	public void gotoMultiModePage() {
		StateController.SwitchToMatching ();
	}

}
