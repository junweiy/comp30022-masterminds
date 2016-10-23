using UnityEngine;

public class MiniMapCameraController : MonoBehaviour {
    private void LateUpdate() {
        GetComponent<Camera>().transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
    }
}