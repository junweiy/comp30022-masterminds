using UnityEngine;

// Static class for fetching/updating profile with the server
public static class ProfileMessenger {
    public static string UpdateProfileUrl = "http://115.146.95.82/masterminds/profile/update.py";
    public static string GetProfileUrl = "http://115.146.95.82/masterminds/profile/getProfile.py";
    public static string NewUserUrl = "http://115.146.95.82/masterminds/profile/newUser.py";
    public static string Password = "overdue";

    // Gets the timestamp
    public static int GetTimeStamp() {
        return System.DateTime.Now.Millisecond;
    }

    private static void ThrowExceptionIfError(string text) {
        Debug.Log(text);
        ErrorResponse res = JsonUtility.FromJson<ErrorResponse>(text);
        if (res.code != -1 && res.code != 0 && res.message != null) {
            throw new ProfileMessagingException(res.code, res.message);
        }
    }

    // Submits a new profile to the server, overwriting existing profile on server
    public static void SubmitNewProfile(Profile profile) {
        WWWForm form = new WWWForm();

        NewProfileRequest m = new NewProfileRequest();
        m.newProfile = profile;
        m.uid = profile.uid;
        m.timestamp = GetTimeStamp();
        m.token = ProfileMessenger.Password;
        form.AddField("message", JsonUtility.ToJson(m));
        WWW w = new WWW(UpdateProfileUrl, form);
        // wait until complete
        while (!w.isDone) {}
        if (!string.IsNullOrEmpty(w.error)) {
            Debug.Log(w.error);
        } else {
            Debug.Log(w.text);
            ThrowExceptionIfError(w.text);
            Debug.Log("Finished submitting profile");
            GlobalState.LoadProfileWithUid(profile.uid);
        }
    }

    // Gets the latest version of the given profile form the server
    public static Profile GetNewProfileFromServer(Profile currentProfile) {
        int uid = currentProfile.uid;
        return ProfileMessenger.GetProfileById(uid);
    }

    // Gets the profile from server given the user id
    public static Profile GetProfileById(int userid) {
        string token = ProfileMessenger.Password;

        ProfileUpdateRequestWithUid req = new ProfileUpdateRequestWithUid();
        req.uid = userid;
        req.token = token;
        req.timestamp = GetTimeStamp();

        WWWForm form = new WWWForm();
        form.AddField("message", JsonUtility.ToJson(req));
        WWW w = new WWW(GetProfileUrl, form);
        // wait until complete
        while (!w.isDone) {}
        if (!string.IsNullOrEmpty(w.error)) {
            Debug.Log(w.error);
            return null;
        } else {
            ThrowExceptionIfError(w.text);
            var res = JsonUtility.FromJson<ProfileUpdateResponse>(w.text);
            return res.profile;
        }
    }

    // Gets the profile from server given the email of the profile
    public static Profile GetProfileByEmail(string email) {
        string token = ProfileMessenger.Password;

        ProfileUpdateRequestWithEmail req = new ProfileUpdateRequestWithEmail();
        req.email = email;
        req.token = token;
        req.timestamp = GetTimeStamp();

        WWWForm form = new WWWForm();
        form.AddField("message", JsonUtility.ToJson(req));
        WWW w = new WWW(GetProfileUrl, form);
        // wait until complete
        while (!w.isDone) {}
        if (!string.IsNullOrEmpty(w.error)) {
            Debug.Log(w.error);
            return null;
        } else {
            ThrowExceptionIfError(w.text);
            var res = JsonUtility.FromJson<ProfileUpdateResponse>(w.text);
            return res.profile;
        }
    }

    // Request to create a new user in the server, returns the user id of new user if successful
    public static int? CreateNewUser(string userName, string email) {
        var req = new NewUserRequest();
        req.email = email;
        req.userName = userName;
        req.timestamp = GetTimeStamp();
        req.token = Password;
        WWWForm form = new WWWForm();
        form.AddField("message", JsonUtility.ToJson(req));
        WWW w = new WWW(NewUserUrl, form);
        // wait until complete
        while (!w.isDone) {}
        if (!string.IsNullOrEmpty(w.error)) {
            Debug.Log(w.error);
            return null;
        } else {
            ThrowExceptionIfError(w.text);
            var res = JsonUtility.FromJson<NewUserResponse>(w.text);
            return res.uid;
        }
    }

    // Classes for JSON requests and responses

    [System.Serializable]
    private class NewUserRequest {
        public string userName;
        public string email;
        public int timestamp;
        public string token;
    }

    [System.Serializable]
    private class NewUserResponse {
        public int code;
        public string message;
        public int uid;
    }

    [System.Serializable]
    private class ProfileUpdateRequestWithUid {
        public int uid;
        public string token;
        public int timestamp;
    }

    [System.Serializable]
    private class ProfileUpdateRequestWithEmail {
        public string email;
        public string token;
        public int timestamp;
    }

    [System.Serializable]
    private class ProfileUpdateResponse {
        public int code;
        public Profile profile;
    }

    [System.Serializable]
    private class NewProfileRequest {
        public Profile newProfile;
        public int uid;
        public string token;
        public int timestamp;
    }

    [System.Serializable]
    private class ErrorResponse {
        public int code;
        public string message;
    }
}