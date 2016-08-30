using UnityEngine;
using System.Collections;

public class StaffOfTrials : Item {

	public int attackIncreased;

	public StaffOfTrials() : base("Staff of Trials", ItemTypeEnum.Equipment, 100, 200, "It is a item that can increase the damage of all spells.") {
	}


	public override void applyEffect(Character ply) {
		// ply.setAttack(ply.getAttack() + attackIncreased);
	}

}
