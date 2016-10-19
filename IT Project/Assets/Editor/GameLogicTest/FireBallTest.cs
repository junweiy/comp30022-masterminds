using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class FireBallTest {


	private const float COOLDOWN = 2;
	private const int DAMAGE = 20;


	[Test]
	public void CreateTest()
	{
		FireBall fb = new FireBall();
		Assert.AreEqual (fb.currentCooldown, COOLDOWN);
		Assert.AreEqual (fb.cooldown, COOLDOWN);
		Assert.AreEqual (fb.damage, DAMAGE);
	}
}
