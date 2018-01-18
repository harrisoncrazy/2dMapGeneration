using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public static AudioManager Instance;

	public AudioSource main;

	public float currentGlobalVolume = 1.0f;

	public AudioClip[] musicClips = new AudioClip[4];
	private int oldRandTrack;

	// Use this for initialization
	void Start () {
		Instance = this;

		main = this.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (main.isPlaying == false) {
			findNewTrack ();
		}
	}

	void findNewTrack() {
		int randTrack = Random.Range (0, musicClips.Length);
		if (randTrack == oldRandTrack) {
			findNewTrack ();
			return;
		}

		main.PlayOneShot(musicClips[randTrack]);
	}
}
