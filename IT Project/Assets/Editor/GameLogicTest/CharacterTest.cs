using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System;

public class CharacterTest {

	GameObject characterObject;
	Character character;

	public const int DAMAGE_TAKEN_FOR_TESTING = 10;

    [SetUp]
    public void SetUp() {
		characterObject = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/Character", typeof(GameObject))) as GameObject;
		character = characterObject.GetComponent<Character> ();
    }

    [Test]
    public void TakeDamageTest() {

		float currHP = character.Hp;
		character.TakeDamage (DAMAGE_TAKEN_FOR_TESTING);
		Assert.AreEqual (character.Hp, currHP - DAMAGE_TAKEN_FOR_TESTING);
		// Alway take damage until he died.
		while (character.Hp > 0) {
			character.TakeDamage (DAMAGE_TAKEN_FOR_TESTING);
		}
		// if character is died, its hp should be zero and can't be less than 0.
		Assert.True (character.IsDead);
		Assert.True (character.Hp == 0);

    }


}
