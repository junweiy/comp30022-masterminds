using UnityEngine;
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
		if (collision.gameObject.tag == "Character") {
			characterList.Add (collision.gameObject.GetComponent<CharacterController>().character);
			StartCoroutine (Damage(collision.gameObject.GetComponent<CharacterController>().character));
		}
	}

	void OnCollisionExit(Collision collision) {
		if (collision.gameObject.tag == "Character") {
			characterList.Remove (collision.gameObject.GetComponent<CharacterController>().character);
		}
	}

	IEnumerator Damage (Character player) {
		yield return new WaitForSeconds (healthDropSecondsInterval);
		while (characterList.Contains (player)) {
			player.TakeDamage (healthDropPerTime);
			yield return new WaitForSeconds (healthDropSecondsInterval);
		}
	}

}
