using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    //references
    Loser lostGame;                         //reference to Loser script needed to lose game    
    AudioSource playerAS;                   //audiosource on player playing crash sounds
    
    //variables
    [SerializeField]
    GameObject DamageEffects;               //effects played when player gets hit
    [SerializeField]
    Transform SpawnPlace1;                  //place where dmg effects will be spawned
    [SerializeField]
    Transform SpawnPlace2;                  //place where dmg effects will be spawned
    [SerializeField]
    AudioClip DestroySound;                 //sound played on player's death
    [SerializeField]
    AudioClip DamageSound;                  //sound played when player will get hit
    
    public static int PlayerHealthNum;      //player's health
    private bool didDie;                    //flag saying if player died
    float explOffset;                       //offset between two explosions on player's death
    bool didExplode;                        //flag needed to spawn explosion effect without repeating
    
    void Awake() {
        //getting audio source
        playerAS = GetComponent<AudioSource>();
        //getting loser reference
        lostGame = GetComponent<Loser>();
        //initializing variables
        PlayerHealthNum = 3;
        didDie = false;
        didExplode = false;
    }

    void Update() {
        //if player's hea;th is 0, and player didn't die before
        if(PlayerHealthNum == 0 && !didDie) {
            //saying that player died
            didDie = true;
            //spawning explosion effects
            Instantiate(DamageEffects, SpawnPlace1.position, Quaternion.identity);
            Instantiate(DamageEffects, SpawnPlace2.position, Quaternion.identity);
            //playing explosion sound
            PlayDmgSound(DestroySound, 1f);
            //setting offset for second explosion
            explOffset = Time.unscaledTime + 1.5f;
            //losing game
            lostGame.LoseGame();
        }
        //second explosion
        if(didDie && Time.unscaledTime > explOffset && !didExplode) {
            //spawning explosions
            Instantiate(DamageEffects, SpawnPlace1.position, Quaternion.identity);
            Instantiate(DamageEffects, SpawnPlace2.position, Quaternion.identity);
            //saying that there was second explosion
            didExplode = true;
        }
    }

    //function adding damage to player
    public void AddDmg() {
        if(PlayerHealthNum != 1) {
            //setting right place to spawn explosion
            Transform pos = PlayerHealthNum == 3 ? SpawnPlace1 : SpawnPlace2;
            //playing explosion sound
            PlayDmgSound(DamageSound, 0.5f);
            //spawning explosion effects
            Instantiate(DamageEffects, pos.position, Quaternion.identity);
        }
        PlayerHealthNum--;
    }

    //function playing damage sound
    private void PlayDmgSound(AudioClip clip, float volume) {
        playerAS.clip = clip;
        playerAS.volume = volume;
        playerAS.Play();
    }
}
