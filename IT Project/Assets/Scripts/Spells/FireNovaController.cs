using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class FireNovaController : Photon.MonoBehaviour { 
	// The tag of the character
	public const string CHARACTER_TAG = "Character";
	// Character view ID
	public int charID;
	// The damage of spell at current level
	public int damage;
	// The range of spell at current level
	public float range;
	// The power of spell at current level
	public float power;
	// The casting time of spell at current level
	public int castingTime;

	public float timePassed;


	/* The function utilised coroutine to achieve casting time effect.
	 */ 
	void Start () {
		timePassed = 0;
	}

	void Update() {
		timePassed+= Time.deltaTime;
		if (timePassed >= castingTime) {
			castFireNova ();
		}
	}

	public void castFireNova() {
		// After casting time find all objects within casting range
		Collider[] colliders = Physics.OverlapSphere(this.transform.position, range);
		foreach (Collider hit in colliders) {
			if (!hit.CompareTag(CHARACTER_TAG)) {
				continue;
			}
			Character anotherCharacter = hit.GetComponent<Character> (); 
			if (anotherCharacter.charID.Equals(charID)) {
				continue;
			}
			// all players around will be pushed with certain amount of power
			Rigidbody rb = hit.GetComponent<Rigidbody> ();
			if (rb != null) {
				rb.AddExplosionForce (power, transform.position, range);
				anotherCharacter.TakeDamage (damage);
				if (anotherCharacter.isDead) {
					anotherCharacter.numDeath++;
					PhotonView.Find (charID).gameObject.GetComponent<Character> ().Killed();
				}

			}
		}
		// The spell object is destroyed
		Destroy (this.gameObject);
	}
}
