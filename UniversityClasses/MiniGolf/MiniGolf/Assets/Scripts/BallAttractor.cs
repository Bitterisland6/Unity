using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAttractor : MonoBehaviour
{
    //variables
    [SerializeField][Range(0f, 100f)]
    float AttractionPower;              //power of the force pulling object towards the attracting object
    [SerializeField][Range(0f, 1f)]
    float AttractionRadius;             //radius of the attraction area
    [SerializeField]
    Transform AttractionCentre;         //object which attracts other objects
    
    //private variables
    Vector3 attractionDirection;        //direction of the force added to attracted object

    
    void FixedUpdate() {
        foreach (Collider col in Physics.OverlapSphere(AttractionCentre.position, AttractionRadius)) {
            //checking for the ball in attracting area
            if(col.tag == "Ball") { 
                //counting direction of the attraction force based by ball's position
                attractionDirection = AttractionCentre.position - col.transform.position;
                //adding force
                col.GetComponent<Rigidbody>().AddForce(attractionDirection.normalized * AttractionPower * Time.fixedDeltaTime);
            }
        }    
    }
}
