﻿using UnityEngine;
using System.Collections;

public class KeyboardMovement : Photon.MonoBehaviour {

    

	// Use this for initialization
	void Start () {
		
	}
	
//	// Update is called once per frame
//	void Update () {
//		//rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);
//		if (!photonView.isMine) {
//			return;
//		}
//
//        //Move front
//		if (Input.GetKey(KeyCode.W))
//        {
//			rb.AddForce (velocity * new Vector3(0,0,1), ForceMode.Acceleration);
//        }
//
//        //Move back
//        if (Input.GetKey("s"))
//        {
//			rb.AddForce (velocity * new Vector3(0,0,-1), ForceMode.Acceleration);
//        }
//
//        //Move left
//        if (Input.GetKey("a"))
//        {
//			rb.AddForce (velocity * new Vector3(-1,0,0), ForceMode.Acceleration);
//        }
//
//        //Move rignt
//        if (Input.GetKey("d"))
//        {
//			rb.AddForce (velocity * new Vector3(1,0,0), ForceMode.Acceleration);
//        }
//
//
//
//
//    }
}
