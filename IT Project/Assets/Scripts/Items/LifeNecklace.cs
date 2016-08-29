using UnityEngine;
using System.Collections;

public class LifeNecklace : Item {
	public int maxHPIncreased;
	
	public LifeNecklace() : base("Life Necklace", 100, 200, "It is a item that can increase the max of HP.") {
	}
		

	public override void applyEffect(Character ply) {
		// ply.setHP(ply.getHP() + maxHPIncreased);
	}

}
