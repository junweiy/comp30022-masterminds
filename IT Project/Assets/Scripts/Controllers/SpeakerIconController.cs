using UnityEngine;

public class SpeakerIconController : MonoBehaviour {
    public GameObject SpeakerIcon;

    public static GameObject FindMainPlayer() {
		GameObject[] players = GameObjectFinder.FindAllCharacters ();
        foreach (GameObject player in players) {
            if (player.GetPhotonView().isMine) {
                return player;
            }
        }
        throw new UnityException();
    }

    public static T GetMainPlayerController<T>() {
        GameObject mainPlayer = FindMainPlayer();
        return mainPlayer.GetComponent<T>();
    }
}