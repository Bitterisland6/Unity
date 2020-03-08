using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHit : MonoBehaviour
{
    //references
    BulletController myBC;          //reference needed to stop Bullet from moving
    PlayerHealth plHealth;          //reference needed to add dmg to player
    StressReceiver stress;          //reference needed to shake camera
    CoverHealth cover;              //reference needed to dmg asteroid
    EnemyController controller;     //reference needed to destroy enemy

    void Awake() {
        //getting bullet reference
        myBC = GetComponentInParent<BulletController>(); 
    }

    //checking collision of a bullet
    void OnTriggerEnter(Collider other) {
        //if hit object is shootable
        if(other.gameObject.layer == LayerMask.NameToLayer("Shootable")) {
            //stopping bullet and destroyng bullet graphics
            myBC.RemoveForce();
            Destroy(gameObject);
            //if bullet hit enemy
            if(other.tag == "Enemy") {
                //getting enemy controller reference
                controller = other.GetComponent<EnemyController>();
                //destroying enemy and updating number of enemies alive
                controller.DestroyEnemy();
                EnemiesTextController.Enemies--;
            }
            //if bullet hit player
            if(other.tag == "Player") {
                //getting stress and player references, adding dmg and shaking screen
                plHealth = other.GetComponent<PlayerHealth>();
                plHealth.AddDmg();
                stress = other.GetComponent<StressReceiver>();
                stress.InduceStress(2f);
            }
            //if bullet hit asteroid
            if(other.tag == "Cover") {
                //getting asteroid reference and adding dmg
                cover = other.gameObject.GetComponentInChildren<CoverHealth>();
                cover.DmgCover();
            }
        }
    }
}
