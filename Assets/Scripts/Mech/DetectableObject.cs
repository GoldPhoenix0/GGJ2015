using UnityEngine;
using System.Collections;

public class DetectableObject : MonoBehaviour {

	// This will be placed in a scene-wide registry of detectable objects...

	public string metadata;
	public Renderer myRender;

	void Start() {
		if(myRender == null) {
			myRender = renderer;
		}
		if(myRender == null) {
			enabled = false;
		}
	}

	public bool CheckVisibility(Transform lookingFrom) {
		if(!enabled) {
			return false;
		}
		if(!myRender.isVisible) {
			myRender.material.color = Color.red;
			return false;
		}
		if(Physics.Linecast(transform.position, lookingFrom.position)) {
			Debug.DrawLine(transform.position, lookingFrom.position, Color.red);
			myRender.material.color = Color.yellow;
			return false;
		}
		myRender.material.color = Color.blue;
		return true;
	}
}
