using UnityEngine;
using System.Collections;

public class FireBallController : MonoBehaviour {
	public int damage = 40;

	private Character character;
	void OnCollisionEnter(Collision collision) {
		GameObject gameObject = collision.gameObject;
		Destroy (this.gameObject);
		if (gameObject.tag == "Character") {
			character = gameObject.GetComponent<Character>();
			character.TakeDamage (damage);
		}

	}
}
