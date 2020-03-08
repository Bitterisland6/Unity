using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainSongController : MonoBehaviour
{
    //audio variables
    public AudioClip playedSong; //currentl;y played song
    private AudioSource mainAS; //audio source attached to camera

    void Start() {
        //audio initialization
        mainAS = GetComponent<AudioSource>();
        PlaySong(playedSong, 0.1f);    
    }

    //function that will play given song with given volume
    public void PlaySong(AudioClip song, float volume) {
        mainAS.volume = volume;
        mainAS.clip = song;
        mainAS.Play();
    }
}
