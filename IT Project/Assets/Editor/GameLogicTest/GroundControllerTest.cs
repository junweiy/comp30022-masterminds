using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class GroundControllerTest {

	[Test]
	public void GroundTest()
	{
        //Arrange
        GroundController ground = new GroundController();

        Assert.Equals(ground.TimePassed, 0);
	}
}
