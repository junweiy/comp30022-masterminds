using UnityEngine;
using UnityEditor;
using NUnit.Framework;

/**
 * An unit test for fire ball class
 * */
public class FireBallTest {

	private const float COOLDOWN = 2;
	private const int DAMAGE = 20;

	[Test]
    //The class only have a constructor, so just create an new object and tests its properties.
	public void CreateTest()
	{
		FireBall fb = new FireBall();
		Assert.AreEqual (fb.CurrentCooldown, COOLDOWN);
		Assert.AreEqual (fb.Cooldown, COOLDOWN);
		Assert.AreEqual (fb.Damage, DAMAGE);
	}
}
