using UnityEngine;
using System.Collections;

public class UnityRateTestGui : MonoBehaviour {
	private UnityRate rate;
	private string msg = "Wel come";
	private int version = 1;
	
	void OnGUI() {
		var w = Screen.width;
		var h = Screen.height/10;
		GUILayout.BeginArea(new Rect(0,0, w, Screen.height));
		GUILayout.Label("Version: "+version);
		GUILayout.Label(msg);
		if (GUILayout.Button("Create", GUILayout.Height(h))) {
			rate = new UnityRate();
			rate.version = version.ToString();
			rate.debug = true;
			msg = "Created";
		}
		if (GUILayout.Button("Check", GUILayout.Height(h))) {
			msg = rate.Check() ? "Yes" : "No";
		}
		if (GUILayout.Button("AskLater", GUILayout.Height(h))) {
			rate.AskLater();
			msg = "ok";
		}
		if (GUILayout.Button("DisableRate", GUILayout.Height(h))) {
			rate.DisableRate();
			msg = "ok";
		}
		if (GUILayout.Button("Reset All", GUILayout.Height(h))) {
			rate.Reset();
			version = 1;
			msg = "ok";
		}
		if (GUILayout.Button("Fake First Date", GUILayout.Height(h))) {
			rate.SetFirstDate(System.DateTime.Now.AddDays(-rate.minDays * 2));
			msg = "ok";
		}
		if (GUILayout.Button("Increase App version", GUILayout.Height(h))) {
			version = version + 1;
			rate.version = version.ToString();
			msg = "ok";
		}
		if (GUILayout.Button("Send to rating", GUILayout.Height(h))) {
			rate.SendToRating();
			msg = "Sent";
		}
		
		GUILayout.EndArea();
	}
}
