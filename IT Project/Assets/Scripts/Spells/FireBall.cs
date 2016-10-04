using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class FireBall : Spell {
	// The damage of Fireball
	private const int DAMAGE = 60;
	// The cool down time of Fireball (unit in frames)
	private const int COOLDOWN = 10;
	// The path of the prefab
	public const string PREFAB_PATH = "Prefabs/Fireball";

	/* The function initialises the Fireball object with basic properties.
	 */
	public FireBall() : base(COOLDOWN, DAMAGE) {
	}


}
