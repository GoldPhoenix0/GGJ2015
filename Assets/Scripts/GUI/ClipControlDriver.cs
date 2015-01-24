using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ClipControlDriver : MonoBehaviour {

	[SerializeField] SynchronisedPlayback playback;
	[SerializeField] Image radialTime;
	[SerializeField] Text timeTextField;

	void Update() {
		string minutes = Mathf.Floor(playback.synchronisedAbsTime / 60).ToString("00");
		string seconds = Mathf.Floor(playback.synchronisedAbsTime % 60).ToString("00");
		radialTime.fillAmount = playback.synchronisedTime;
		timeTextField.text = minutes + ":" + seconds;
	}
}
