using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyMe : MonoBehaviour
{
    //variables
    public float aliveTime; //lifetime of an object
    
    //destroy the projectile after given life time
    void Awake() {
        Destroy(gameObject, aliveTime);
    }
}
