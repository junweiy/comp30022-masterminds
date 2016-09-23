using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class FireBallController : NetworkBehaviour {
	// The tag of the character
	public const string CHARACTER_TAG = "Character";
	// Character netID that cast the spell
	[SyncVar]
	public NetworkInstanceId chId;

	[SyncVar]
	public Vector3 velocity;

	[SyncVar]
	public float range;

	// The damage of spell at current level
	[SyncVar]
	public int damage;

	[SyncVar]
	public float distanceTravelled;

	void Start() {
		distanceTravelled = 0;
	}

	void Update() {
		distanceTravelled += velocity.magnitude;
		this.transform.position += velocity;
		if (distanceTravelled >= range) {
			Destroy (this.gameObject);
		}
	}

	/* The function detects if the fireball hits on any other player while flying. If it does, 
	 * damage will be caused and the fireball will disappear.
	 */ 
	void OnCollisionEnter(Collision collision) {
		GameObject originalPlayer = ClientScene.FindLocalObject (chId);
		Character c;
		GameObject gameObject = collision.gameObject;
		if (gameObject.tag == CHARACTER_TAG) {
			c = gameObject.GetComponent<Character> ();
			if (!originalPlayer.GetComponent<Character>().Equals(c)) {
				Destroy (this.gameObject);
				c.TakeDamage (damage);
			}
		} else {
			Destroy (this.gameObject);
		}

	}
}
