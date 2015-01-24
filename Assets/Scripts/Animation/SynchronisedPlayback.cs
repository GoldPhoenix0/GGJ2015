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
	[SerializeField] DatabaseManager dbm;
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

	public void SelectFootage(GeneralMetadata data) {
		currentData = data;
	}

	[RPC]
	void LoadFootage(string dataKey) {
		SelectFootage(dbm.GetMetadata(dataKey));
	}

	[SerializeField] GeneralMetadata currentData;
	public void Play() {
		if(Network.peerType != NetworkPeerType.Disconnected) {
			networkView.RPC("RemPlay", RPCMode.Others, currentData.animationName, currentData.cameraIndex, currentData.startTime, currentData.endTime);
		} else {
			RemPlay(currentData.animationName, currentData.cameraIndex, currentData.startTime, currentData.endTime);
		}
	}

	[RPC]
	void RemPlay(string name, int cameraIndex, float startTime, float endTime) {
		if(!animation.isPlaying) {
			animation.Play(name);
		}
		// Stop the animation, do some voodoo to treat start and end properly (trick out the absolute and relative time values)
		index = cameraIndex;
		camManager.SetCamera(cameraIndex);
		curState = animation[name];
		curState.time = startTime;
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

	public void StepForward() {
		if(Network.peerType != NetworkPeerType.Disconnected) {
			networkView.RPC("RemStepForward", RPCMode.Others);
		} else {
			RemStepForward();
		}
	}
	[RPC]
	void RemStepForward() {
		curState.time += 0.2f;
	}
	public void StepBack() {
		if(Network.peerType != NetworkPeerType.Disconnected) {
			networkView.RPC("RemStepBack", RPCMode.Others);
		} else {
			RemStepBack();
		}
	}
	[RPC]
	void RemStepBack() {
		curState.time -= 0.2f;
	}
	public float synchronisedTime;
	public float synchronisedAbsTime;
	[RPC]
	void UpdateNormalisedTime(float normTime, float absTime) {
		synchronisedTime = normTime;
		synchronisedAbsTime = absTime;
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
		if(Network.peerType == NetworkPeerType.Disconnected && curState != null) {
			UpdateNormalisedTime(curState.normalizedTime, curState.time);
		}
		if(camManager.GetMode() == CameraManager.CameraMode.Eyes && curState != null) {
			networkView.RPC ("UpdateNormalisedTime", RPCMode.Others, curState.normalizedTime, curState.time);
		}

	}
}