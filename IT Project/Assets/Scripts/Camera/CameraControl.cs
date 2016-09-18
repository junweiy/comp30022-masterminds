using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {
    
    // The target that the camera will follow
    public Transform m_Target; 
	Quaternion rotation;

    private void Awake()
    {
        this.transform.position = m_Target.position;
		rotation = transform.rotation;
    }

    // Fixed update will sync the position
    private void FixedUpdate()
    {

        this.transform.position = m_Target.position;
    }

	void LateUpdate() {
		transform.rotation = rotation;
	}
    

}
