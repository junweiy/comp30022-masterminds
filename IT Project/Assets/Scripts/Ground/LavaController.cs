﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LavaController : MonoBehaviour {
	// Health Point dropped every time
	public int healthDropPerTime;
	// Time between HP dropping in seconds
	public int healthDropSecondsInterval;
	private List<Character> characterList;


	void Start () {
		characterList = new List<Character> ();
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.name == "Character") {
			Debug.Log ("1");
			characterList.Add (collision.gameObject.GetComponent<CharacterController>().getCharacter());
			StartCoroutine (Damage(collision.gameObject.GetComponent<CharacterController>().getCharacter()));
		}


	}

	void OnCollisionExit(Collision collision) {
		if (collision.gameObject.name == "Character") {
			Debug.Log ("2");
			characterList.Remove (collision.gameObject.GetComponent<CharacterController>().getCharacter());
		}
	}

	IEnumerator Damage (Character player) {
		while (characterList.Contains (player)) {
			player.loseHealth (healthDropPerTime);
			yield return new WaitForSeconds (healthDropSecondsInterval);
		}
	}

}
