using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnDoor : MonoBehaviour
{
    //variable
    bool activated = false; //checking if the winning door was activated
    public Transform spawnPlace; //place where the door will spawn
    public GameObject door; //winning doors

    //if player gers close to end, spawn winning door
    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player" && !activated) {
            activated = true;
            Instantiate(door, spawnPlace.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    
}
