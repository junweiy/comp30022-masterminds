using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System;


// The class will test functionality of all items
public class ItemTest {

	public const float MAXHP = 10;
	public const int BASEATTACK = 0;

	LifeNecklace lifeNecklace;
	StaffOfTrials staffOfTrials;
	Character characterStub;

	[SetUp]
	public void Setup() {
		lifeNecklace = new LifeNecklace ();
		staffOfTrials = new StaffOfTrials ();
		characterStub = NSubstitute.Substitute.For<Character> ();
		characterStub.maxHp = MAXHP;
		characterStub.baseAttack = BASEATTACK;
	}

	[Test]
	public void CanApplyEffectWithLifeNecklace() {
		float initialMaxHP = characterStub.maxHp;
		lifeNecklace.ApplyEffect (characterStub);
		Assert.AreEqual (characterStub.maxHp, initialMaxHP + lifeNecklace.maxHPIncreased);
	}

	[Test]
	public void CanApplyEffectWithStaffOfTrials() {
		int initialBaseAttack = characterStub.baseAttack;
		staffOfTrials.ApplyEffect (characterStub);
		Assert.AreEqual (characterStub.baseAttack, initialBaseAttack + staffOfTrials.attackIncreased);
	}

	[Test]
	public void CanRemoveEffectWithLifeNecklace() {
		float initialMaxHP = characterStub.maxHp;
		lifeNecklace.RemoveEffect (characterStub);
		Assert.AreEqual (characterStub.maxHp, initialMaxHP - lifeNecklace.maxHPIncreased);
	}

	[Test]
	public void CanRemoveEffectWithStaffOfTrials() {
		int initialBaseAttack = characterStub.baseAttack;
		staffOfTrials.RemoveEffect (characterStub);
		Assert.AreEqual (characterStub.baseAttack, initialBaseAttack - staffOfTrials.attackIncreased);
	}

	[Test]
	public void CanLevelUpWithLifeNecklace() {
		int initialMaxHPIncreased = lifeNecklace.maxHPIncreased;
		int initialLevel = lifeNecklace.level;
		lifeNecklace.LevelUp ();
		Assert.AreEqual (lifeNecklace.maxHPIncreased, initialMaxHPIncreased + LifeNecklace.LVL_UP_HP_INCREMENT);
		Assert.AreEqual (lifeNecklace.level, initialLevel + 1);
	}

	[Test]
	public void CanLevelUpWithStaffOfTrials() {
		int initialMaxHPIncreased = staffOfTrials.attackIncreased;
		int initialLevel = staffOfTrials.level;
		staffOfTrials.LevelUp ();
		Assert.AreEqual (staffOfTrials.attackIncreased, initialMaxHPIncreased + StaffOfTrials.LVL_UP_ATTACK_INCREMENT);
		Assert.AreEqual (staffOfTrials.level, initialLevel + 1);
	}



}
