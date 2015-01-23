using UnityEngine;
using System.Collections;

public class DetectableTest : MonoBehaviour {

	public DetectableObject testing;

	void Update() {
		if(testing == null) {
			return;
		}
		testing.CheckVisibility(transform);
	}
}
