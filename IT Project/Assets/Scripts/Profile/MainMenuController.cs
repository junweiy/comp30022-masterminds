using UnityEngine;
using UnityEngine.UI;

// Controller for the main menu
public class MainMenuController : MonoBehaviour {
    public InputField RegisterUserNameField;
    public InputField RegisterEmailField;

    public GameObject MainMenuPage;
    public GameObject SingleModePage;
    public GameObject MultiModePage;
    public GameObject RegisterPage;
    public GameObject LoginPage;
    public GameObject AlertPage;

    // Use this for initialization
    private void Start() {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }

    // Update is called once per frame
    private void Update() {}

    private void DisableAllPages() {
        MainMenuPage.SetActive(false);
        //SingleModePage.SetActive(false);
        //MultiModePage.SetActive(false);
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
                GameObject.FindGameObjectWithTag("ProfileHandler").GetComponent<ProfileHandler>().LoggedIn(email);
                GotoMainMenu();
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
                GameObject.FindGameObjectWithTag("ProfileHandler").GetComponent<ProfileHandler>().LoggedIn(email);
                GotoMainMenu();
            }
        } catch (ProfileMessagingException e) {
            DisplayError(e);
        }
    }

    public void GotoMainMenu() {
        SwitchTo(MainMenuPage);
    }

    public void GotoRegister() {
        SwitchTo(RegisterPage);
    }

    public void GotoProfile() {
        StateController.SwitchToProfile();
    }

    public void Gotoreplay() {
        StateController.SwitchToReplaySelection();
    }


    public void GotoSingleModePage() {
        SwitchTo(SingleModePage);
    }

    // for multiPlayer button
    public void GotoMultiModePage() {
        ProfileHandler ph = GameObject.FindGameObjectWithTag("ProfileHandler").GetComponent<ProfileHandler>();
        ph.LoadedFromFile = false;
        StateController.SwitchToMatching();
    }

    // for load button
    public void LoadGame() {
        ProfileHandler ph = GameObject.FindGameObjectWithTag("ProfileHandler").GetComponent<ProfileHandler>();
        ph.LoadedFromFile = true;
        StateController.SwitchToMatching();
    }
}