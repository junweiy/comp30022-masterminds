using UnityEngine;
using UnityEngine.UI;

public abstract class Spell {
	// The cool down time of spell (unit in frames)
	public float cooldown {get; private set;}
	// The icon path used to genereate icon on spell bar
	public string iconPath {get; private set;}
	// Whether the spell is a constant skill
	public bool isInstantSpell {get; private set;}
	// The current cool down time
	public float currentCooldown {get; set;}
	// The price of the spell
	public int price {get; private set;}
	// The name of the spell
	public string name {get; private set;}
	// The description of the spell
	public string description {get; private set;}
	// The current level of the spell
	public int level {get; set;}
	// The range of the spell
	public float range {get; private set;}

	/* The initialisation of the spell with relative properties.
	 */
	public Spell(float cd, string path, bool isInstant, int price, string name, string des,float range) {
		this.cooldown = cd;
		this.iconPath = path;
		this.isInstantSpell = isInstant;
		this.currentCooldown = cd;
		this.price = price;
		this.name = name;
		this.description = des;
		this.level = 1;
		this.range = range;
	}


	// Function used to apply the effect to the character
	abstract public void applyEffect(Character character,Transform characterTransform,Vector3 destination);
	// Function used to modify properties to achieve levelup
	abstract public void levelUp ();

}

