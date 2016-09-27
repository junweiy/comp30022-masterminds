using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class CharacterTest {

    Character character;
	Spell dummySpell;
	Item dummyItem;

	public const float DAMAGE_TAKEN_FOR_TESTING = 10;
	public const int COIN_CHANGE_FOR_TESTING = 10;
	public const int SCORE_CHANGE_FOR_TESTING = 10;

    [SetUp]
    public void SetUp() {
		character = new Character();
		dummySpell = NSubstitute.Substitute.For<Spell> ();
		dummyItem = NSubstitute.Substitute.For<Item> ();
    }

	[Test]
	public void AddSpellTest()
	{
		character.AddSpell(dummySpell);
		Assert.That (character.spells, Has.Exactly (1).EqualTo(dummySpell));
	}

    [Test]
	[ExpectedException(typeof(FullItemException))]
    public void AddItemTest() {
		character.AddItem (dummyItem);
		Assert.That (character.items, Has.Exactly (1).EqualTo(dummyItem));
		while (character.HasSpaceForItem()) {
			character.AddItem (dummyItem);
		}
		character.AddItem (dummyItem);
    }

    [Test]
    public void HasSpaceForItemTest() {
		Assert.True (character.HasSpaceForItem());
		while (character.items.Count < Character.MAXIMUM_NUMBER_OF_ITEM) {
			character.AddItem (dummyItem);
		}
		Assert.False (character.HasSpaceForItem());
    }

    [Test]
    public void TakeDamageTest() {
		float currHP = character.hp;
		character.TakeDamage (DAMAGE_TAKEN_FOR_TESTING);
		Assert.AreEqual (character.hp, currHP - DAMAGE_TAKEN_FOR_TESTING);
		while (character.hp > 0) {
			character.TakeDamage (DAMAGE_TAKEN_FOR_TESTING);
		}
		Assert.True (character.isDead);
    }

	[Test]
	public void AddCoinTest() {
		int initialCoin = character.coin;
		character.AddCoin (COIN_CHANGE_FOR_TESTING);
		Assert.AreEqual (character.coin, initialCoin + COIN_CHANGE_FOR_TESTING);
	}

    [Test]
    public void DeductCoinTest() {
		int initialCoin = character.coin;
		character.DeductCoin (COIN_CHANGE_FOR_TESTING);
		Assert.AreEqual (character.coin, initialCoin - COIN_CHANGE_FOR_TESTING);
    }

    [Test]
    public void AddScoreTest() {
		int initialScore = character.score;
		character.AddScore (SCORE_CHANGE_FOR_TESTING);
		Assert.AreEqual (character.score, initialScore + SCORE_CHANGE_FOR_TESTING);
    }

    [Test]
    public void DeductScoreTest() {
		int initialScore = character.score;
		character.DeductScore (SCORE_CHANGE_FOR_TESTING);
		Assert.AreEqual (character.score, 0);
		character.AddScore (SCORE_CHANGE_FOR_TESTING);
		int anotherScore = character.score;
		character.DeductScore (SCORE_CHANGE_FOR_TESTING);
		Assert.AreEqual (character.score, anotherScore - SCORE_CHANGE_FOR_TESTING);
    }


}
