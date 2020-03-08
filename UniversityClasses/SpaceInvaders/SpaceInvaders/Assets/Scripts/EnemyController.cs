using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    Vector3 [] Directions; //directions in which enemy will be moving
    [SerializeField]
    float Speed; //speed of enemies
    [SerializeField]
    float MoveTime; //amount of time for move in one direction
    [SerializeField]
    float MoveOffset; //time between two sequences of move
    [SerializeField]
    GameObject HostileBullet; //bullet shot by enemies
    [SerializeField]
    Transform Gun; //place of bullet spawn
    [SerializeField]
    Rigidbody EnemyRb; //rigidbody of an enemy
    [SerializeField]
    float FireRate; //offset for shooting

    //variables
    private float currentMove; //time when moving sequence began
    private int shootChance; //chance for enemy to shoot
    private int firstPart; //flag checking if we do the first 3 moves of sequence
    private float sequenceStart; //start of a move in sequence
    private float nextShoot; //posibility of next shoot
    private int iter;

    void Awake(){
        iter = 0;
        currentMove = 5f + Time.time;
        shootChance = 3;
        sequenceStart = 0f;
        firstPart = 1;
        nextShoot = Time.time + FireRate;
        EnemyRb = GetComponent<Rigidbody>();
    }
    
    void FixedUpdate(){
        if(Time.time > currentMove){
            Move();
            sequenceStart += Time.deltaTime;
        }
        if(Time.time > nextShoot){
            nextShoot = Time.time + FireRate;
            if(Random.Range(0,50) <= shootChance){
                Fire();
            }
        }

    }
    
    void Move(){
        if(iter == 3){
            firstPart *= -1;
            iter = 0;
        }else
        {
            if(sequenceStart < MoveTime){
                EnemyRb.velocity = iter != 1 ?  Directions[iter] * firstPart * Speed: Directions[iter] * Speed;
            }else{
                sequenceStart = 0;
                iter++;
                currentMove = Time.time + MoveOffset;
            }
        }
    }

    void Fire(){
        Instantiate(HostileBullet, Gun.position, Gun.rotation);
    }


}
