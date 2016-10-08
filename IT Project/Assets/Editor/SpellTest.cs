using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System;

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


}
