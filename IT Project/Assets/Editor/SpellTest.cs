using UnityEngine;
using UnityEditor;
using NUnit.Framework;

// The test will test the behaviour of fire ball and fire nova spells
public class SpellTest {
	FireBall fb;
	FireNova fn;
	Character character;
	GameObject characterObject;
	Transform t;
	Vector3 des;


	[SetUp]
	public void Start() {
		fb = new FireBall ();
		fn = new FireNova ();
		character = new Character ();
		characterObject = new GameObject ();
		t = characterObject.transform;
		des = new Vector3 (0, 0, 0);
	}

	[Test]
	public void CanApplyEffectOfFireBall() {
		Assert.True(fb.ApplyEffect(character, t, des));
	}

	[Test]
	public void CanApplyEffectOfFireNova() {
		Assert.True(fn.ApplyEffect(character, t, des));
	}

	[Test]
	public void CanUpgradeFireBall() {
		int initialLevel = fb.level;
		int initialDamage = fb.damage;
		fb.LevelUp ();
		Assert.AreEqual (fb.level, initialLevel + 1);
		Assert.AreEqual (fb.damage, initialDamage + FireBall.LVL_UP_DAMAGE_INCREMENT);
	}

	[Test]
	public void CanUpgradeFireNova() {
		int initialLevel = fn.level;
		int initialDamage = fn.damage;
		float initialPower = fn.power;
		float initialRange = fn.range;
		fn.LevelUp ();
		Assert.AreEqual (fn.level, initialLevel + 1);
		Assert.AreEqual (fn.damage, initialDamage + FireNova.LVL_UP_DAMAGE_INCREMENT);
		Assert.AreEqual (fn.power, initialPower + FireNova.LVL_UP_POWER_INCREMENT);
		Assert.AreEqual (fn.range, initialRange + FireNova.LVL_UP_RANGE_INCREMENT);
	}

	[TearDown]
	public void TearDown() {
		if (characterObject) {
			Object.DestroyImmediate(characterObject);
		}
	}

}
