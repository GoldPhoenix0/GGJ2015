using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ScreenshotViewer : MonoBehaviour {

	[SerializeField] RawImage image;
	[SerializeField] GameObject imageObj;
	[SerializeField] Button dataButtonPrefab;

	ScreenshotWithMetadata data;
	List<Button> CurButts = new List<Button>();
	List<ButtonDataManager> managers = new List<ButtonDataManager>();

	[SerializeField] PopulateDetailedView detailPop;

	class ButtonDataManager {
		Vector2 position;
		string key;
		PopulateDetailedView pop;
		public ButtonDataManager(Vector2 pos, string key, PopulateDetailedView pop) {
			position = pos;
			this.key = key;
			this.pop = pop;
		}
		public void DataPressed() {
			Debug.Log(key);
			pop.InitialiseDetailedView(key);
		}
	}

	float targetScale;

	public void SetData(ScreenshotWithMetadata data) {
		foreach(Button butt in CurButts) {
			Destroy(butt.gameObject);
		}
		CurButts.Clear();
		imageObj.SetActive(true);
		image.texture = data.screenshot;
		RectTransform trans = imageObj.GetComponent<RectTransform>();
		Vector2 curDelta = trans.sizeDelta;

		for(int i = 0; i < data.positions.Length; ++i) {
			if(data.positions[i].x > 1 || data.positions[i].y > 1 || data.positions[i].z < 0) {
				continue;
			}
			Button curButt = (Button)Instantiate(dataButtonPrefab);
			CurButts.Add(curButt);
			RectTransform curTrans = curButt.GetComponent<RectTransform>();
			curTrans.SetParent(image.GetComponent<RectTransform>(), false);
			Rect imgRect = image.GetComponent<RectTransform>().rect;
			Vector2 rectSize = new Vector2(imgRect.width, imgRect.height);
			curTrans.anchoredPosition = Vector2.Scale(data.positions[i], rectSize);
			ButtonDataManager newDat = new ButtonDataManager(data.positions[i], data.data[i], detailPop);
			managers.Add(newDat);
			curButt.onClick.AddListener(newDat.DataPressed);
			Debug.Log(data.positions[i]);
			Debug.Log(curTrans.anchoredPosition);
		}
		
		if(targetScale < 10) {
			targetScale = trans.sizeDelta.y;
		}

		curDelta.y = 0;
		trans.sizeDelta = curDelta;

	}

	void Update() {
		if(imageObj.activeInHierarchy) {


			RectTransform trans = imageObj.GetComponent<RectTransform>();
			Vector2 curDelta = trans.sizeDelta;
			curDelta.y = Mathf.Lerp (curDelta.y, targetScale, Time.deltaTime);
			trans.sizeDelta = curDelta;
			if(Mathf.Abs(targetScale - curDelta.y) < 20f) {
				foreach(Button butt in CurButts) {
					butt.interactable = true;
				}
			} else {
				foreach(Button butt in CurButts) {
					butt.interactable = false;
				}
			}

		}
	}

	public void DismissData() {
		foreach(Button butt in CurButts) {
			Destroy(butt.gameObject);
		}
		CurButts.Clear();
		RectTransform trans = imageObj.GetComponent<RectTransform>();
		Vector2 curDelta = trans.sizeDelta;
		curDelta.y = targetScale;
		trans.sizeDelta = curDelta;
		imageObj.SetActive(false);
	}

}
