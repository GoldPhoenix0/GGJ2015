using UnityEngine;
using System.Collections;

public class InitialFileViewer : MonoBehaviour {

	// This disables the playback controls, and initialises the details viewer with a specific file. When it is dismissed for the first time, enable the playback viewer.

	[SerializeField] PopulateDetailedView details;
	[SerializeField] string initialData;
	[SerializeField] GameObject controls;
	[SerializeField] GameObject comms;
	[SerializeField] UnityEngine.UI.Button dismissButton;

	bool inited = false;

	IEnumerator Start() {
		controls.SetActive(false);
		comms.SetActive(false);
		dismissButton.interactable = false;
		yield return null;
		details.InitialiseDetailedView(initialData);
		inited = true;
	}

	void Update() {
		if(inited && !details.gameObject.activeInHierarchy) {
			dismissButton.interactable = true;
			controls.SetActive(true);
			comms.SetActive(true);
			enabled = false;
		}
	}
}
