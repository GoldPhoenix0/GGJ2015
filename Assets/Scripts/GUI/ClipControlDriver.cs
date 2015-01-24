using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ClipControlDriver : MonoBehaviour {

	[SerializeField] SynchronisedPlayback playback;
	[SerializeField] Image radialTime;
	[SerializeField] Text timeTextField;

	void Update() {
		radialTime.fillAmount = playback.synchronisedTime;
	}
}
