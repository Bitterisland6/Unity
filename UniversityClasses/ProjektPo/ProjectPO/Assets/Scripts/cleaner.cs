using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cleaner : MonoBehaviour
{
    public GameObject gameManager; //object needed to play audio in makeDead function

    //if something gets in this collider it gets destroyed
    void OnTriggerEnter2D(Collider2D other) {
        //if it's player, make him dead
        if(other.tag == "Player") {
            playerHealth playerFell = other.GetComponent<playerHealth>();
            playerFell.makeDead(gameManager);
        } else //destroy everything else
            Destroy(other.gameObject); 
    }
}
