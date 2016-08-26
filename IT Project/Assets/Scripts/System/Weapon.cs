using UnityEngine;
using System.Collections;

public abstract class Weapon : Item {

	public abstract int getPrice ();

	public abstract int getDamage();

	public abstract string getDescription();
}
