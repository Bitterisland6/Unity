using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObstacle : MonoBehaviour
{
    //variables
    [SerializeField]
    Vector3 Direction;                      //move direction of obstacle
    [SerializeField]        
    float Distance;                         //distance of the move
    [SerializeField][Range(0.1f, 5.0f)]
    float MoveSpeed;                        //speed of move

    //private variables
    float distanceCovered;                  //current covered distance by obstacle
    bool posDir;                            //bool saying if obstacle should move in the given Direction or the opposite

    void Awake() {
        distanceCovered = 0f;        
        posDir = true;
    }

    void FixedUpdate() {
        //setting the move Direction
        if(distanceCovered > Distance) {
            posDir = false;
        } else if(distanceCovered < 0f) {
            posDir = true;
        }

        //moving obstacle
        if(posDir) {
            distanceCovered += MoveSpeed * Time.deltaTime;
            transform.position += Direction * MoveSpeed * Time.deltaTime;
        } else {
            distanceCovered -= MoveSpeed * Time.deltaTime;
            transform.position -= Direction * MoveSpeed * Time.deltaTime;
        }
    }
}
