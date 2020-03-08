using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPosition : MonoBehaviour
{
    //references
    [SerializeField]
    Rigidbody targetRb;             //ball's rigidbody
    [SerializeField]
    AudioClip FallSound;            //sound played when ball falls from track

    //private variables
    Vector3 lastGoodPos;            //vector holding last position of the ball, when it was not moving
    private AudioSource AS;         //audio source needed to play winning sound
    
    void Start() {
        //setting last good position to starting position
        lastGoodPos = targetRb.position;
        //getting audiosource from gameobject
        AS = GetComponent<AudioSource>();
    }

    void Update() {
        //if ball falls down, return ball to the last good position
        if(targetRb.position.y < -2f) {  
            ReturnBall();       
        }
        
        //updating last good position
        if(targetRb.velocity == Vector3.zero)
            lastGoodPos = targetRb.position;
            
    }

    //function returning ball to "checkpoint"
    private void ReturnBall() {
        //playing falling sound
        AS.clip = FallSound;
        AS.Play();
        //reseting ball position
        targetRb.position = lastGoodPos;
        targetRb.velocity = Vector3.zero;
        targetRb.angularVelocity = Vector3.zero;
        
    }
}
