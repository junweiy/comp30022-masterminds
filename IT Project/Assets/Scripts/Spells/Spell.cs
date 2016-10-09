using UnityEngine;
using UnityEngine.UI;

public abstract class Spell {
	// The cool down time of spell (unit in frames)
	public float cooldown {get; private set;}
	// The current cool down time
	public float currentCooldown {get; set;}
	// The damage of spell
	public int damage { get; private set; }


	public Spell() {
		this.cooldown = 0;
		this.currentCooldown = 0;
		this.damage = 0;
	}

	/* The initialisation of the spell with relative properties.
	 */
	public Spell(float cd, int damage) {
		this.cooldown = cd;
		this.currentCooldown = cd;
		this.damage = damage;
	}

}

