using UnityEngine;
using System.Collections;

public class IgnoreRotation : MonoBehaviour {
    // Use this for initialization
    private void Start() {}

    // Update is called once per frame
    private void Update() {
        this.transform.rotation = Quaternion.identity;
    }
}