using UnityEngine;
using System.Collections;

public enum ItemTypeEnum {Spell, Equipment, Upgrade}

abstract public class Item {
	// Name of the item
	public string itemName {get; private set;}
    // Type of the item
	public ItemTypeEnum itemType {get; private set;}
    // The price of selling the item back to shop
	public int sellingPrice {get; private set;}
	// The price of purchasing the item at shop
	public int purchasePrice {get; private set;}
	// The description of the item
	public string description {get; private set;}
	// The level of the item
	public int level {get; set;}

	// Initialise an Item instance with given information
	public Item(string name, ItemTypeEnum type, int sellingPrice, int purchasePrice, string description) {
		this.itemName = name;
        this.itemType = type;
		this.sellingPrice = sellingPrice;
		this.purchasePrice = purchasePrice;
		this.description = description;
		this.level = 1;
	}
		
	// Apply desired effects to the player when wearing
	public abstract void ApplyEffect(Character ply);
	// remove effects to the player when taking off
	public abstract void RemoveEffect(Character ply);
	// Function used to modify properties to achieve levelup
	abstract public void LevelUp ();
}
