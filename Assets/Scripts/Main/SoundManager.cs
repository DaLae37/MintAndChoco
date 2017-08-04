using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager instance;

    public AudioClip mainBGM;

    AudioSource audioSource;

    void Awake() {
        if (instance != null)
            Destroy(this.gameObject);

        else {
            instance = this;
            audioSource = GetComponent<AudioSource>();
            DontDestroyOnLoad(this.gameObject);
        }
    }

	// Use this for initialization
	void Start () {
        PlayMainBGM();
    }

    public void PlayMainBGM() {
        audioSource.Stop();
        audioSource.clip = mainBGM;
        audioSource.Play();
    }
}
