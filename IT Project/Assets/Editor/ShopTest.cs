using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System;

public class ShopTest {

    protected class DummyItem : Item {
        public DummyItem(string name, ItemTypeEnum type, int sellingPrice, int purchasePrice, string description) : base(name, type, sellingPrice, purchasePrice, description) {
        }

        public override void ApplyEffect(Character ply) {
            return;
        }

        public override void LevelUp() {
            return;
        }

        public override void RemoveEffect(Character ply) {
            return;
        }
    }
		

    Character c;
    Shop s;
	Item item1;
	Item item2;

    [SetUp]
    public void setUp() {
		c = new Character ();
		s = new Shop ();
		item1 = new DummyItem("item1", ItemTypeEnum.Equipment, 20, 40, "Description");
		item2 = new DummyItem("item2", ItemTypeEnum.Upgrade, 100, 200, "Description");
		c.AddCoin (100);
        s.addItem(item1);
        s.addItem(item2);
    }

    [Test]
	public void canPurchaseTest() {
        Assert.True(s.canPurchase(item1, c));
        Assert.False(s.canPurchase(item2, c));
		c.AddCoin (1000);
        Assert.True(s.canPurchase(item1, c));
        Assert.True(s.canPurchase(item2, c));
    }

    [Test]
    public void purchaseTest() {
        s.purchase(item1, c);
        Assert.True(c.items.Contains(item1));
        //Assert.False(s.canPurchase(item1, c));
		c.AddCoin(1000);
        //Assert.False(s.canPurchase(item1, c));
        s.purchase(item2, c);
        Assert.True(c.items.Contains(item2));
    }

    [Test]
    public void getPurchaseableItemsTest() {
        var purchaseable = s.getPurchasableItems(c);
        Assert.AreEqual(1, purchaseable.Count);
        Assert.True(purchaseable.Contains(item1));

		c.AddCoin(1000);
        purchaseable = s.getPurchasableItems(c);
        Assert.AreEqual(2, purchaseable.Count);
        Assert.True(purchaseable.Contains(item1));
        Assert.True(purchaseable.Contains(item2));
    }

    [Test]
    public void getPurchaseableItemsWithTypeTest() {
        var purchaseableEquipment = s.getPurchaseableItemsWithType(c, ItemTypeEnum.Equipment);
        var purchaseableUpgrade = s.getPurchaseableItemsWithType(c, ItemTypeEnum.Upgrade);
        Assert.True(purchaseableEquipment.Contains(item1));
        Assert.AreEqual(1, purchaseableEquipment.Count);
        Assert.False(purchaseableUpgrade.Contains(item2));
        Assert.AreEqual(0, purchaseableUpgrade.Count);

		c.AddCoin(10000);
        purchaseableUpgrade = s.getPurchaseableItemsWithType(c, ItemTypeEnum.Upgrade);
        Assert.True(purchaseableUpgrade.Contains(item2));
        Assert.AreEqual(1, purchaseableUpgrade.Count);
    }


}

