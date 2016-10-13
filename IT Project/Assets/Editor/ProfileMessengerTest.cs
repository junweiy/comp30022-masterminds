using UnityEngine;
using System.Collections;

using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System;

public class ProfileMessengerTest {

	private string DummyUsername;
	private string DummmyEmail;

	[SetUp]
	public void SetUp() {
		
	}


	[Test]
	public void CreateAndGetTest() {

		//first test null input
		//Assert.AreEqual (ProfileMessenger.createNewUser("",""),null);

		//create a user and get it and see if they have same user id
		int pid = (int)ProfileMessenger.createNewUser("UnitTest1","unit1@test.com");
		Profile p = ProfileMessenger.GetProfileByEmail("unit1@test.com");
		Assert.AreEqual (p.uid, pid);



	}


	[Test]
	public void SubmitProfileTest() {

		//

	}


}
