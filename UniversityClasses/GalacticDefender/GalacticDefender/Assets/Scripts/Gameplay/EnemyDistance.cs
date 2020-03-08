using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDistance : MonoBehaviour
{
    //variables
    [SerializeField]
    Vector3 [] Directions;              //directions in which object will be moving
    [SerializeField]
    float Speed;                        //speed of an object
    [SerializeField]
    float MoveTime;                     //amount of time for move in one direction
    [SerializeField]
    float MoveOffset;                   //time between two sequences of move

    //private variables
    private float currentMove;          //time when moving sequence began
    private int firstPart;              //flag checking if we do the first 3 moves of sequence
    private float sequenceStart;        //start of a move in sequence
    private int iter;                   //move part iterator
    private Rigidbody EnemyRb;          //rigidbody of an object

    void Awake() {
        //variables initialization
        iter = 0;
        currentMove = 5f + Time.time;
        sequenceStart = 0f;
        firstPart = 1;
        //getting rigidbody reference
        EnemyRb = GetComponent<Rigidbody>();
    }
    
    void FixedUpdate() {
        //if object can move
        if(Time.time > currentMove) {
            //moving object and updating move duration
            Move();
            sequenceStart += Time.deltaTime;
        }
    }
    
    //function moving object
    void Move() {
        //if object did first 3 moves, set the direction for another 3 moves
        if(iter == 3) {
            firstPart *= -1;
            iter = 0;
        } else {
            //if move should last
            if(sequenceStart < MoveTime) {
                //setting enemy's velocity based on iteration step
                EnemyRb.velocity = iter != 1 ?  Directions[iter] * firstPart * Speed: Directions[iter] * Speed;

            } else {
                //reseting sequence duration and setting time for next move
                sequenceStart = 0;
                iter++;
                currentMove = Time.time + MoveOffset;
            }
        }
    }
}
