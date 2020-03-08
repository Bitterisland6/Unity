using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreEnemy : MonoBehaviour
{
    void Awake() {
        //ignoring collision between enemy and asteroid
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Shootable"), LayerMask.NameToLayer("Shootable"));
    }
}
