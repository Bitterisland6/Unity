using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSongController : MonoBehaviour
{
    //audio variables
    private AudioSource mainAS;
    [SerializeField]
    AudioClip playedSong;
    

    void Start() {
        mainAS = GetComponent<AudioSource>();
        PlaySong(playedSong, 1f);
    }

    public void PlaySong(AudioClip Song, float volume) {
        mainAS.volume = volume;
        mainAS.clip = Song;
        mainAS.Play();
    }
}
