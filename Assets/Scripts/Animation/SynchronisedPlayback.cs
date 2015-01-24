using UnityEngine;
using System.Collections;

public class SynchronisedPlayback : MonoBehaviour {

	// This has animationclips (Unity internal? Or maybe a collection of linked ones? Lower level anyway)
	// It connects with a remote UI, which commands start, pause, rewind, fastforward.

	AnimationState curState;

	float targetSpeed;
	int index;
	[SerializeField] CameraManager camManager;
	[SerializeField] DetectableObjectRegistry detectionRegistry;
	[SerializeField] ScreenshotViewer viewer;
	public void TakeScreenshot() {
		// This sends a signal through to the other player, which uses the camManager to get the current look direction and sends back the information.
		networkView.RPC("RequestScreenshot", RPCMode.Others, Network.player);
	}

	[RPC]
	void RequestScreenshot(NetworkPlayer player) {
		// This should find the current position and direction of the camera, and send them back to the other player
		networkView.RPC("ConfirmScreenshotPosition", player, curState.time, index, camManager.GetCurrentCameraOffset(), curState.name);
	}

	[RPC]
	void ConfirmScreenshotPosition(float absTime, int camIndex, Quaternion rotationOffset, string name) {
		// This should take a screenshot (turn it into a texture? Alongside pixel positions of all relevant objects?)
		camManager.SetCamera(camIndex);
		animation.Play(name);
		curState = animation[name];
		curState.time = absTime;
		curState.speed = 0;
		camManager.SetCameraOffset(rotationOffset);
		lastScreenshot = new ScreenshotWithMetadata(camManager.GetScreenshotCamera(), detectionRegistry);
		viewer.SetData(lastScreenshot);
	}

	public ScreenshotWithMetadata lastScreenshot;

	[SerializeField] string defName;
	public void Play() {
		if(Network.peerType != NetworkPeerType.Disconnected) {
			networkView.RPC("RemPlay", RPCMode.Others, defName, 0);
		} else {
			RemPlay(defName, 0);
		}
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
		if(Network.peerType != NetworkPeerType.Disconnected) {
			networkView.RPC("RemPause", RPCMode.Others);
		} else {
			RemPause();
		}
	}
	[RPC]
	void RemPause() {
		targetSpeed = 0;
	}
	
	public void Rewind() {
		if(Network.peerType != NetworkPeerType.Disconnected) {
			networkView.RPC("RemRewind", RPCMode.Others);
		} else {
			RemRewind();
		}
	}
	[RPC]
	void RemRewind() {
		targetSpeed = -5;
	}
	
	public void FastForward() {
		if(Network.peerType != NetworkPeerType.Disconnected) {
			networkView.RPC("RemFastForward", RPCMode.Others);
		} else {
			RemFastForward();
		}
	}
	[RPC]
	void RemFastForward() {
		targetSpeed = 5;
	}

	void Update() {
		if(curState != null && camManager.GetMode() == CameraManager.CameraMode.Eyes) {
			curState.speed = Mathf.Lerp(curState.speed, targetSpeed, Time.deltaTime * 2);
		}
		if(Input.GetKeyDown(KeyCode.S)) {
			if(Network.peerType == NetworkPeerType.Disconnected) {
				ConfirmScreenshotPosition(curState.time, index, camManager.GetCurrentCameraOffset(), curState.name);
			} else {
				TakeScreenshot();
			}
		}

	}
}