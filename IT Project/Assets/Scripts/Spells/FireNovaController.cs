using UnityEngine;
using System.Collections;

public class FireNovaController : MonoBehaviour { 
	// The tag of the character
	public const string CHARACTER_TAG = "Character";
	// The damage of spell at current level
	public int damage;
	// The range of spell at current level
	public float range;
	// The power of spell at current level
	public float power;
	// The casting time of spell at current level
	public int castingTime;

	/* The function looks for the main character and returns the character controller component of 
	 * the character.
	 */ 
	private CharacterController findMainCharacterController() {
		GameObject[] gos = GameObject.FindGameObjectsWithTag (CHARACTER_TAG);
		foreach (GameObject go in gos) {
			CharacterController cc = go.GetComponent<CharacterController> ();
			if (cc.isMainCharacter) {
				return cc;
			}
		}
		return null;
	}

	/* The function utilised coroutine to achieve casting time effect.
	 */ 
	void Start () {
		StartCoroutine (castFireNova());
	}

	/* The function firstly disables the movement of the main character for several seconds,
	 * then detect all players within the casting range and push against all of them and give
	 * corresponding amount of damage. 
	 */ 
	IEnumerator castFireNova() {
		CharacterController cc = findMainCharacterController ();;
		Character c = cc.character;
		// Stop the movement of the player
		c.canMove = false;
		cc.stopMoving ();
		yield return new WaitForSecondsRealtime (castingTime);
		// After casting time find all objects within casting range
		Collider[] colliders = Physics.OverlapSphere(this.transform.position, range);
		foreach (Collider hit in colliders) {
			if (!hit.CompareTag(CHARACTER_TAG)) {
				continue;
			}
			if (!hit.GetComponent<CharacterController> (). isMainCharacter) {
				// all other players will be pushed with certain amount of power
				Rigidbody rb = hit.GetComponent<Rigidbody> ();
				if (rb != null) {
					CharacterController ccOther = hit.GetComponent<CharacterController> (); 
					rb.AddExplosionForce (power, transform.position, range);
					ccOther.character.TakeDamage (damage);
				}
			}
		}
		// The player can move again
		c.canMove = true;
		// The spell object is destroyed
		Destroy (this.gameObject);
	}
}
