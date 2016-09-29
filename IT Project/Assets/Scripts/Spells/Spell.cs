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

	public Spell() {
		this.cooldown = 0;
		this.iconPath = "";
		this.isInstantSpell = false;
		this.currentCooldown = 0;
		this.price = 0;
		this.name = "";
		this.description = "";
		this.level = 1;
	}

	/* The initialisation of the spell with relative properties.
	 */
	public Spell(float cd, string path, bool isInstant, int price, string name, string des) {
		this.cooldown = cd;
		this.iconPath = path;
		this.isInstantSpell = isInstant;
		this.currentCooldown = cd;
		this.price = price;
		this.name = name;
		this.description = des;
		this.level = 1;
	}


	// Function used to modify properties to achieve levelup
	abstract public void LevelUp ();

}

