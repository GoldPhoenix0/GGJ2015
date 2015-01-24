using UnityEngine;
using System.Collections;

public class HackyNetworkStarter : MonoBehaviour {

	[SerializeField] NetworkCore core;

	void Start() {
		if(Screen.currentResolution.width == 1280 && Screen.currentResolution.height == 800) {
			core.StartServer();
		}
	}
}
