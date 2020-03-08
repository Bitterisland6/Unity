using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winGame : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player") {
            playerHealth playerWins = other.gameObject.GetComponent<playerHealth>();
            playerWins.winGame();
        }
    }
}
