using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongController : MonoBehaviour
{
    //references
    private AudioSource mainAS;     //audio source playing given song
    
    //variables
    [SerializeField]
    AudioClip playedSong;           //played song

    public static float Volume;     //Volume of the audio source

    void Awake() {
        //getting audio source from gameobject
        mainAS = GetComponent<AudioSource>();
        //playing song
        PlaySong(playedSong, Volume);
    }

    //function playing given song with given volume
    public void PlaySong(AudioClip Song, float volume){
        mainAS.volume = volume;
        mainAS.clip = Song;
        mainAS.Play();
    }
}
