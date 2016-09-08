using UnityEngine;
using UnityEngine.UI;

public abstract class Spell {
	
	public float cooldown;
	public string iconPath;
	public bool isInstantSpell;
	public float currentCooldown;
	public int price;
	public string name;
	public string description;
	public int level;
	public float range;

	public Spell(float cd, string path, bool isInstant, float currCd, int price, string name, string des,float range) {
		this.cooldown = cd;
		this.iconPath = path;
		this.isInstantSpell = isInstant;
		this.currentCooldown = currCd;
		this.price = price;
		this.name = name;
		this.description = des;
		this.level = 1;
		this.range = range;
	}



	abstract public void applyEffect(Character character,Transform characterTransform,Vector3 destination);
	abstract public void levelUp ();

    public bool isInstant()
    {
        return isInstantSpell;
    }

}

