using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ScreenshotViewer : MonoBehaviour {

	[SerializeField] RawImage image;
	[SerializeField] Button dataButtonPrefab;

	ScreenshotWithMetadata data;
	List<Button> curButtons = new List<Button>();


	public void SetData(ScreenshotWithMetadata data) {
		foreach(Button butt in curButtons) {
			Destroy(butt.gameObject);
		}
		curButtons.Clear();
		image.gameObject.SetActive(true);
		image.texture = data.screenshot;

		for(int i = 0; i < data.positions.Length; ++i) {
			if(data.positions[i].x > 1 || data.positions[i].y > 1 || data.positions[i].z < 0) {
				continue;
			}
			Button curButt = (Button)Instantiate(dataButtonPrefab);
			curButtons.Add(curButt);
			RectTransform curTrans = curButt.GetComponent<RectTransform>();
			curTrans.SetParent(image.GetComponent<RectTransform>(), false);
			curTrans.anchoredPosition = Vector2.Scale(data.positions[i], image.GetComponent<RectTransform>().sizeDelta);
		}
	}
}
