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
	// Character that cast the spell
	public Character ch;

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
		yield return new WaitForSecondsRealtime (castingTime);
		// After casting time find all objects within casting range
		Collider[] colliders = Physics.OverlapSphere(this.transform.position, range);
		foreach (Collider hit in colliders) {
			if (!hit.CompareTag(CHARACTER_TAG)) {
				continue;
			}
			// all players around will be pushed with certain amount of power
			Rigidbody rb = hit.GetComponent<Rigidbody> ();
			if (rb != null) {
				CharacterController ccOther = hit.GetComponent<CharacterController> (); 
				rb.AddExplosionForce (power, transform.position, range);
				ccOther.character.TakeDamage (damage);
			}
		}
		// The spell object is destroyed
		Destroy (this.gameObject);
	}
}
