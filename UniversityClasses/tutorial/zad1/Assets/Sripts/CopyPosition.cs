using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyPosition : MonoBehaviour
{
   [SerializeField]
   Transform targetTransform;

    
    void Update() {
        transform.position = targetTransform.position;
    }
}
