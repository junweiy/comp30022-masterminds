using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

	public GameObject registerUserNameField;
	public GameObject registerEmailField;

	public GameObject mainMenuPage;
	public GameObject singleModePage;
	public GameObject multiModePage;
	public GameObject registerPage;
	public GameObject loginPage;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
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

	public void registerSubmit() {
		string userName = registerUserNameField.GetComponent<Text> ().text;
		string email = registerEmailField.GetComponent<Text> ().text;
		int? uid = ProfileMessenger.createNewUser (userName, email);
		if (uid == null) {
			Debug.LogWarning ("register failed");
		} else {
			GlobalState.loadProfileWithUid ( (int) uid);
			gotoMainMenu ();
		}
	}

	public void registerCancel() {
		switchTo (loginPage);
	}

	public void login(GameObject emailTextField) {
		string email = emailTextField.GetComponent<Text> ().text;
		bool outcome = GlobalState.loadProfileWithEmail (email);
		if (outcome == false) {
			Debug.LogWarning ("login failed");
		} else {
			gotoMainMenu ();
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

	public void gotoSingleModePage() {
		switchTo (singleModePage);
	}

	public void gotoMultiModePage() {
		switchTo (multiModePage);
	}

}
