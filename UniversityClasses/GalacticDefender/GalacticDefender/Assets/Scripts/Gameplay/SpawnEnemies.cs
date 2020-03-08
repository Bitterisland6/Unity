using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    //variables
    [SerializeField]
    int EnemiesRow;                     //number of enemies in a row
    [SerializeField]
    int EnemiesColumn;                  //number of enemies in a column
    [SerializeField]
    GameObject enemyPreFab;             //enemy, that will spawn
    [SerializeField]
    Transform SpawnPoint;               //begining of spawn
    [SerializeField]
    GameObject Portal;                  //portal which enemies come from
    [SerializeField]
    float AnimDuration;                 //duration of portal animation
    [SerializeField]
    float AnimStart;                    //time of portal animation start
    [SerializeField]
    float SpawnStart;                   //time of enemies spawn
    [SerializeField]
    AudioClip PortalSound;              //sound of portal
    
    public static bool canAnimate;      //bool saying if portal can be animated

    //private variables
    float startAnimOffset;              //starting offset of portal animation  
    float spawnOffset;                  //spawn offset 
    float currentDuration;              //current duration of portal animation
    bool spawned;                       //bool saying if enemies are already spawned
    bool animated;                      //bool saying if portal is animated
    Animator portalAnim;                //reference to animator on a portal, needed to animate portal
    AudioSource portalAS;               //reference to audio source on portal, needed to play portal sound

    void Awake() {
        //setting enemies text
        EnemiesTextController.Enemies = EnemiesColumn*EnemiesRow;
        //gettin animator reference
        portalAnim = Portal.GetComponent<Animator>();
        //initializating variables
        startAnimOffset = Time.time + AnimStart;
        spawnOffset = Time.time + 50f;
        currentDuration -= AnimStart;
        spawned = false;
        animated = false;
        canAnimate = true;
        //getting audio source reference
        portalAS = Portal.GetComponent<AudioSource>();
        portalAS.clip = PortalSound;
        portalAS.volume = 0.8f;
    }

    void Update() {
        //updating duration of animation
        currentDuration += Time.deltaTime;
        //rotating portal
        Portal.GetComponent<Rigidbody>().rotation *= Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.05f));
        //checking if portal can be animated
        if(!animated && Time.time > startAnimOffset && canAnimate) {
            //starting animation
            portalAnim.SetTrigger("Start");
            //setting right spawn offset
            spawnOffset = Time.time + SpawnStart;
            //saying that portal was animated
            animated = !animated;  
            //playing portal sound
            portalAS.Play();
        }
        //checking if we can spawn enemies
        if(Time.time > spawnOffset && !spawned) {
            //spawning enemies and saying that they are spawned
            spawned = !spawned;
            spawn();
        }
        //checking if we should end portal animation
        if(currentDuration >= AnimDuration && animated && canAnimate) {
            //ending animation
            portalAnim.SetTrigger("Start");
            //making it impossible to animate again
            canAnimate = !canAnimate;
            //ending portal sound
            portalAS.Stop();
        }
    }

    //function spawning enemies
    public void spawn(){
        for(int i = 0; i < EnemiesColumn; i++) {
            for(int j = 0; j < EnemiesRow; j++) {
                Instantiate(enemyPreFab, SpawnPoint.position + new Vector3(j*3.0f, i*3.0f, 0f), Quaternion.identity);
            }
        }
    }
}
