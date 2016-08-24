using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {
    
    // The target that the camera will follow
    public Transform m_Target; 


    private void Awake()
    {
        this.transform.position = m_Target.position;
    }

    // Fixed update will sync the position
    private void FixedUpdate()
    {

        this.transform.position = m_Target.position;
    }
    

}
