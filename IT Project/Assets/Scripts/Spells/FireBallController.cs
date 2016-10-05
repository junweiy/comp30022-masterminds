﻿using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class FireBallController : Photon.MonoBehaviour {
	// The tag of the character
	public const string CHARACTER_TAG = "Character";
	// The velocity of fire ball
	public const float VELOCITY = 300;
	// The range that can be chosen to cast within
	private const float RANGE = 600;

	// Character ID that cast the spell
	public int charID;

	public int damage;

	public float distanceTravelled;

	void Start() {
		this.GetComponent<Rigidbody> ().velocity = VELOCITY * ( this.transform.rotation * new Vector3(0,0,1));
		distanceTravelled = 0;
	}


	void Update() {
		distanceTravelled += VELOCITY * Time.deltaTime;
		if (distanceTravelled >= RANGE) {
			Destroy (this.gameObject);
		}
	}
		

	/* The function detects if the fireball hits on any other player while flying. If it does, 
	 * damage will be caused and the fireball will disappear.
	 */ 
	void OnCollisionEnter(Collision collision) {
		Character c;
		GameObject gameObject = collision.gameObject;
		if (gameObject.tag == CHARACTER_TAG) {
			c = gameObject.GetComponent<Character> ();
			if (!c.charID.Equals(charID)) {
				Destroy (this.gameObject);
				c.TakeDamage (damage);
				if (c.isDead) {
					c.numDeath++;
					PhotonView.Find (charID).gameObject.GetComponent<Character> ().Killed();
				}
			}
		} else {
			Destroy (this.gameObject);
		}

	}
}
