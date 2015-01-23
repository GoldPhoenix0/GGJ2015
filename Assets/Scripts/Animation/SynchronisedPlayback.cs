using UnityEngine;
using System.Collections;

public class SynchronisedPlayback : MonoBehaviour {

	// This has animationclips (Unity internal? Or maybe a collection of linked ones? Lower level anyway)
	// It connects with a remote UI, which commands start, pause, rewind, fastforward.

	AnimationState curState;

	float targetSpeed;

	public void Play(string name) {
		networkView.RPC("RemPlay", RPCMode.Others, name);
	}
	[RPC]
	void RemPlay(string name) {
		if(!animation.isPlaying) {
			animation.Play(name);
		}
		curState = animation[name];
		targetSpeed = 1;
	}
	public void Pause() {
		networkView.RPC("RemPause", RPCMode.Others);
	}
	[RPC]
	void RemPause() {
		targetSpeed = 0;
	}
	
	public void Rewind() {
		networkView.RPC("RemRewind", RPCMode.Others);
	}
	[RPC]
	void RemRewind() {
		targetSpeed = -5;
	}
	
	public void FastForward() {
		networkView.RPC("RemFastForward", RPCMode.Others);
	}
	[RPC]
	void RemFastForward() {
		targetSpeed = 5;
	}

	void Update() {
		if(curState != null) {
			curState.speed = Mathf.Lerp(curState.speed, targetSpeed, Time.deltaTime * 2);
		}
	}
}