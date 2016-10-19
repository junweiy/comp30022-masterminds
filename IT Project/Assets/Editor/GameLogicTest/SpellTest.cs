using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System;

public class SpellTest {
	
	private const float COOLDOWN = 5;
	private const int DAMAGE = 10;


	[SetUp]
	public void Start() {
		
	}

	[Test]
	public void InitializeSpell(){
		Spell s = new Spell (COOLDOWN, DAMAGE);
		Assert.AreEqual (s.currentCooldown, COOLDOWN);
		Assert.AreEqual (s.cooldown, COOLDOWN);

		Spell s2 = new Spell ();
		Assert.AreEqual (s.currentCooldown, 0);
		Assert.AreEqual (s.cooldown, 0);
		Assert.AreEqual (s.damage, 0);
	}



}
