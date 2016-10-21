using UnityEngine;
using System.Collections;

public class SpeakerIconController : MonoBehaviour {

    public GameObject SpeakerIcon;

    public static GameObject FindMainPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Character");
        foreach (GameObject player in players)
        {
            if (player.GetPhotonView().isMine)
            {
                return player;
            }
        }
        throw new UnityException();

    }

    public static T GetMainPlayerController<T>()
    {
        GameObject mainPlayer = FindMainPlayer();
        return mainPlayer.GetComponent<T>();
    }
}
