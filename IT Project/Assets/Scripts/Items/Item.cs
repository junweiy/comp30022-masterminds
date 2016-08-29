using UnityEngine;
using System.Collections;

abstract public class Item {
	// Name of the item
	private string name;
	// The price of selling the item back to shop
	private int sellingPrice;
	// The price of purchasing the item at shop
	private int purchasePrice;
	// The description of the item
	private string description;

	public Item(string name, int sellingPrice, int purchasePrice, string description) {
		this.name = name;
		this.sellingPrice = sellingPrice;
		this.purchasePrice = purchasePrice;
		this.description = description;
	}

	// Get the name of the item
	public string getName() {
		return this.name;
	}
	// Get the selling price of the item
	public int getSellingPrice() {
		return this.sellingPrice;
	}
	// Get the purchase price of the item
	public int getPurchasePrice() {
		return this.purchasePrice;
	}
	// Get the description of the item
	public string getDescription() {
		return this.description;
	}
	// Apply desired effects to the player
	public abstract void applyEffect(Character ply);
}
