using UnityEngine;
using System.Collections;

public class FireBallController : MonoBehaviour {
	// The tag of the character
	public const string CHARACTER_TAG = "Character";
	// Character that cast the spell
	public Character ch;
	// The damage of spell at current level
	public int damage;

	/* The function detects if the fireball hits on any other player while flying. If it does, 
	 * damage will be caused and the fireball will disappear.
	 */ 
	void OnCollisionEnter(Collision collision) {
		CharacterController cc;
		GameObject gameObject = collision.gameObject;
		if (gameObject.tag == CHARACTER_TAG) {
			cc = gameObject.GetComponent<CharacterController>();
			if (cc.character != ch) {
				Destroy (this.gameObject);
				cc.character.TakeDamage (damage);
			}
		}

	}
}
