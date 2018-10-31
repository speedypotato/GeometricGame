using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {

    public AudioClip bgMusic;
    public AudioSource audio;

    // Use this for initialization
    void Start() {
        //audio = gameObject.AddComponent<AudioSource>();
        //bgMusic = Resources.Load<AudioClip>("Sounds/bgMusic");
        audio.volume = 0.2f;
        audio.loop = true;
    }

    // Update is called once per frame
    void Update() {
        if (GameController.running && !audio.isPlaying) {
            print("Play Music");
            audio.Play();
        } else if (!GameController.running) {
            audio.Stop();
        }
    }
}
