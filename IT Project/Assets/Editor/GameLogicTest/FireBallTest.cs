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
		Assert.AreEqual (fb.CurrentCooldown, COOLDOWN);
		Assert.AreEqual (fb.Cooldown, COOLDOWN);
		Assert.AreEqual (fb.Damage, DAMAGE);
	}
}
