using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sporeController : MonoBehaviour
{
    //variables
    public float sporeSpeedHigh; //most amount of force spore can have attached to it
    public float sporeSpeedLow; //least amount of force spore can have attached to it
    public float sporeAngle; //angle of shoot
    public float sporeTourqeAngle; //torque of the shooted spore
    private Rigidbody2D sporeRB; //reference to rigidbody on spore
    
    
    void Start() {
        //initialization
        sporeRB = GetComponent<Rigidbody2D>();
        sporeRB.AddForce(new Vector2(Random.Range(-sporeAngle, sporeAngle), Random.Range(sporeSpeedLow, sporeSpeedHigh)), ForceMode2D.Impulse);
        sporeRB.AddTorque((Random.Range(-sporeTourqeAngle, sporeTourqeAngle)));

    }
}
