using UnityEngine;

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public class UnityRate {
	private string prefsPrefixe = "unityrate";
	
	public string appId = "undefinedAppId";
	
	public bool debug = false;
	
	public int minCount = 15;
	
	public int minDays = 15;
	
	public int askLaterDays = 7;
	
	public string version = "undefinedVersion";
	
#if UNITY_IPHONE
	[DllImport("__Internal", CharSet = CharSet.Ansi)]
    private static extern void UnityRate_sendToRate([In, MarshalAs(UnmanagedType.LPStr)]string id);
#endif
	
	public bool Check() {
		if (PlayerPrefs.GetString(prefsPrefixe+".disabled", "false") == "true") {
			if (debug) Debug.Log("UnityRate.Check rejected by disabled");
			return false;
		}
		
		if (PlayerPrefs.GetString(prefsPrefixe+".version", "") == version) {
			if (debug) Debug.Log("UnityRate.Check rejected by already rated the version "+version);
			return false;
		}
		
		var count = int.Parse(PlayerPrefs.GetString(prefsPrefixe+".count", "0")) + 1;
		if (debug) Debug.Log("UnityRate.Check increasing count to "+count);
		PlayerPrefs.SetString(prefsPrefixe+".count", count.ToString());
		
		if (count < minCount) {
			if (debug) Debug.Log("UnityRate.Check rejected by minCount of "+count);
			return false;
		}
		
		var dateStr = PlayerPrefs.GetString(prefsPrefixe+".date", "null");
		if (dateStr == "null") {
			if (debug) Debug.Log("UnityRate.Check no firstdate found, setting it to now");
			PlayerPrefs.SetString(prefsPrefixe+".date", DateTime.Now.ToString("o"));
			return false;
		}
		
		DateTime date = DateTime.Parse(dateStr);
		int days = (DateTime.Now - date).Days;
		if (days < minDays) {
			if (debug) Debug.Log("UnityRate.Check rejected by minDays of "+days);
			return false;
		}

		if (debug) Debug.Log("UnityRate.Check success");
		
		return true;
	}
	
	public void RegisterUserRated() {
		if (debug) Debug.Log("UnityRate.RegisterUserRated for version "+version);
		PlayerPrefs.SetString(prefsPrefixe+".version", version);
		// set date as now so we give a calm time if the version is updated
		PlayerPrefs.SetString(prefsPrefixe+".date", DateTime.Now.ToString("o"));
	}
	
	public void AskLater() {
		// change ask later to match the new date
		var date = DateTime.Now.AddDays(askLaterDays - minDays);
		if (debug) Debug.Log("UnityRate.AskLater to "+date);
		PlayerPrefs.SetString(prefsPrefixe+".date", date.ToString("o"));
	}
	
	public void DisableRate() {
		if (debug) Debug.Log("UnityRate.DisableRate");
		PlayerPrefs.SetString(prefsPrefixe+".disabled", "true");
	}
	
	public void SendToRating() {
		if (debug) Debug.Log("SendToRating.started");
#if UNITY_EDITOR 
		// fake it
		Debug.Log("Fake rating...");
#elif UNITY_ANDROID		
		AndroidJavaClass appClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = appClass.GetStatic<AndroidJavaObject>("currentActivity");
		
		// uri = Uri.parse("market://details?id=" + APP_PNAME)
		AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
		AndroidJavaObject uri = uriClass.CallStatic<AndroidJavaObject>("parse", "market://details?id=" + appId);
		
		// intent = new Intent(Intent.ACTION_VIEW, uri)
		AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
		AndroidJavaObject intent = new AndroidJavaObject("android.content.Intent", intentClass.GetStatic<AndroidJavaObject>("ACTION_VIEW"), uri);
		
		// activity.startActivity(intent);
		activity.Call("startActivity", intent);
#elif UNITY_IPHONE
		UnityRate_sendToRate(appId);
#endif
		
		RegisterUserRated();
		
		if (debug) Debug.Log("SendToRating.finished");
	}
	
	public void Reset() {
		if (debug) Debug.Log("UnityRate.Reset");
		PlayerPrefs.DeleteKey(prefsPrefixe+".date");
		PlayerPrefs.DeleteKey(prefsPrefixe+".disabled");
		PlayerPrefs.DeleteKey(prefsPrefixe+".count");
		PlayerPrefs.DeleteKey(prefsPrefixe+".version");
	}
	
	public void SetFirstDate(DateTime date) {
		if (debug) Debug.Log("UnityRate.SetFirstDate to "+date);
		PlayerPrefs.SetString(prefsPrefixe+".date", date.ToString("o"));
	}
}