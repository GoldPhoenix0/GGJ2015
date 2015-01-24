using UnityEngine;
using System.Collections;

public class GeneralMetadata {
	public string title;
	public string body;
	public Texture icon;
}

public class ScreenshotWithMetadata {
	public RenderTexture screenshot;
	Vector2[] positions;
	GeneralMetadata[] data;
}
