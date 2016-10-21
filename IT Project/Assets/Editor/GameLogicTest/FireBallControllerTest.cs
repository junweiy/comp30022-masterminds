using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class FireBallControllerTest {

	private const int DAMAGE = 10;

	GameObject fb;
	FireBallController fbController;
	GameObject characterObject;
	Character character;


	[SetUp]
	public void SetUp() {
		fb = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/FireBall", typeof(GameObject))) as GameObject;
		fb.transform.position = new Vector3 (10, 0, 10);
		fbController = fb.GetComponent<FireBallController> ();

		characterObject = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/Character", typeof(GameObject))) as GameObject;
		character = characterObject.GetComponent<Character> ();
		character.Hp = 100;
	}


	[Test]
	public void OnCollisionTest()
	{
		// First test that when fireball hit the player
		// The player will take damage based on the damage of the fireball
		// set the damage of fireball
		fbController.Damage = DAMAGE;
		//record previous hp
		float previoushp = character.Hp;
		//fbController.OnCollisionEnter (characterObject);
		//can't create a collision object in test. Collision can only be created by Unity's physics engine.
		//
		//So can't unit test unless another method is provided
		//e.g. a function takes an gameObject as argument

		//Assert.AreEqual (previoushp,character.hp);
	}
}
