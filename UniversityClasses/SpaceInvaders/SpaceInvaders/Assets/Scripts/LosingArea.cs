using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LosingArea : MonoBehaviour
{
    [SerializeField]
    Loser LostGame;

    void OnTriggerEnter(Collider other) {
        if(other.tag == "Enemy") {
            LostGame.LoseGame();
        }
    }
}
