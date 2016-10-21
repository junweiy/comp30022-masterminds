using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class FireNovaTest {

	private const float COOLDOWN = 5;
	private const int DAMAGE = 30;


	[Test]
	public void CreateTest()
	{
		FireNova fb = new FireNova();
		Assert.AreEqual (fb.CurrentCooldown, COOLDOWN);
		Assert.AreEqual (fb.Cooldown, COOLDOWN);
		Assert.AreEqual (fb.Damage, DAMAGE);
	}


}
