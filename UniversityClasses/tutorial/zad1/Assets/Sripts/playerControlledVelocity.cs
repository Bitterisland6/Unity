using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControlledVelocity : MonoBehaviour
{
    [SerializeField]
    Vector3 velocity;
    [SerializeField]
    KeyCode keyPositive;
    [SerializeField]
    KeyCode keyNegative;


    void FixedUpdate() {
        if(Input.GetKey(keyPositive))
            GetComponent<Rigidbody>().velocity += velocity;

        if(Input.GetKey(keyNegative))
            GetComponent<Rigidbody>().velocity -= velocity;
    }
}
