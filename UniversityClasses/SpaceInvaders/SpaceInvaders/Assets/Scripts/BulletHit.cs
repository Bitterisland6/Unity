using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHit : MonoBehaviour
{
    //references
    BulletController myBC;  //reference to BulletController script needed to call removeforce function
    Loser YouLost;          //reference to Loser script to call LoseGame function

    void Awake() {
        myBC = GetComponentInParent<BulletController>(); 
    }

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Shootable")) {
            //stopping and destroying bullet model
            myBC.RemoveForce();
            Destroy(gameObject);
            //checking what type of object have been hit by bullet
            if(other.tag == "Enemy") {
                EnemiesTextController.Enemies--;
                Destroy(other.gameObject);
            }
            if(other.tag == "Player") {
                YouLost = other.GetComponent<Loser>();
                YouLost.LoseGame();
            }
            if(other.tag == "Cover") {
                CoverHealth cover = other.gameObject.GetComponent<CoverHealth>();
                cover.DmgCover();
            }

        }
    }
}
