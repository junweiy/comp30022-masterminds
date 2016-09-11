﻿using UnityEngine;
using System.Collections;

public class FireBallController : MonoBehaviour {
	// The tag of the character
	public const string CHARACTER_TAG = "Character";
	// The damage of spell at current level
	public int damage;

	/* The function detects if the fireball hits on any other player while flying. If it does, 
	 * damage will be caused and the fireball will disappear.
	 */ 
	void OnCollisionEnter(Collision collision) {
		CharacterController cc;
		GameObject gameObject = collision.gameObject;
		Destroy (this.gameObject);
		if (gameObject.tag == CHARACTER_TAG) {
			cc = gameObject.GetComponent<CharacterController>();
			cc.character.TakeDamage (damage);
		}

	}
}
