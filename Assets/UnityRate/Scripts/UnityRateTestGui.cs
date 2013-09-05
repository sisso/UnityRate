using UnityEngine;
using System.Collections;

public class UnityRateTestGui : MonoBehaviour {
	private UnityRate rate;
	private string msg = "Wel come";
	
	void OnGUI() {
		var w = Screen.width;
		var h = Screen.height/5;
		GUILayout.BeginArea(new Rect(0,0, w, Screen.height));
		GUILayout.Label(msg);
		if (GUILayout.Button("Create", GUILayout.Height(h))) {
			rate = new UnityRate();
			msg = "Created";
		}
		if (GUILayout.Button("Send to rating", GUILayout.Height(h))) {
			rate.SendToRating();
			msg = "Sent";
		}
		
		GUILayout.EndArea();
	}
}
