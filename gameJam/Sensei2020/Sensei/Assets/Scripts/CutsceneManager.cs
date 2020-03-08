using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    //references
    [SerializeField]
    GameObject enemy;                   //reference to an enemy on the scene
    [SerializeField]
    AudioClip mainSong;                 //main song in scene

    //variables
    CharacterController eC;             //enemie's character controller
    float enemySpeed = 0.1f;            //velocity of the enemy
    AudioSource mainAS;                 //main audio source in the scene
    float moveOffset = 4f;              //offset of enemy's move

    void Start() {
        eC = enemy.GetComponent<CharacterController>();
        mainAS = GetComponent<AudioSource>();
        mainAS.clip = mainSong;
        mainAS.Play();
    }

    // Update is called once per frame
    void FixedUpdate() {
        if(moveOffset <= 0) {
            eC.Move(enemySpeed * eC.transform.forward);
            enemySpeed += 0.0001f;
        }else { 
            moveOffset -= Time.deltaTime;
        }
    }
}
