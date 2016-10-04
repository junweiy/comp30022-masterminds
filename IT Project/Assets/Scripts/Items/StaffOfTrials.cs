using UnityEngine;
using System.Collections;

public class StaffOfTrials : Item {
	// The name of the item
	private const string NAME = "Staff of Trials";
	// The selling price of the item
	private const int SELLING_PRICE = 100;
	// The purchase price of the item
	private const int PURCHASE_PRICE = 200;
	// The description of the item
	private const string DESCRIPTION = "It is a item that can increase the damage of all spells.";
	// The attack increment of level 1 Life Necklace
	private const int INITIAL_ATTACK_INCREMENT = 10;
	// The extra attack increment when leveling up
	public const int LVL_UP_ATTACK_INCREMENT = 10;

	// The attack increased when wearing the equipment
	public int attackIncreased;

	public StaffOfTrials() : base(NAME, ItemTypeEnum.Equipment, SELLING_PRICE, PURCHASE_PRICE, DESCRIPTION) {
		attackIncreased = INITIAL_ATTACK_INCREMENT;
	}

	/* The function applies the effect of the equipment when the character wears it.
	 */
	public override void ApplyEffect(Character ply) {
		//ply.baseAttack += attackIncreased;
	}

	/* The function removes the effect of the equipment when the character takes it off.
	 */
	public override void RemoveEffect(Character ply) {
		//ply.baseAttack -= attackIncreased;
	}

	/* The function applies changes to the spell when upgrading it.
	 */
	public override void LevelUp () {
		this.level++;
		attackIncreased += LVL_UP_ATTACK_INCREMENT;
	}

}
