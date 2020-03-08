using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //variables
    [SerializeField]
    Vector3 [] Directions;              //directions in which enemy will be moving
    [SerializeField]
    float Speed;                        //speed of enemies
    [SerializeField]
    float MoveTime;                     //amount of time for move in one direction
    [SerializeField]
    float MoveOffset;                   //time between two sequences of move
    [SerializeField]
    GameObject HostileBullet;           //bullet shot by enemies
    [SerializeField]
    Transform Gun;                      //place of bullet spawn
    [SerializeField]
    float FireRate;                     //offset for shooting
    [SerializeField]
    GameObject DeathFx;                 //effects of enemy's death

    // private variables
    private float currentMove;          //time when moving sequence began
    private int shootChance;            //chance for enemy to shoot
    private int firstPart;              //flag checking if we do the first 3 moves of sequence
    private float sequenceStart;        //start of a move in sequence
    private float nextShoot;            //posibility of next shoot
    private int iter;                   //move part iterator
    private float rotAngleH;            //rotation of move
    private Rigidbody EnemyRb;          //rigidbody of an enemy

    void Awake() {
        //initialization of private variables
        iter = 0;
        currentMove = 5f + Time.time;
        shootChance = 3;
        sequenceStart = 0f;
        firstPart = 1;
        nextShoot = Time.time + FireRate;
        EnemyRb = GetComponent<Rigidbody>();
        rotAngleH = -10f;
        //start movement of enemy - coming from the portal
        EnemyRb.velocity = new Vector3(0.0f, 0.0f, -2f);
    }
    
    void FixedUpdate() {
        //if enemy can move
        if(Time.time > currentMove) {
            //moving enemy and updating move duration
            Move();
            sequenceStart += Time.deltaTime;
        }
        //if enemy can shoot (fire rate)
        if(Time.time > nextShoot) {
            //setting time for next shot
            nextShoot = Time.time + FireRate;
            //random chance for actual shot
            if(Random.Range(0,50) <= shootChance) {
                Fire();
            }
        }
        //rotating enemy left and right when moving
        EnemyRb.rotation = Quaternion.RotateTowards(EnemyRb.rotation, Quaternion.Euler(EnemyRb.rotation.eulerAngles.x, EnemyRb.rotation.eulerAngles.y, EnemyRb.velocity.x*rotAngleH), 1.0f);
    }
    
    //function moving enemy
    void Move() {
        //if enemy did first 3 moves, set the direction for another 3 moves
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

    //function spawning bullet
    void Fire() {
        Instantiate(HostileBullet, Gun.position, Gun.rotation);
    }

    //function destroying enemy
    public void DestroyEnemy() {
        Instantiate(DeathFx, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
