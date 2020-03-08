using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthPickUp : MonoBehaviour
{
    //variables
    public float healthAmount; //amount of health that player gonna heal

    //healing player when he picks up heal
    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player") {
            playerHealth theHealth = other.gameObject.GetComponent<playerHealth>(); 
            theHealth.addHealth(healthAmount);
            Destroy(gameObject);
        }

    }
}
