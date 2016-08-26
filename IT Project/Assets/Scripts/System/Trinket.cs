using UnityEngine;
using System.Collections;

public abstract class Trinket : Item {

	public abstract int getPrice ();

	public abstract void applyEffect(Character character);

	string getDescription();
}
