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

        Assert.AreEqual(1, c1.spells.Count);
        Assert.AreEqual(fireball, c1.spells[0]);

        c1.addSpell(fireball);
        Assert.AreEqual(1, c1.spells.Count);
        Assert.AreEqual(fireball, c1.spells[0]);

        var anotherFireball = new FireBall();
        c1.addSpell(anotherFireball);
        Assert.AreEqual(1, c1.spells.Count);
        Assert.AreEqual(fireball, c1.spells[0]);

        var firenova = new FireNova();
        c1.addSpell(firenova);
        Assert.AreEqual(2, c1.spells.Count);
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
