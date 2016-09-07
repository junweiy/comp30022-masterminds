using UnityEngine;
using System.Collections;

public class ProfileMessenger {
	public static string updateProfileUrl = "http://115.146.95.82/masterminds/profile/update.py";
	public static string getProfileUrl = "http://115.146.95.82/masterminds/profile/get.py";

	public static void submitNewProfile(Profile profile) {
		WWWForm form = new WWWForm ();
		form.AddField ("profile", Profile.toJson (profile));
		WWW w = new WWW (updateProfileUrl, form);
		if (!string.IsNullOrEmpty(w.error)) {
			Debug.Log(w.error);
		}
		else {
			Debug.Log("Finished submitting profile");
		}
	}

	public static Profile updateProfileFromServer(Profile currentProfile) {
		int uid = currentProfile.userId;
		string token = "";

		UpdateRequest req = new UpdateRequest ();
		req.uid = uid;
		req.token = token;

		WWWForm form = new WWWForm ();
		form.AddField ("message", UpdateRequest.toJson(req));
		WWW w = new WWW (updateProfileUrl, form);
		if (!string.IsNullOrEmpty(w.error)) {
			Debug.Log(w.error);
			return null;
		}
		else {
			return JsonUtility.FromJson<Profile> (w.text);
		}
	}
}