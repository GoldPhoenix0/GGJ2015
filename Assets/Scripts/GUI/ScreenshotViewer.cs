using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScreenshotViewer : MonoBehaviour {

	[SerializeField] RawImage image;

	ScreenshotWithMetadata data;

	public void SetData(ScreenshotWithMetadata data) {
		image.enabled = true;
		image.texture = data.screenshot;
	}
}
