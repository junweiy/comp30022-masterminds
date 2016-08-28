using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public abstract class Spell : Item {
	
	public float cooldown;
	public Image spellIcon;
	[HideInInspector]
	public float currentCooldown;

	abstract public int getPrice ();

	abstract public string getDescription ();

	abstract public void applyEffect(Vector3 charPos, Character character);
}
