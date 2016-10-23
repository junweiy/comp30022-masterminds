using UnityEngine;
using UnityEngine.UI;

// Controller for the main menu
public class MainMenuController : MonoBehaviour {
    public InputField RegisterUserNameField;
    public InputField RegisterEmailField;

    public GameObject MainMenuPage;
    public GameObject RegisterPage;
    public GameObject LoginPage;
    public GameObject AlertPage;

    // Use this for initialization
    private void Start() {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }

    private void DisableAllPages() {
        MainMenuPage.SetActive(false);
        RegisterPage.SetActive(false);
        LoginPage.SetActive(false);
    }

    private void SwitchTo(GameObject page) {
        DisableAllPages();
        page.SetActive(true);
    }

    private void DisplayError(ProfileMessagingException exception) {
        AlertPage.SetActive(true);
        AlertPage.GetComponent<ErrorPageController>().ShowAlert(exception.message);
    }

    public void RegisterSubmit() {
        string userName = RegisterUserNameField.text;
        string email = RegisterEmailField.text;
        try {
            int? uid = ProfileMessenger.CreateNewUser(userName, email);
            if (uid == null) {
                Debug.LogWarning("register failed");
            } else {
                GlobalState.LoadProfileWithUid((int) uid);
				GameObjectFinder.FindProfileHandler().LoggedIn(email);
                GoToMainMenu();
            }
        } catch (ProfileMessagingException e) {
            DisplayError(e);
        }
    }

    public void RegisterCancel() {
        SwitchTo(LoginPage);
    }

    public void Login(GameObject emailTextField) {
        string email = emailTextField.GetComponent<Text>().text;
        try {
            bool outcome = GlobalState.LoadProfileWithEmail(email);
            if (outcome == false) {
                Debug.LogWarning("login failed");
            } else {
				GameObjectFinder.FindProfileHandler().LoggedIn(email);
                GoToMainMenu();
            }
        } catch (ProfileMessagingException e) {
            DisplayError(e);
        }
    }

    public void GoToMainMenu() {
        SwitchTo(MainMenuPage);
    }

    public void GoToRegister() {
        SwitchTo(RegisterPage);
    }

    public void GoToProfile() {
        StateController.SwitchToProfile();
    }

    public void GoToReplay() {
        StateController.SwitchToReplaySelection();
    }
		

    // for multiPlayer button
    public void GoToQuickMatch() {
		ProfileHandler ph = GameObjectFinder.FindProfileHandler();
        ph.LoadedFromFile = false;
        StateController.SwitchToMatching();
    }

    // for load button
    public void LoadGame() {
		ProfileHandler ph = GameObjectFinder.FindProfileHandler();
        ph.LoadedFromFile = true;
        StateController.SwitchToMatching();
    }
}