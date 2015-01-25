using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AudioScript : MonoBehaviour {

	[SerializeField] AudioClip[] clips;
	AudioSource audio;

	void Start(){
		audio = GetComponent<AudioSource>();
	}

	public void PlayAudio (int audioIndex){
		if (clips[audioIndex] != null){
			audio.clip = clips[audioIndex];
			audio.Play();
		}
	}

}
