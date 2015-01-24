﻿using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class DetectableObjectRegistry : MonoBehaviour {

	List<DetectableObject> detectables;

	public void RegisterDetectable(DetectableObject obj) {
		detectables.Add(obj);
	}

	public List<DetectableObject> GetCurrentlyInView(Transform camTrans) {
		List<DetectableObject> retV = new List<DetectableObject>();
		foreach(DetectableObject obj in detectables) {
			if(obj.CheckVisibility(camTrans)) {
				retV.Add(obj);
			}
		}
		return retV;
	}
	void Start() {
		foreach(DetectableObject obj in GetComponentsInChildren<DetectableObject>()) {
			RegisterDetectable(obj);
		}
	}
}
