using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public static class NetworkHelper {
    public static T GetObjectFromNetIdValue<T>(uint netIdValue, bool isServer) {
        NetworkInstanceId netInstanceId = new NetworkInstanceId(netIdValue);
        NetworkIdentity foundNetworkIdentity = null;
        if (isServer) {
            NetworkServer.objects.TryGetValue(netInstanceId, out foundNetworkIdentity);
        } else {
            ClientScene.objects.TryGetValue(netInstanceId, out foundNetworkIdentity);
        }

        if (foundNetworkIdentity) {
            T foundObject = foundNetworkIdentity.GetComponent<T>();
            if (foundObject != null) {
                return foundObject;
            }
        }

        return default(T);
    }
}