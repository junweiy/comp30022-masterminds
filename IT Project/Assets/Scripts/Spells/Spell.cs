using UnityEngine;
using UnityEngine.UI;

public abstract class Spell {
	
	public float cooldown;
	public Image spellIcon;
    private bool isInstantSpell;
	public float currentCooldown;

	abstract public int getPrice ();

	abstract public string getDescription ();

	abstract public void applyEffect(Character character,Transform characterTransform,Vector3 destination);
    abstract public void applyEffect(Character character, Transform characterTransform);

    public bool isInstant()
    {
        return isInstantSpell;
    }

}

