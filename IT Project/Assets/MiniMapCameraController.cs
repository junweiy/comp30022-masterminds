﻿using UnityEngine;
using System.Collections;

public class MiniMapCameraController : MonoBehaviour
{

    void LateUpdate()
    {
        GetComponent<Camera>().transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
    }
}
