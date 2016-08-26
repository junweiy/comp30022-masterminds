using UnityEngine;
using System.Collections;

public abstract class Armour : Item {

	public abstract int getPrice ();

	public abstract int getDefense();

	public abstract string getDescription();
}
