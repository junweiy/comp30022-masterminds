using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class FireNova : Spell {
	// The damage of FireNova
	private const int DAMAGE = 30;
	// The power of FireNova
	private const float POWER = 800.0F;
	// The cool down time of FireNova (unit in frames)
	private const int COOLDOWN = 5;
	// The range within which ememies will be affected
	private const float RANGE = 60.0F;
	// The time required for casting (unit in secs)
	public const int CASTING_TIME = 5;
	// The path of the prefab
	private const string PREFAB_PATH = "Prefabs/FireNova";


	// The range of FireNova
	public float range { get; set; }
	// The power of FireNova
	public float power { get; private set; }
	// The casting time for the spell
	public int castingTime { get; private set; }

	/* The function initialises the FireNova object with basic properties.
	 */
	public FireNova() : base(COOLDOWN, DAMAGE) {
		this.power = POWER;
		this.castingTime = CASTING_TIME;
		this.range = RANGE;
	}

}
