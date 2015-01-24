using UnityEngine;
using System.Collections;

public class HackyCamSetter : MonoBehaviour {

	// Later this should work more cleanly (considering DK2 has a different resolution)
	CameraManager.CameraMode mode;
	[SerializeField] CameraManager manager;

	void Start() {
		mode = CameraManager.CameraMode.Hands;
		if(Screen.currentResolution.width == 1280 && Screen.currentResolution.height == 800) {
			mode = CameraManager.CameraMode.Eyes;
		}
		manager.InitialiseCamera(mode);
	}
}
