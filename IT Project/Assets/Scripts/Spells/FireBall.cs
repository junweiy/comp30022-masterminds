using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class FireBall : Spell {
	// The damage of level 1 Fireball
	private const int INITIAL_DAMAGE = 60;
	// The cool down time of Fireball (unit in frames)
	private const int COOLDOWN = 1;
	// The icon path used to genereate icon on spell bar
	private const string ICON_PATH = "Textures/Spells/fireBall";
	// Whether Fireball is a constant skill
	private const bool ISCONST = true;
	// The price of Fireball
	private const int PRICE = 10;
	// The name of Fireball
	public const string NAME = "fire ball";
	// The description of the Fireball
	public const string DESCRIPTION = "fire ball";
	// The path of the prefab
	public const string PREFAB_PATH = "Prefabs/Fireball";

	// The increment in damage when upgrading the spell
	public const int LVL_UP_DAMAGE_INCREMENT = 20;

	// The damage of Fireball
	public int damage { get; private set; }

	/* The function initialises the Fireball object with basic properties.
	 */
	public FireBall() : base(COOLDOWN, ICON_PATH, ISCONST, PRICE, NAME, DESCRIPTION) {
		this.damage = INITIAL_DAMAGE;
	}


//	/* The function takes three arguments, which are the character object, the transform of the character
//	 * and the destination that the player decided to cast towards. Then the fireball will be initialised
//	 * and move towards to desired direction until it hits a player or flys out of the border, and damage
//	 * will be given if it hits on a player.
//	 */ 
//	public override bool ApplyEffect(Character character,Transform charTransform,Vector3 destination) {
//		FireBallController fbc;
//		// The spawning point is half way between the character position and desired destination
//		Vector3 pos = charTransform.position + (destination - charTransform.position) / 2;
//		// The direction is the vector starting from current position towards desired destination
//		Vector3 dir = destination - charTransform.position;
//		GameObject fb = PhotonNetwork.Instantiate ("Prefabs/Fireball", pos, new Quaternion(0,0,0,0), 0);
//		// Change related properties to reflect certain level of spell
//		fbc = fb.GetComponent<FireBallController> ();
//		return fb != null;
//	}

	/* The function applies changes to the spell when upgrading it.
	 */
	public override void LevelUp () {
		this.level++;
		this.damage += LVL_UP_DAMAGE_INCREMENT;
	}
		
}
