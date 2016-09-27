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

	static void throwExceptionIfError(string text) {
		Debug.Log (text);
		ErrorResponse res = JsonUtility.FromJson<ErrorResponse> (text);
		if (res.code != -1 && res.code != 0 && res.message != null) {
			throw new ProfileMessagingException(res.code, res.message);
		}
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
			throwExceptionIfError (w.text);
			Debug.Log("Finished submitting profile");
		}
	}

	public static Profile getNewProfileFromServer(Profile currentProfile) {
		int uid = currentProfile.uid;
		return ProfileMessenger.getProfileById (uid);
	}

	public static Profile getProfileById(int userid) {
		string token = ProfileMessenger.password;

		ProfileUpdateRequestWithUid req = new ProfileUpdateRequestWithUid ();
		req.uid = userid;
		req.token = token;
		req.timestamp = getTimeStamp();

		WWWForm form = new WWWForm ();
		form.AddField ("message", req.toJson());
		WWW w = new WWW (getProfileUrl, form);
		// wait until complete
		while (!w.isDone) {
		}
		if (!string.IsNullOrEmpty(w.error)) {
			Debug.Log(w.error);
			return null;
		}
		else {
			throwExceptionIfError (w.text);
			var res = JsonUtility.FromJson<ProfileUpdateResponse> (w.text);
			return res.profile;
		}
	}

	public static Profile getProfileByEmail(string email) {
		string token = ProfileMessenger.password;

		ProfileUpdateRequestWithEmail req = new ProfileUpdateRequestWithEmail ();
		req.email = email;
		req.token = token;
		req.timestamp = getTimeStamp();

		WWWForm form = new WWWForm ();
		form.AddField ("message", req.toJson());
		Debug.Log (req.toJson ());
		WWW w = new WWW (getProfileUrl, form);
		// wait until complete
		while (!w.isDone) {
		}
		if (!string.IsNullOrEmpty(w.error)) {
			Debug.Log(w.error);
			return null;
		}
		else {
			throwExceptionIfError (w.text);
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
			throwExceptionIfError (w.text);
			var res = JsonUtility.FromJson<NewUserResponse> (w.text);
			return res.uid;
		}
	}





	[System.Serializable]
	class NewUserRequest {
		public string userName;
		public string email;
		public int timestamp;
		public string token;
	}

	[System.Serializable]
	class NewUserResponse {
		public int code;
		public string message;
		public int uid;
	}

	[System.Serializable]
	class ProfileUpdateRequestWithUid {
		public int uid;
		public string token;
		public int timestamp;

		public string toJson() {
			return JsonUtility.ToJson (this);
		}
	}

	[System.Serializable]
	class ProfileUpdateRequestWithEmail {
		public string email;
		public string token;
		public int timestamp;

		public string toJson() {
			return JsonUtility.ToJson (this);
		}
	}

	[System.Serializable]
	class ProfileUpdateResponse {
		public int code;
		public Profile profile;
	}

	[System.Serializable]
	class NewProfileRequest {
		public Profile newProfile;
		public int uid;
		public string token;
	}

	[System.Serializable]
	class ErrorResponse {
		public int code;
		public string message;
	}
}

