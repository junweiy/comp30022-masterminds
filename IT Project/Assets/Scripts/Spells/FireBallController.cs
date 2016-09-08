using UnityEngine;
using System.Collections;

public class FireBallController : MonoBehaviour {
	public int damage = 40;

	private CharacterController characterController;
	void OnCollisionEnter(Collision collision) {
		GameObject gameObject = collision.gameObject;
		Destroy (this.gameObject);
		if (gameObject.tag == "Character") {
			characterController = gameObject.GetComponent<CharacterController>();
			characterController.character.TakeDamage (damage);
		}

	}
}
