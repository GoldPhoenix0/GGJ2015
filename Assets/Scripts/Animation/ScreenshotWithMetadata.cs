using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class GeneralMetadata {
	public string title;
	public string body;
	public Texture icon;
}

public class ScreenshotWithMetadata {
	public Texture2D screenshot;
	Vector2[] positions;
	string[] data;

	public ScreenshotWithMetadata(Camera takeFrom, DetectableObjectRegistry reg) {
		Texture2D tex = new Texture2D(takeFrom.targetTexture.width, takeFrom.targetTexture.height);
		RenderTexture.active = takeFrom.targetTexture;
		tex.ReadPixels(new Rect(0, 0, tex.width, tex.height), tex.width, tex.height);
		tex.Apply();
		screenshot = tex;
		List<DetectableObject> objectsHere = reg.GetCurrentlyInView(takeFrom.transform);
		positions = new Vector2[objectsHere.Count];
		data = new string[objectsHere.Count];
		for(int i = 0; i < objectsHere.Count; ++i) {
			positions[i] = takeFrom.WorldToViewportPoint(objectsHere[i].transform.position);
			data[i] = objectsHere[i].metadataKey;
			Debug.Log(positions[i]);
		}
	}
}
