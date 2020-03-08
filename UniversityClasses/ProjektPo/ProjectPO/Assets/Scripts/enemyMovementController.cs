using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovementController : MonoBehaviour
{
    //variables
    public float enemySpeed; //speed of an enemy
    Animator enemyAnimator; //acces to enemy animator, will allow us to change animations

    //facing variables
    public GameObject enemyGraphic; //graphic which we will be flipping
    bool canFlip = true; //conditions that will allow enemy to flip
    bool facingRight = false; //checking if enemy is facing right
    float flipTime = 5f; //time possibility to flip graphic
    float nextFlipChance = 0f; //when next graphic flip will be possible

    //attacking
    public float chargeTime; //the amount of time enemy will wait till charge
    float startChargeTime; //when enemy will charge
    bool charging; //checking if enemy is charging
    Rigidbody2D enemyRB; //reference to enemy rigidbody
    
    //audio variables
    public AudioClip chargeSound; //sound displayed on enemy charge
    AudioSource enemyAS; //reference to audio source on children
    private float startChargeSound = 0f; //start of charge sound playing
    private float chargeSoundCD = 5f; //cooldown of the charge sound

    void Start() {
        //initialization
        enemyAnimator = GetComponentInChildren<Animator>();
        enemyRB = GetComponent<Rigidbody2D>();

        //audio initialization
        enemyAS = GetComponentInChildren<AudioSource>();
    }

    void Update() {
            //fliping enemy
            if(Time.time > nextFlipChance) {
                 if(Random.Range(0,10) >= 5)
                    flipFacing();
                
                nextFlipChance = Time.time + flipTime;
            }
            
    }

    //when player gets in enemy range
    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player") {
            //if enemy is facing right and player is on left
            if(facingRight && other.transform.position.x < transform.position.x)
                flipFacing();
            //if enemy is facing left and player is on right
            else if(!facingRight && other.transform.position.x > transform.position.x)
                flipFacing();
            //once flipped cannot flip again
            canFlip = false;
            //setting enemy to charge
            charging = true;
            //"getting ready" to charge
            startChargeTime = Time.time + chargeTime;
            //playing charge sound
            if(startChargeSound < Time.time) {
                startChargeSound = Time.time + chargeSoundCD;
                enemyAS.PlayOneShot(chargeSound);
            }
            
        }
    }

    //if player stays in enemy range
    void OnTriggerStay2D(Collider2D other) {
        if(other.tag == "Player") {
            //charge
            if(startChargeTime < Time.time) {
                if(!facingRight)
                    enemyRB.AddForce(new Vector2(-1, 0)*enemySpeed);
                else 
                    enemyRB.AddForce(new Vector2(1, 0)*enemySpeed);
                enemyAnimator.SetBool("isCharging", charging);
            }
        }
    }

    //if player leaves danger zone
    void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Player") {
            canFlip = true;
            charging = false;
            enemyRB.velocity = new Vector2(0f, 0f);
            enemyAnimator.SetBool("isCharging", charging);
        }    
    }

    //function flipping enemy if it's possible
    void flipFacing() {
        if(!canFlip)
            return;

        //direction which enemy is facing
        float facingX = enemyGraphic.transform.localScale.x;
        facingX *= -1f;
        enemyGraphic.transform.localScale = new Vector3(facingX, enemyGraphic.transform.localScale.y, enemyGraphic.transform.localScale.z);
        facingRight = !facingRight;
    }
}
