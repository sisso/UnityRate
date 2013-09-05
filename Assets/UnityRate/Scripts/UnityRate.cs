using UnityEngine;
using System.Collections;

using System.Runtime.InteropServices;

public class UnityRate {
	private string prefsPrefixe = "unityrate";
	
	private string appName = "undefinedAppName";
	
	public void IncreaseCount() {
		var amount = int.Parse(PlayerPrefs.GetString(prefsPrefixe+".count", "0"));
		PlayerPrefs.SetString(prefsPrefixe+".count", (amount+1).ToString());
	}
	
	public void SendToRating() {
		Debug.Log("SendToRating.started");
#if UNITY_ANDROID		
		AndroidJavaClass appClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = appClass.GetStatic<AndroidJavaObject>("currentActivity");
		
		// uri = Uri.parse("market://details?id=" + APP_PNAME)
		AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
		AndroidJavaObject uri = uriClass.CallStatic<AndroidJavaObject>("parse", "market://details?id=" + appName);
		
		// intent = new Intent(Intent.ACTION_VIEW, uri)
		AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
		AndroidJavaObject intent = new AndroidJavaObject("android.content.Intent", intentClass.GetStatic<AndroidJavaObject>("ACTION_VIEW"), uri);
		
		// activity.startActivity(intent);
		activity.Call("startActivity", intent);
#endif
		Debug.Log("SendToRating.finished");
	}
}