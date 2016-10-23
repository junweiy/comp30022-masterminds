using UnityEngine;

public class ProfileHandler : Photon.MonoBehaviour {
    public const int MAINMENU_SCENE_NUMBER = 1;
    public const int MATCHING_SCENE_NUMBER = 2;
    public const int RESULT_SCENE_NUMBER = 4;

    public bool LoadedFromFile;
    public bool IsLogedIn;
    public string UserName;
    public int Kill;
    public int Death;
    public bool Won;
    public bool AlreadyUpdated;


    // Use this for initialization
    private void Start() {
        LoadedFromFile = false;
        IsLogedIn = false;
        AlreadyUpdated = true;
        DontDestroyOnLoad(this.gameObject);
		if (GameObjectFinder.FindAllProfileHandler().Length == 2) {
            Destroy(this.gameObject);
        }
    }

	// Update the profile handler with stats
    public void UpdateProfile(int kill, int death, bool won) {
        this.Kill = kill;
        this.Death = death;
        this.Won = won;
        this.AlreadyUpdated = false;
        Debug.Log("Updated with " + kill + " " + death);
    }

    public void ResetStats() {
        this.AlreadyUpdated = true;
        LoadedFromFile = false;
    }

    public void LoggedIn(string userName) {
        this.IsLogedIn = true;
        this.UserName = userName;
    }

    private void OnLevelWasLoaded(int level) {
        if (level == MAINMENU_SCENE_NUMBER) {
			// Skip login from other scenes if already logged in
            if (IsLogedIn) {
                GameObject.Find("Canvas").transform.FindChild("Login").gameObject.SetActive(false);
                GameObject.Find("Canvas").transform.FindChild("MainMenu").gameObject.SetActive(true);
            }
			// Update the profile to the server if not updated yet
            if (IsLogedIn && !AlreadyUpdated) {
                Profile oldProfile = ProfileMessenger.GetProfileByEmail(UserName);
                oldProfile.numGamesPlayed += 1;
                oldProfile.numDeath += Death;
                oldProfile.numPlayerKilled += Kill;
                if (Won) {
                    oldProfile.numGamesWon++;
                } else {
                    oldProfile.numGamesLost++;
                }
                ResetStats();
                ProfileMessenger.SubmitNewProfile(oldProfile);
            }
        }
		// Inform RandomMatchMaker if the player has chosen to load from save file
        if (level == MATCHING_SCENE_NUMBER) {
			RandomMatchmaker rm = GameObjectFinder.FindRandomMatchMaker ();
            rm.LoadedFromFile = LoadedFromFile;
        }

        if (level == RESULT_SCENE_NUMBER) {
			// Display stats in result scene
			ResultPageController rpc = GameObjectFinder.FindResultPageController ();
            rpc.IsWinner = Won;
            rpc.UserName = UserName;
            rpc.Kill = Kill;
            rpc.Death = Death;
        }
    }
}