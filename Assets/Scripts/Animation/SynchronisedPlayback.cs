using UnityEngine;
using System.Collections;

public class SynchronisedPlayback : MonoBehaviour {

	// This has animationclips (Unity internal? Or maybe a collection of linked ones? Lower level anyway)
	// It connects with a remote UI, which commands start, pause, rewind, fastforward.

	AnimationState curState;

	float targetSpeed;
	int index;
	[SerializeField] CameraManager camManager;

	public void TakeScreenshot() {
		// This sends a signal through to the other player, which uses the camManager to get the current look direction and sends back the information.
		networkView.RPC("RequestScreenshot", RPCMode.Others, Network.player);
	}

	[RPC]
	void RequestScreenshot(NetworkPlayer player) {
		// This should find the current position and direction of the camera, and send them back to the other player
		networkView.RPC("ConfirmScreenshot", player, curState.time, index, camManager.GetCurrentCameraOffset());
	}

	[RPC]
	void ConfirmScreenshotPosition(float absTime, int camIndex, Quaternion rotationOffset) {
		// This should take a screenshot (turn it into a texture? Alongside pixel positions of all relevant objects?)

	}
	[SerializeField] string defName;
	public void Play() {
		networkView.RPC("RemPlay", RPCMode.Others, defName, 0);
	}
	/*
	public void Play(string name, int cameraIndex) {
		networkView.RPC("RemPlay", RPCMode.Others, name, cameraIndex);
	}*/
	[RPC]
	void RemPlay(string name, int cameraIndex) {
		if(!animation.isPlaying) {
			animation.Play(name);
		}
		index = cameraIndex;
		camManager.SetCamera(cameraIndex);
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