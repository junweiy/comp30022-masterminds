using UnityEngine;

public class MiniMapController : MonoBehaviour {
    private void Start() {
		GameObject[] players = GameObjectFinder.FindAllCharacters();
        foreach (GameObject player in players) {
            Renderer miniMapDot = player.transform.GetChild(4).gameObject.GetComponent<Renderer>();
            if (player.GetPhotonView().isMine) {
                miniMapDot.material = Resources.Load<Material>("Materials/Blue");
                break;
            }
        }
    }
}