using UnityEngine;
using System.Collections;

abstract public class Item {
	// Name of the item
	private string itemName;
	public string ItemName {
		get { return itemName; }
		set { itemName = value; }
	}
    // Type of the item
    private string itemType;
    public string ItemType
    {
        get { return itemType; }
        set { itemType = value; }
    }
    // The price of selling the item back to shop
    private int sellingPrice;
	public int SellingPrice {
		get { return sellingPrice;}
		set { sellingPrice = value;}
	}
	// The price of purchasing the item at shop
	private int purchasePrice;
	public int PurchasePrice {
		get { return purchasePrice; }
		set { purchasePrice = value;}
	}
	// The description of the item
	private string description;
	public string Description{
		get { return description; }
		set { description = value; }
	}

	// Initialise an Item instance with given information
	public Item(string name, string type, int sellingPrice, int purchasePrice, string description) {
		this.itemName = name;
        this.itemType = type;
		this.sellingPrice = sellingPrice;
		this.purchasePrice = purchasePrice;
		this.description = description;
	}
		
	// Apply desired effects to the player
	public abstract void applyEffect(Character ply);
}
