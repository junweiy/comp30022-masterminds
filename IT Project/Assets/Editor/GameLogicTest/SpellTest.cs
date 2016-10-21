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
		Assert.AreEqual (s.CurrentCooldown, COOLDOWN);
		Assert.AreEqual (s.Cooldown, COOLDOWN);

		Spell s2 = new Spell ();
		Assert.AreEqual (s.CurrentCooldown, 0);
		Assert.AreEqual (s.Cooldown, 0);
		Assert.AreEqual (s.Damage, 0);
	}



}
