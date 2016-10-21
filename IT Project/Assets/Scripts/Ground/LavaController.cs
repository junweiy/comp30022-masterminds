using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LavaController : MonoBehaviour {
	// Health Point dropped every time
	public int HealthDropPerTime;
	// Time between HP dropping in seconds
	public int HealthDropSecondsInterval;
	private List<Character> _characterList;


	void Start () {
		_characterList = new List<Character> ();
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "Character") {
			Debug.Log ("Enter");
			_characterList.Add (collision.gameObject.GetComponent<CharacterController>().Character);
			StartCoroutine (Damage(collision.gameObject.GetComponent<CharacterController>().Character));
		}
	}

	void OnCollisionExit(Collision collision) {
		if (collision.gameObject.tag == "Character") {
			_characterList.Remove (collision.gameObject.GetComponent<CharacterController>().Character);
		}
	}

	IEnumerator Damage (Character player) {
		yield return new WaitForSeconds (HealthDropSecondsInterval);
		while (_characterList.Contains (player)) {
			player.TakeDamage (HealthDropPerTime);
			if (GameController.CheckIfGameEnds() && !FindMainCharacter ().IsDead) {
				FindMainCharacter ().UpdateProfile (true);
			}
			yield return new WaitForSeconds (HealthDropSecondsInterval);
		}
	}

	public static Character FindMainCharacter() {
		GameObject mainPlayer = VoiceButtonController.FindMainPlayer ();
		if (mainPlayer != null) {
			return mainPlayer.GetComponent<Character> ();
		}
		return null;
	}
		
}
