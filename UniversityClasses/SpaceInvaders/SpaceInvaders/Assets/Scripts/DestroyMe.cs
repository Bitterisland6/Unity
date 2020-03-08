using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMe : MonoBehaviour
{
    //time variables
    float destroyTime = 10f;

    void Update() {
        Destroy(gameObject, destroyTime);
    }
}
