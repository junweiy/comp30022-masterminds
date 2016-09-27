using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class FireNovaController : NetworkBehaviour { 
	// The tag of the character
	public const string CHARACTER_TAG = "Character";
	// The damage of spell at current level
	[SyncVar]
	public int damage;
	// The range of spell at current level
	[SyncVar]
	public float range;
	// The power of spell at current level
	[SyncVar]
	public float power;
	// The casting time of spell at current level
	[SyncVar]
	public int castingTime;
	// Character netID that cast the spell
	[SyncVar]
	public NetworkInstanceId chId;

	public int timePassed;

	public bool alreadyCast;

	/* The function utilised coroutine to achieve casting time effect.
	 */ 
	void Start () {
		timePassed = 0;
		alreadyCast = false;
	}

	void Update() {
		timePassed++;
		if (timePassed >= castingTime && !alreadyCast) {
			castFireNova ();
			alreadyCast = true;
		}
	}

	public void castFireNova() {
		GameObject originalPlayer = ClientScene.FindLocalObject (chId);
		// After casting time find all objects within casting range
		Collider[] colliders = Physics.OverlapSphere(this.transform.position, range);
		foreach (Collider hit in colliders) {
			if (!hit.CompareTag(CHARACTER_TAG)) {
				continue;
			}
			if (hit.gameObject.GetComponent<Character> ().Equals (originalPlayer.GetComponent<Character>())) {
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
