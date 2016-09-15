using UnityEngine;
using System.Collections;

public class ProfileMessenger {
	public static string updateProfileUrl = "http://115.146.95.82/masterminds/profile/update.py";
	public static string getProfileUrl = "http://115.146.95.82/masterminds/profile/getProfile.py";
	public static string newUserUrl = "http://115.146.95.82/masterminds/profile/newUser.py";
	public static string password = "overdue";

	public static int getTimeStamp() {
		return System.DateTime.Now.Millisecond;
	}

	public static void submitNewProfile(Profile profile) {
		WWWForm form = new WWWForm ();

		NewProfileRequest m = new NewProfileRequest ();
		m.newProfile = profile;
		m.uid = profile.uid;
		m.token = ProfileMessenger.password;

		form.AddField ("profile", JsonUtility.ToJson(m));
		WWW w = new WWW (updateProfileUrl, form);
		// wait until complete
		while (!w.isDone) {
		}
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
		req.timestamp = getTimeStamp();

		WWWForm form = new WWWForm ();
		form.AddField ("message", ProfileUpdateRequest.toJson(req));
		WWW w = new WWW (getProfileUrl, form);
		// wait until complete
		while (!w.isDone) {
		}
		if (!string.IsNullOrEmpty(w.error)) {
			Debug.Log(w.error);
			return null;
		}
		else {
			var res = JsonUtility.FromJson<ProfileUpdateResponse> (w.text);
			return res.profile;
		}
	}

	public static int? createNewUser(string userName, string email) {
		var req = new NewUserRequest ();
		req.email = email;
		req.userName = userName;
		req.timestamp = getTimeStamp();
		req.token = password;

		WWWForm form = new WWWForm ();
		form.AddField ("message", JsonUtility.ToJson (req));
		WWW w = new WWW (newUserUrl, form);
		// wait until complete
		while (!w.isDone) {
		}
		if (!string.IsNullOrEmpty(w.error)) {
			Debug.Log(w.error);
			return null;
		}
		else {
			var res = JsonUtility.FromJson<NewUserResponse> (w.text);
			return res.uid;
		}
	}
}

[System.Serializable]
public class NewUserRequest {
	public string userName;
	public string email;
	public int timestamp;
	public string token;
}

[System.Serializable]
public class NewUserResponse {
	public int code;
	public string message;
	public int uid;
}

[System.Serializable]
public class ProfileUpdateRequest {

	public int uid;
	public string token;
	public int timestamp;

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
public class NewProfileRequest {
	public Profile newProfile;
	public int uid;
	public string token;
}

