using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMe : MonoBehaviour
{
    //variables
    float destroyTime = 12f;        //life time of an object     

    void Update() {
        //destroying gameobject
        Destroy(gameObject, destroyTime);
    }
}
