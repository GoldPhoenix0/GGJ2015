using UnityEngine;
using System.Collections;

public class HandsUIEnabler : MonoBehaviour {

	SynchronisedPlayback playback;

	void SetSynchronisedPlayback (SynchronisedPlayback playback) {
		this.playback = playback;
	}
}
