using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    //variables
    [SerializeField]
    Transform ShootPlace;           //place where the ball is shot from
    [SerializeField]
    float RotationSpeed;            //speed of the cannon's rotation
    
    //private variables
    bool rotateRight;               //bool saying if canon should rotate to the right, or to the left side
    bool canRotate;                 //bool saying if cannon can rotate
    Quaternion rotationLimits;      //limits of the cannon's rotation

    void Awake() {
        rotateRight = true;
        canRotate = true;
        rotationLimits = Quaternion.Euler((new Vector3(0f, 20f, 0f)));
    }

    void FixedUpdate() {
            //checking cannon's rotation and setting the rotation direction
             if(transform.rotation.eulerAngles.y < 30f  && transform.rotation.eulerAngles.y > rotationLimits.eulerAngles.y) {
                rotateRight = false;
             }
        else if(transform.rotation.eulerAngles.y < 340f && transform.rotation.eulerAngles.y > 330f) {
                rotateRight = true;
             }
             //rotating cannon
             if(rotateRight && canRotate) {
                transform.rotation *= Quaternion.Euler(new Vector3(0f, 1f, 0f) * RotationSpeed);
             } 
        else if(!rotateRight && canRotate) {
                transform.rotation *= Quaternion.Euler(new Vector3(0f, -1f, 0f) * RotationSpeed);
             }
    }

    //shoting the ball
    void OnTriggerEnter(Collider other) {
        if(other.tag == "Ball") {
            canRotate = false;
            Rigidbody ballRb = other.GetComponent<Rigidbody>();
            ballRb.position = ShootPlace.position;
            ballRb.rotation = ShootPlace.rotation;
            ballRb.useGravity = false;
            ballRb.AddForce(transform.forward * 20f, ForceMode.Impulse);
            ballRb.useGravity = true;
            canRotate = true;

        }
    }
}
