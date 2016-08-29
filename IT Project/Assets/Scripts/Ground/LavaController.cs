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
		if (collision.gameObject.name == "Character") {
			Debug.Log ("1");
			characterList.Add (collision.gameObject.GetComponent<Character>());
			StartCoroutine (Damage(collision.gameObject.GetComponent<Character>()));
		}


	}

	void OnCollisionExit(Collision collision) {
		if (collision.gameObject.name == "Character") {
			Debug.Log ("2");
			characterList.Remove (collision.gameObject.GetComponent<Character>());
		}
	}

	IEnumerator Damage (Character player) {
		while (characterList.Contains (player)) {
			player.TakeDamage (healthDropPerTime);
			yield return new WaitForSeconds (healthDropSecondsInterval);
		}
	}

}
