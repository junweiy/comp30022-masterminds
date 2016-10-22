using UnityEngine;
using UnityEditor;
using NUnit.Framework;

/**
 * An unit test for fire nova class
 * */
public class FireNovaTest {

	private const float COOLDOWN = 5;
	private const int DAMAGE = 30;


	[Test]
    //The class only have a constructor, so just create an new object and tests its properties.
    public void CreateTest()
	{
		FireNova fb = new FireNova();
		Assert.AreEqual (fb.CurrentCooldown, COOLDOWN);
		Assert.AreEqual (fb.Cooldown, COOLDOWN);
		Assert.AreEqual (fb.Damage, DAMAGE);
	}


}
