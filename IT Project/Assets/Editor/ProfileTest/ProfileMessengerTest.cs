using UnityEngine;
using System.Collections;
using UnityEditor;
using NUnit.Framework;
using System;

/**
 *  This class is a unit test for player profile messaging system.
 */
public class ProfileMessengerTest {

	private const string DummyUsername = "UnitTest1";
	private const string DummmyEmail = "unit1@test.com";

	[SetUp]
	public void SetUp() {
		
	}

	[Test]
	[ExpectedException(typeof(ProfileMessagingException))]
	public void NullUserTest()
	{
		ProfileMessenger.CreateNewUser("","");
	}

	/*  It tests the creation of a player profle by creating and get.*/
	[Test]
	public void CreateAndGetTest() {

		//create a user and get it and see if they have same user id
		int pid;
		try{
			pid = (int)ProfileMessenger.CreateNewUser(DummyUsername,DummmyEmail);
		}catch(ProfileMessagingException e) { return;}

		Profile p = ProfileMessenger.GetProfileByEmail(DummmyEmail);
		Assert.AreEqual (p.Uid, pid);

	}


	[Test]
	public void SubmitProfileTest() {

		//

	}


}
