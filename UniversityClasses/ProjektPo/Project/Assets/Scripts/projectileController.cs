using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileController : MonoBehaviour
{   
    //variables
    public float rocketSpeed; //speed of a rocket

    //references
    Rigidbody2D myRB; //rigidbody of a projectile
    
    void Awake() {
        //initialization of components
        myRB = GetComponent<Rigidbody2D>();
        //shooting missile in right direction
        if(transform.localRotation.z > 0)
            myRB.AddForce(new Vector2(-1, 0)*rocketSpeed, ForceMode2D.Impulse);
        else
            myRB.AddForce(new Vector2(1, 0)*rocketSpeed, ForceMode2D.Impulse);
    }

    //function that stops missile on hit
    public void removeForce() {
        myRB.velocity = new Vector2(0, 0);
    }
}
