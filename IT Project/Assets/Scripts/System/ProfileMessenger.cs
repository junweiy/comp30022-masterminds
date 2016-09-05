using UnityEngine;
using System.Collections;

public class ProfileMessenger {
	public static string updateProfileUrl = "http://masterminds.com/profile/update"; // TODO update when server is set up
	public static string getProfileUrl = "http://masterminds.com/profile/get"; // TODO update when server is set up

	public static void submitNewProfile(Profile profile) {
		WWWForm form = new WWWForm ();
		form.AddField ("profile", Profile.toJson (profile));
		WWW w = new WWW (updateProfileUrl, form);
		if (!string.IsNullOrEmpty(w.error)) {
			Debug.Log(w.error);
		}
		else {
			Debug.Log("Finished Uploading Screenshot");
		}
	}

	public static void updateProfileFromServer() {
		// TODO
	}
}
