using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSongController : MonoBehaviour
{
    //references
    private AudioSource mainAS;     //reference to the main audio source

    //variables
    [SerializeField]
    AudioClip PlayedSong;           //song played on the beggining of the level
    [SerializeField]
    AudioClip FasterSong;           //song played when enemies are in the middle of the distance
    [SerializeField]
    AudioClip FastestSong;          //song played when enemies are very close to player
    [SerializeField]
    Transform EnemyDistance;        //object needed to get the enemies distance to the player
    [SerializeField]
    float FastestDist;              //distance to play the fastest song
    [SerializeField]
    float FasterDist;               //distance to play faster song
    
    public static float Volume;     //volume of played sound

    //private variables
    bool playedFaster = false;      //flag checking if faster song should be played
    bool playedFastest = false;     //flag checking if fastest song should be played
    float pos;                      //position in z axis of enemies

    void Start() {
        mainAS = GetComponent<AudioSource>();
        PlaySong(PlayedSong, Volume);
    }

    void Update() {
        if(EnemyDistance) {
            //updating enemies position
            pos = EnemyDistance.position.z;
            //if pos is less than distance to play fastest song, and we haven't played fastest song yet, then we play fastest song
            if(pos <= FastestDist && !playedFastest) {
                mainAS.Stop();
                PlaySong(FastestSong, Volume);
                playedFastest = true;
            } else if(pos > FastestDist && pos <= FasterDist && !playedFaster) {
                //same thing for faster song, but we check if enemies are between fastest and faster dist
                mainAS.Stop();
                PlaySong(FasterSong, Volume);
                playedFaster = true;
            }
        }
    }

    //function playing given song
    public void PlaySong(AudioClip Song, float volume) {
        mainAS.volume = volume;
        mainAS.clip = Song;
        mainAS.Play();
    }
}
