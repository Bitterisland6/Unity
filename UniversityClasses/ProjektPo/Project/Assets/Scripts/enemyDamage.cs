using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyDamage : MonoBehaviour
{
    //varaibles
    public float damage; //how much damage enemy can do
    public float damageRate; //how often can enemy deal damage;
    public float pushBackForce; //force that will push back player from the object
    private float nextDamage; //when next atack is possible;
    
    //audio variables
    public AudioClip hurtingSound; //sound which will be played when enemy deals damage;
    private AudioSource enemyAS; //reference to object's audio source

    void Start() {
        //initialization
        nextDamage = 0f;

        //audio initialization
        enemyAS = GetComponent<AudioSource>();
    }

    //adding damage to player
    void OnTriggerStay2D(Collider2D other) {
        //we want to damage only player
        if(other.tag == "Player" && nextDamage < Time.time)
        {
            //reference to playerHealth script
            playerHealth thePlayerHealth = other.gameObject.GetComponent<playerHealth>();
            //adding damage to player
            thePlayerHealth.addDamage(damage);
            //delay in next atack
            nextDamage = Time.time + damageRate;
            //pushing the player back
            pushBack(other.transform);
            //playing sound
            enemyAS.PlayOneShot(hurtingSound);
        }    
    }

    //function that will push the enemy back
    void pushBack(Transform pushedObject) {
        //vector pushing player in oposite y direction
        Vector2 pushDirection = new Vector2(0f, (pushedObject.position.y - transform.position.y)).normalized;  
        pushDirection *= pushBackForce;
        //reference to rigidbody of pushed object
        Rigidbody2D pushRB = pushedObject.gameObject.GetComponent<Rigidbody2D>();
        //removing all forces from pushed object
        pushRB.velocity = Vector2.zero;
        //pushing object
        pushRB.AddForce(pushDirection, ForceMode2D.Impulse);
        
    }
}
