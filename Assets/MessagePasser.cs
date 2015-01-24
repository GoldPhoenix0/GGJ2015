using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MessagePasser : MonoBehaviour {

	public Color startColour;
	public Color decayedColour;

	public float brightTime = 1, decayTime = 10;

	float curTime;

	string curMessage;
	public Text messageText;

	void Start() {
		messageText.text = "";
	}

	public void SetMessage(string newMess) {
		curMessage = newMess;
	}

	public void SendSavedText() {
		networkView.RPC("RemoteMessage", RPCMode.Others, curMessage);
	}

	public void SendText(string sent) {
		networkView.RPC("RemoteMessage", RPCMode.Others, sent);
	}

	[RPC]
	void RemoteMessage(string newMessage) {
		// Here we should pop up a signal to eyes
		messageText.text = curMessage;
		curTime = 0;
	}

	void Update() {
		curTime += Time.deltaTime;
		Color resultantColour = decayedColour;
		if(curTime < brightTime) {
			resultantColour = Color.Lerp(startColour, decayedColour, curTime / brightTime);
			messageText.color = resultantColour;
			return;
		}
		if(curTime < decayTime) {
			resultantColour = Color.Lerp(decayedColour, new Color(decayedColour.r, decayedColour.g, decayedColour.b, 0), Mathf.InverseLerp(brightTime, decayTime, curTime));
			messageText.color = resultantColour;
			return;
		}
	}
}
