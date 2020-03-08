﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HorrorLvlManager : MonoBehaviour
{      
    //references
    [SerializeField]
    GameObject enemy;               //enemy on the map
    CharacterController enemCHC;    //character controller of the enemy on the map
    PlayerController player;
    
    //variables
    [SerializeField]
    AudioClip horrorSong;           //song played during level
    [SerializeField]
    int collectiblesNum;            //total number of collectibles possible to get
    [SerializeField]
    GameObject poofFX;              //particle system spawned on player disapear
    float waitingTime;              //time for enemy to start walking
    float speed = 0.2f;             //speed of the enemy
    float endTime;                  //end of enemy's "life" 
    string roomName = "HorrorRoom"; //name of the room
    bool isPlayer = false;          //flag saying if there is a player on the scene
    bool spawned = false;           //flag saying if we spawned poof


    void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            AudioSource.PlayClipAtPoint(horrorSong, transform.position);
            waitingTime = 4f;
            player = other.gameObject.GetComponent<PlayerController>();
            enemCHC = enemy.GetComponent<CharacterController>();
            if (player.colected.Count == collectiblesNum) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            
            endTime = Time.time + waitingTime + 1.5f * player.GetValue(roomName);
            
            isPlayer = true;
        }
    }

    void FixedUpdate() {
        if(isPlayer) {
            if(waitingTime <= 0) {
                enemy.SetActive(true);
                enemCHC.Move(enemy.transform.forward * speed);
            } else {
                waitingTime -= Time.deltaTime;
            }

            if(endTime < Time.time) {
                if(!spawned) {
                    Instantiate(poofFX, enemy.transform.position, Quaternion.identity);
                    spawned = true;
                }
                enemy.SetActive(false);
            }
        }
    }

}
