using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class FireNovaControllerTest {

	private const int DAMAGE = 10;

    private GameObject fn; // Fire Nova
    private FireBallController fnController;
    private GameObject characterObject;
    private Character character;


	[SetUp]
	public void SetUp() {
		fn = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/FireNova", typeof(GameObject))) as GameObject;
		fnController = fn.GetComponent<FireBallController> ();
		characterObject = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/Character", typeof(GameObject))) as GameObject;
		character = characterObject.GetComponent<Character> ();
	}



	[Test]
	public void OnCollisionTest()
	{
		//same condition as FireBallTest
		//can't use unit test
	}
}
