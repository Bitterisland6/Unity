using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverHealth : MonoBehaviour
{
    //references
    AudioSource coverAS;    //cover's audiosource
    
    //variables
    [SerializeField]
    int CoverEndurance;     //how many shots can cover take
    [SerializeField]
    AudioClip DmgClip;      //clip played on cover taking hit
    [SerializeField]
    AudioClip DestroyClp;   //clip played when cover is destroyed
    [SerializeField]
    GameObject DmgFx;       //effects spawned when cover takes dmg
    [SerializeField]
    Transform DmgPlace;     //spawn for DmgFx

    void Awake() {
        //getting audiosource reference
        coverAS = GetComponentInParent<AudioSource>();
    }

    void Update() {
        //destroing asteroid if its health is 0
        if(CoverEndurance == 0) {
            MakeDead();
        }
    }

    //function adding dmg to asteroid, playing hit sound and spawning hit effects
    public void DmgCover() {
        CoverEndurance--;
        PlaySound(DmgClip, 0.2f);
        Instantiate(DmgFx, DmgPlace.position, Quaternion.identity);
    }

    //function destroying asteroid
    void MakeDead() {
        PlaySound(DestroyClp, 0.5f);
        Destroy(gameObject);
    }

    //function needed to play asteroid hit sound
    void PlaySound(AudioClip clip, float volume) {
        coverAS.clip = clip;
        coverAS.volume = volume;
        coverAS.Play();
    }
}
