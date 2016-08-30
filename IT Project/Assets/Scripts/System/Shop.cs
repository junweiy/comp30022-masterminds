using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class Shop {

	private HashSet<Item> items;

	public bool canPurchase(Item item, Character character) {
		return (character.getCoin() >= item.PurchasePrice) && character.hasSpaceForItem(item);
	}

	public void purchase(Item item, Character character) {
		if (canPurchase (item, character)) {
			character.addItem (item);
			character.deductCoin (item.PurchasePrice);
		}
	}

	public HashSet<Item> getPurchasableItems(Character character) {
		return (HashSet<Item>) this.items.Where(i => this.canPurchase(i, character));
	}

    public HashSet<Item> getPurchasableEquipments()
    {
        return (this.items);
    }

	// create a empty shop
	public Shop() {
		this.items = new HashSet<Item> ();
	}

	// create a shop with provided items
	public Shop(IEnumerable<Item> items) {
		this.items = new HashSet<Item> (items);
	}

    public Shop(Item item)
    {
        this.items = new HashSet<Item>();
        this.items.Add(item);
    }

}
