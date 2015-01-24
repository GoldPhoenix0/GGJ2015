using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HandsUIEnabler : MonoBehaviour {

	[SerializeField] Button play;
	[SerializeField] Button pause;
	[SerializeField] Button rewind;
	[SerializeField] Button ffword;

    SynchronisedPlayback playback;

    void SetSynchronisedPlayback (SynchronisedPlayback newPB) {
        this.playback = newPB;
        play.onClick.AddListener(() => { playback.Play(); });
		/*pause.onClick.AddListener(playback.Pause);
		rewind.onClick.AddListener(playback.Rewind);
		ffword.onClick.AddListener(playback.FastForward);
        */
        Debug.Log("Setting up playback stuff");
	}
}
