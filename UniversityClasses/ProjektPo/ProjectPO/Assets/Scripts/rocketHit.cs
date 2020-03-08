using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocketHit : MonoBehaviour
{
    //variables
    public float weaponDamage; //damage of the weapon
    
    //references
    projectileController myPC; //reference to object-parent
    public GameObject explosionEffect; //efect of explosion, spaned on hit of the rocket

    void Awake() {
        //initialization of components
        myPC = GetComponentInParent<projectileController>();
    }

    //if rocket hits something shootable
    void OnTriggerEnter2D(Collider2D other) {
        //if hit object is shootable, then stop missile from move, make explosion, 
        //and destroy missile
        if(other.gameObject.layer == LayerMask.NameToLayer("Shootable")) {
            //stop racket form movement
            myPC.removeForce();
            //explosion
            Instantiate(explosionEffect, transform.position, transform.rotation);
            Destroy(gameObject);
            //if the hit object is enemy
            if(other.tag == "Enemy") {
                //variable used to call addDamage function from enemyHealth script
                enemyHealth hurtEnemy = other.gameObject.GetComponent<enemyHealth>();
                hurtEnemy.addDamage(weaponDamage);
            }
            
        }

    }

    //to be sure that, something happens on hit 
    //(if it doesn't happen on enter, then it'll happen on stay)
    void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Shootable")) {
            myPC.removeForce();
            Instantiate(explosionEffect, transform.position, transform.rotation);
            Destroy(gameObject);
            //if the hit object is enemy
            if(other.tag == "Enemy") {
                //variable used to call addDamage function from enemyHealth script 
                enemyHealth hurtEnemy = other.gameObject.GetComponent<enemyHealth>();
                hurtEnemy.addDamage(weaponDamage);
            }
        }
    }
}
