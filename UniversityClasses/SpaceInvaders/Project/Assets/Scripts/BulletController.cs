using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    //variables
    [SerializeField]
    float bulletSpeed; //speed of the bullet

    //referencies
    Rigidbody myRb;    //bullet's projectile
    
    void Awake() {
        //getting rigidbody of a bullet
        myRb = GetComponent<Rigidbody>();
        //moving bullet
        myRb.velocity = (transform.forward * bulletSpeed);
    }

    //function stopping bullet from move
    public void RemoveForce() {
        myRb.velocity = new Vector3(0f, 0f, 0f);
    }
}
