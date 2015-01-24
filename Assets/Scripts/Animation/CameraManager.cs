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
	
	[SerializeField]GameObject eyesPrefab;
	[SerializeField]GameObject handsPrefab;

	CameraMode curMode;

	public void InitialiseCamera(CameraMode mode) {
		switch(mode) {
		case CameraMode.Eyes:
			currentCameraHook = (GameObject)Instantiate(eyesPrefab);
			break;
		case CameraMode.Hands:
			currentCameraHook = (GameObject)Instantiate(handsPrefab);
			break;
		}
		curMode = mode;
	}

	public Quaternion GetCurrentCameraOffset() {
		if(curMode == CameraMode.Eyes) {
			return currentCameraHook.GetComponent<LocalRotationQuery>().GetLocalRotation();
		}
		return Quaternion.identity;
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
