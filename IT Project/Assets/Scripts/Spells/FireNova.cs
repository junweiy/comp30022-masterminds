using UnityEngine;
using System.Collections;

public class FireNova : Spell {
	// The damage of level 1 FireNova
	private const int INITIAL_DAMAGE = 40;
	// The power of level 1 FireNova
	private const float INITIAL_POWER = 8000.0F;
	// The cool down time of FireNova (unit in frames)
	private const int COOLDOWN = 1000;
	// The icon path used to genereate icon on spell bar
	private const string ICON_PATH = "textures/spells/fireNova";
	// Whether FireNova is a constant skill
	private const bool ISCONST = true;
	// The price of FireNova
	private const int PRICE = 10;
	// The name of FireNova
	public const string NAME = "fire nova";
	// The description of FireNova
	public const string DESCRIPTION = "fire nova";
	// The range within which ememies will be affected
	private const float INITIAL_RANGE = 30.0F;
	// The time required for casting (unit in secs)
	private const int INITIAL_CASTING_TIME = 1;
	// The path of the prefab
	private const string PREFAB_PATH = "Prefabs/FireNova";
	// The increment in damage when upgrading the spell
	private const int LVL_UP_DAMAGE_INCREMENT = 20;
	// The increment in range when upgrading the spell
	private const float LVL_UP_RANGE_INCREMENT = 1.0f;
	// The increment in power when upgrading the spell
	private const float LVL_UP_POWER_INCREMENT = 500.0f;

	// The damage of FireNova
	private int damage = INITIAL_DAMAGE;
	// The power of FireNova
	private float power = INITIAL_POWER;
	// The casting time for the spell
	private int castingTime = INITIAL_CASTING_TIME;

	/* The function initialises the FireNova object with basic properties.
	 */
	public FireNova() : base(COOLDOWN, ICON_PATH, ISCONST, PRICE, NAME, DESCRIPTION, INITIAL_RANGE) {
	}

	/* The function takes three arguments, which are the character object, the transform of the character
	 * and the destination that the player decided to cast towards. Then the FireNova will be initialised
	 * and during the casting time player will be disabled for moving, after which enemies surrounded by
	 * player will be hit against will explosive force and player can move around again.
	 */ 
	public override void applyEffect(Character character,Transform charTransform,Vector3 destination) {
		FireNovaController fnc;
		Object fireNovaPrefab = Resources.Load(PREFAB_PATH);
		GameObject fn = GameObject.Instantiate (fireNovaPrefab, charTransform.position, charTransform.rotation) as GameObject;
		fnc = fn.GetComponent<FireNovaController> ();
		fnc.damage = damage + character.baseAttack;
		fnc.range = range;
		fnc.power = power;
		fnc.castingTime = castingTime;
	}

	/* The function applies changes to the spell when upgrading it.
	 */
	public override void levelUp () {
		this.level++;
		this.range += LVL_UP_RANGE_INCREMENT;
		this.damage += LVL_UP_DAMAGE_INCREMENT;
		this.power += LVL_UP_POWER_INCREMENT;

	}

}
