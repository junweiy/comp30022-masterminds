using UnityEngine;

public class CameraControl : MonoBehaviour {
    // The target that the camera will follow
    public Transform MTarget;
    private Quaternion _rotation;

    private void Awake() {
        this.transform.position = MTarget.position;
        _rotation = transform.rotation;
    }

    // Fixed update will sync the position
    private void FixedUpdate() {
        this.transform.position = MTarget.position;
    }

    private void LateUpdate() {
        transform.rotation = _rotation;
    }
}