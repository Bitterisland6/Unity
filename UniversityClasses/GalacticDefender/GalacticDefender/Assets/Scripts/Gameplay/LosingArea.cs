using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LosingArea : MonoBehaviour
{
    //references
    [SerializeField]
    Loser LostGame;         //reference needed to make player lose game

    //checking collision
    void OnTriggerEnter(Collider other) {
        //if enemy is in the area, then lose game
        if(other.tag == "Enemy") {
            LostGame.LoseGame();
        }
        
    }
}
