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
	
	}
	
	// Update is called once per frame
	void Update () {
		// TODO Keybord action for testing, shuold probably provide a feature to switch user properly
		if (Input.GetKeyDown (KeyCode.S) && mainMenuPage.activeSelf) {
			SwitchTo (loginPage);
		}
	}

	private void DisableAllPages() {
		mainMenuPage.SetActive(false);
		singleModePage.SetActive(false);
		multiModePage.SetActive(false);
		registerPage.SetActive(false);
		loginPage.SetActive(false);
	}

	private void SwitchTo(GameObject page) {
		DisableAllPages ();
		page.SetActive (true);
	}

	private void DisplayError(ProfileMessagingException exception) {
		alertPage.SetActive (true);
		alertPage.GetComponent<ErrorPageController> ().ShowAlert (exception.message);
	}

	public void RegisterSubmit() {
		string userName = registerUserNameField.text;
		string email = registerEmailField.text;
		try {
			int? uid = ProfileMessenger.createNewUser (userName, email);
			if (uid == null) {
				Debug.LogWarning ("register failed");
			} else {
				GlobalState.loadProfileWithUid ( (int) uid);
				gotoMainMenu ();
			}
		} catch (ProfileMessagingException e) {
			DisplayError (e);
		}
	}

	public void RegisterCancel() {
		SwitchTo (loginPage);
	}

	public void Login(GameObject emailTextField) {
		string email = emailTextField.GetComponent<Text> ().text;
		try {
			bool outcome = GlobalState.loadProfileWithEmail (email);
			if (outcome == false) {
				Debug.LogWarning ("login failed");
			} else {
				gotoMainMenu ();
			}
		} catch (ProfileMessagingException e) {
			DisplayError (e);
		}
	}

	public void gotoMainMenu() {
		SwitchTo (mainMenuPage);
	}

	public void gotoRegister() {
		SwitchTo (registerPage);
	}

	public void gotoProfile() {
		StateController.SwitchToProfile ();
	}

	public void gotoSingleModePage() {
		SwitchTo (singleModePage);
	}

	public void gotoMultiModePage() {
		SwitchTo (multiModePage);
	}

}
