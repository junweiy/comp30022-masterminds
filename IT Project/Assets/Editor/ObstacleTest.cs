using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System;

public class ObstacleTest {

    private Obstacle o = new Obstacle(100);

    [Test]
    public void addOnDestoyActionTest() {
        Assert.AreEqual(0, o.onDestroyActions.Count);
        Action a = delegate { };
        o.AddOnDestoyAction(a);
        Assert.AreEqual(1, o.onDestroyActions.Count);
        Assert.True(o.onDestroyActions.Contains(a));
    }

    [Test]
    public void takeDamageTest() {
        Assert.AreEqual(100, o.maxHealth);
        Assert.AreEqual(100, o.currentHealth);
        bool destroyed = false;
        o.AddOnDestoyAction(delegate { destroyed = true; });

        o.TakeDamage(70);
        Assert.AreEqual(100, o.maxHealth);
        Assert.AreEqual(30, o.currentHealth);
        Assert.False(destroyed);

        o.TakeDamage(1200);
        Assert.AreEqual(100, o.maxHealth);
        Assert.AreEqual(0, o.currentHealth);
        Assert.True(destroyed);
    }
}
