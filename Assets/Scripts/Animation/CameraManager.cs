using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CameraManager : MonoBehaviour {
	[System.Serializable]
	public class CameraPosition {
		public Transform trans;
		public Renderer rend;
	}
	public enum CameraMode {
		Eyes,
		Hands
	}
	[SerializeField]
	List<CameraPosition> camTransforms = new List<CameraPosition>();

	GameObject currentCameraHook;
	
	[SerializeField]GameObject eyesObject;
	[SerializeField]GameObject handsObject;

	CameraMode curMode;

	public void InitialiseCamera(CameraMode mode) {
		switch(mode) {
		case CameraMode.Eyes:
			currentCameraHook = eyesObject;
			break;
		case CameraMode.Hands:
			currentCameraHook = handsObject;
			break;
		}
		currentCameraHook.SetActive(true);
		curMode = mode;
	}

	public Quaternion GetCurrentCameraOffset() {
		if(curMode == CameraMode.Eyes) {
			return currentCameraHook.GetComponent<LocalRotationQuery>().GetLocalRotation();
		}
		return Quaternion.identity;
	}

	public void SetCameraOffset(Quaternion setTo) {
		if(curMode == CameraMode.Hands) {
			handsObject.transform.localRotation = setTo;
		}
	}

	public CameraMode GetMode() {
		return curMode;
	}

	public void SetCamera(int camNum) {
		camNum = Mathf.Clamp(camNum, 0, camTransforms.Count);
		foreach(CameraPosition cam in camTransforms) {
			if(cam.rend != null) {
				cam.rend.enabled = true;
			}
		}
		if(camTransforms[camNum].rend != null) {
			camTransforms[camNum].rend.enabled = false;
		}
		currentCameraHook.transform.parent = camTransforms[camNum].trans;
		currentCameraHook.transform.localPosition = Vector3.zero;
		currentCameraHook.transform.localRotation = Quaternion.identity;
	}
}
