using UnityEngine;
using System.Collections;


[System.Serializable]
public class UpdateRequest {

	public int uid = 0;
	public string token = "";

	public static string toJson(UpdateRequest request) {
		return JsonUtility.ToJson (request);
	}
}
