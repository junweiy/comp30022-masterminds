using UnityEngine;
using System.Collections;

public class LifeNecklace : Item {
	// The name of the item
	private const string NAME = "Life Necklace";
	// The selling price of the item
	private const int SELLING_PRICE = 100;
	// The purchase price of the item
	private const int PURCHASE_PRICE = 200;
	// The description of the item
	private const string DESCRIPTION = "It is a item that can increase the max of HP.";
	// The HP increment of level 1 Life Necklace
	private const int INITIAL_HP_INCREMENT = 10;
	// The extra HP increment when leveling up
	public const int LVL_UP_HP_INCREMENT = 10;

	// The maximum health point can be increased when wearing this equipment
	public int maxHPIncreased { get; private set; }

	/* The function initialises the object with relative properties
	 */
	public LifeNecklace() : base(NAME, ItemTypeEnum.Equipment, SELLING_PRICE, PURCHASE_PRICE, DESCRIPTION) {
		maxHPIncreased = INITIAL_HP_INCREMENT;
	}
		
	/* The function applies the effect of Life Necklace when the character wears it.
	 */
	public override void ApplyEffect(Character ply) {
		ply.maxHp += maxHPIncreased;
	}

	/* The function removes the effect of Life Necklace when the character takes it off.
	 */
	public override void RemoveEffect(Character ply) {
		ply.maxHp -= maxHPIncreased;
	}

	/* The function applies changes to the spell when upgrading it.
	 */
	public override void LevelUp () {
		this.level++;
		maxHPIncreased += LVL_UP_HP_INCREMENT;
	}
}
