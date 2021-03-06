﻿using UnityEngine;
using System.Collections;

public class DetectableObject : MonoBehaviour {

	// This will be placed in a scene-wide registry of detectable objects...

	public string metadataKey;
	public Renderer myRender;
	public DetectableObjectRegistry reg;


	void Start() {
		if(myRender == null) {
			myRender = renderer;
		}
		if(myRender == null) {
			enabled = false;
		}
		if(reg) {
			reg.RegisterDetectable(this);
		}
	}

	public bool CheckVisibility(Transform lookingFrom) {
		if(!enabled) {
			return false;
		}
		if(!myRender.isVisible) {
			return false;
		}
		if(Physics.Linecast(transform.position, lookingFrom.position)) {
			return false;
		}
		return true;
	}
}
