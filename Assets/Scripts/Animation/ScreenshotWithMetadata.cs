using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class GeneralMetadata {
	public string title;
	public string body;
	public Texture icon;
    public float startTime;
    public float endTime;
    public string animationName;
    public int cameraIndex;
}

public class ScreenshotWithMetadata {
	public Texture screenshot;
	public Vector3[] positions;
	public string[] data;

	public ScreenshotWithMetadata(Camera takeFrom, DetectableObjectRegistry reg) {
		screenshot = takeFrom.targetTexture;
		List<DetectableObject> objectsHere = reg.GetCurrentlyInView(takeFrom.transform);
		positions = new Vector3[objectsHere.Count];
		data = new string[objectsHere.Count];
		for(int i = 0; i < objectsHere.Count; ++i) {
			positions[i] = takeFrom.WorldToViewportPoint(objectsHere[i].transform.position);
			data[i] = objectsHere[i].metadataKey;
		}
	}
}
