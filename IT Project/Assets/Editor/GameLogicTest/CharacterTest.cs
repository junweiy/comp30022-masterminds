using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System;

/**
 * This class is an unit test for character
 * */
public class CharacterTest {

	private GameObject characterObject;
    private Character character;

	public const int DAMAGE_TAKEN_FOR_TESTING = 10;

    [SetUp]
    //When set up, we just need to instantiate a unity game object from resources.
    //Then we get the CharacterController Component
    public void SetUp() {
		characterObject = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/Character", typeof(GameObject))) as GameObject;
		character = characterObject.GetComponent<Character> ();
    }

    [Test]
    //Test the only public function take damage
    //1. assert hp is deducted after take damage
    //2. take damage until the character died, see if the character is marked dead and hp should be equal to zero
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
