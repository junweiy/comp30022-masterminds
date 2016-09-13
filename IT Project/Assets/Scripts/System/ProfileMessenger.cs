using UnityEngine;
using System.Collections;

public class ProfileMessenger {
	public static string updateProfileUrl = "http://115.146.95.82/masterminds/profile/update.py";
	public static string getProfileUrl = "http://115.146.95.82/masterminds/profile/getProfile.py";
	public static string password = "overdue";

	public static void submitNewProfile(Profile profile) {
		WWWForm form = new WWWForm ();

		NewUserRequest m = new NewUserRequest ();
		m.newProfile = profile;
		m.uid = profile.uid;
		m.token = ProfileMessenger.password;

		form.AddField ("profile", JsonUtility.ToJson(m));
		WWW w = new WWW (updateProfileUrl, form);
		if (!string.IsNullOrEmpty(w.error)) {
			Debug.Log(w.error);
		}
		else {
			Debug.Log("Finished submitting profile");
		}
	}

	public static Profile getNewProfileFromServer(Profile currentProfile) {
		int uid = currentProfile.uid;
		return ProfileMessenger.getProfileById (uid);
	}

	public static Profile getProfileById(int userid) {
		string token = ProfileMessenger.password;

		ProfileUpdateRequest req = new ProfileUpdateRequest ();
		req.uid = userid;
		req.token = token;
		req.timestamp = 11111;

		WWWForm form = new WWWForm ();
		form.AddField ("message", ProfileUpdateRequest.toJson(req));
		Debug.Log (ProfileUpdateRequest.toJson (req));
		WWW w = new WWW (getProfileUrl, form);
		while (!w.isDone) {
		}
		if (!string.IsNullOrEmpty(w.error)) {
			Debug.Log(w.error);
			return null;
		}
		else {
			Debug.Log (w.text);
			var res = JsonUtility.FromJson<ProfileUpdateResponse> (w.text);
			return res.profile;
		}
	}
}

[System.Serializable]
public class ProfileUpdateRequest {

	public int uid = 0;
	public string token = "";
	public int timestamp = 11111;

	public static string toJson(ProfileUpdateRequest request) {
		return JsonUtility.ToJson (request);
	}
}

[System.Serializable]
public class ProfileUpdateResponse {
	public int code;
	public Profile profile;
}

[System.Serializable]
public class NewUserRequest {
	public Profile newProfile;
	public int uid;
	public string token;
}

