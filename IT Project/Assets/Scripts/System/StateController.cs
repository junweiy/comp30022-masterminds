using UnityEngine.SceneManagement;

public static class StateController {
    public static void SwitchToGamePlay() {
        SceneManager.LoadScene("scenes/gameplay");
    }

    public static void SwitchToResult() {
        SceneManager.LoadScene("scenes/result");
    }

    public static void SwitchToShop() {
        SceneManager.LoadScene("scenes/shop");
    }

    public static void SwitchToRoom() {
        SceneManager.LoadScene("scenes/room");
    }

    public static void SwitchToProfile() {
        SceneManager.LoadScene("scenes/Profile");
    }

    public static void SwitchToMainMenu() {
        SceneManager.LoadScene("scenes/MainMenu");
    }

    public static void SwitchToMatching() {
        SceneManager.LoadScene("scenes/Matching");
    }

    public static void SwitchToReplayScene() {
        SceneManager.LoadScene("scenes/replayScene");
    }

    public static void SwitchToReplaySelection() {
        SceneManager.LoadScene("scenes/ReplaySelection");
    }
}