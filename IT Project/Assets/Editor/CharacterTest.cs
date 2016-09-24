using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class CharacterTest {

    Character c1 = new Character();
    Character c2 = new Character();

    //[SetUp]
    //public void setUp() {

    //}

	[Test]
	public void addSpellTest()
	{
        var fireball = new FireBall();
        c1.addSpell(fireball);

        Assert.AreEqual(c1.spells.Count, 1);
        Assert.AreEqual(c1.spells[0], fireball);

        c1.addSpell(fireball);
        Assert.AreEqual(c1.spells.Count, 1);
        Assert.AreEqual(c1.spells[0], fireball);

        var anotherFireball = new FireBall();
        c1.addSpell(anotherFireball);
        Assert.AreEqual(c1.spells.Count, 1);
        Assert.AreEqual(c1.spells[0], fireball);

        var firenova = new FireNova();
        c1.addSpell(firenova);
        Assert.AreEqual(c1.spells.Count, 2);
        Assert.True(c1.spells.Contains(firenova));
	}

    [Test]
    public void addItemTest() {
        Assert.Fail();
    }

    [Test]
    public void hasSpaceForItemTest() {
        Assert.Fail();
    }

    [Test]
    public void takeDamageTest() {
        Assert.Fail();
    }

    [Test]
    public void deductCoinTest() {
        Assert.Fail();
    }

    [Test]
    public void addScoreTest() {
        Assert.Fail();
    }

    [Test]
    public void deductScoreTest() {
        Assert.Fail();
    }


}
