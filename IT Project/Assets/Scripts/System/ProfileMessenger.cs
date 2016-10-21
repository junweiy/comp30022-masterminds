using UnityEngine;
using System.Collections;

public static class ProfileMessenger {
    public static string UpdateProfileUrl = "http://115.146.95.82/masterminds/profile/update.py";
    public static string GetProfileUrl = "http://115.146.95.82/masterminds/profile/getProfile.py";
    public static string NewUserUrl = "http://115.146.95.82/masterminds/profile/newUser.py";
    public static string Password = "overdue";

    public static int GetTimeStamp() {
        return System.DateTime.Now.Millisecond;
    }

    private static void ThrowExceptionIfError(string text) {
        Debug.Log(text);
        ErrorResponse res = JsonUtility.FromJson<ErrorResponse>(text);
        if (res.Code != -1 && res.Code != 0 && res.Message != null) {
            throw new ProfileMessagingException(res.Code, res.Message);
        }
    }

    public static void SubmitNewProfile(Profile profile) {
        WWWForm form = new WWWForm();

        NewProfileRequest m = new NewProfileRequest();
        m.NewProfile = profile;
        m.Uid = profile.Uid;
        m.Timestamp = GetTimeStamp();
        m.Token = ProfileMessenger.Password;

        form.AddField("message", JsonUtility.ToJson(m));
        WWW w = new WWW(UpdateProfileUrl, form);
        // wait until complete
        while (!w.isDone) {}
        if (!string.IsNullOrEmpty(w.error)) {
            Debug.Log(w.error);
        }
        else {
            Debug.Log(w.text);
            ThrowExceptionIfError(w.text);
            Debug.Log("Finished submitting profile");
            GlobalState.LoadProfileWithUid(profile.Uid);
        }
    }

    public static Profile GetNewProfileFromServer(Profile currentProfile) {
        int uid = currentProfile.Uid;
        return ProfileMessenger.GetProfileById(uid);
    }

    public static Profile GetProfileById(int userid) {
        string token = ProfileMessenger.Password;

        ProfileUpdateRequestWithUid req = new ProfileUpdateRequestWithUid();
        req.Uid = userid;
        req.Token = token;
        req.Timestamp = GetTimeStamp();

        WWWForm form = new WWWForm();
        form.AddField("message", req.ToJson());
        WWW w = new WWW(GetProfileUrl, form);
        // wait until complete
        while (!w.isDone) {}
        if (!string.IsNullOrEmpty(w.error)) {
            Debug.Log(w.error);
            return null;
        }
        else {
            ThrowExceptionIfError(w.text);
            var res = JsonUtility.FromJson<ProfileUpdateResponse>(w.text);
            return res.Profile;
        }
    }

    public static Profile GetProfileByEmail(string email) {
        string token = ProfileMessenger.Password;

        ProfileUpdateRequestWithEmail req = new ProfileUpdateRequestWithEmail();
        req.Email = email;
        req.Token = token;
        req.Timestamp = GetTimeStamp();

        WWWForm form = new WWWForm();
        form.AddField("message", req.ToJson());
        Debug.Log(req.ToJson());
        WWW w = new WWW(GetProfileUrl, form);
        // wait until complete
        while (!w.isDone) {}
        if (!string.IsNullOrEmpty(w.error)) {
            Debug.Log(w.error);
            return null;
        }
        else {
            ThrowExceptionIfError(w.text);
            var res = JsonUtility.FromJson<ProfileUpdateResponse>(w.text);
            return res.Profile;
        }
    }

    public static int? CreateNewUser(string userName, string email) {
        var req = new NewUserRequest();
        req.Email = email;
        req.UserName = userName;
        req.Timestamp = GetTimeStamp();
        req.Token = Password;
        WWWForm form = new WWWForm();
        form.AddField("message", JsonUtility.ToJson(req));
        WWW w = new WWW(NewUserUrl, form);
        // wait until complete
        while (!w.isDone) {}
        if (!string.IsNullOrEmpty(w.error)) {
            Debug.Log(w.error);
            return null;
        }
        else {
            ThrowExceptionIfError(w.text);
            var res = JsonUtility.FromJson<NewUserResponse>(w.text);
            return res.Uid;
        }
    }


    [System.Serializable]
    private class NewUserRequest {
        public string UserName;
        public string Email;
        public int Timestamp;
        public string Token;
    }

    [System.Serializable]
    private class NewUserResponse {
        public int Code;
        public string Message;
        public int Uid;
    }

    [System.Serializable]
    private class ProfileUpdateRequestWithUid {
        public int Uid;
        public string Token;
        public int Timestamp;

        public string ToJson() {
            return JsonUtility.ToJson(this);
        }
    }

    [System.Serializable]
    private class ProfileUpdateRequestWithEmail {
        public string Email;
        public string Token;
        public int Timestamp;

        public string ToJson() {
            return JsonUtility.ToJson(this);
        }
    }

    [System.Serializable]
    private class ProfileUpdateResponse {
        public int Code;
        public Profile Profile;
    }

    [System.Serializable]
    private class NewProfileRequest {
        public Profile NewProfile;
        public int Uid;
        public string Token;
        public int Timestamp;
    }

    [System.Serializable]
    private class ErrorResponse {
        public int Code;
        public string Message;
    }
}