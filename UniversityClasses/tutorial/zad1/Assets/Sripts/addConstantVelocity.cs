using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addConstantVelocity : MonoBehaviour
{
   [SerializeField]
    Vector3 velocity;

    void FixedUpdate() {
        GetComponent<Rigidbody>().velocity += velocity;
    }
}
