using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    Camera face;

    void Start(){
        face = Camera.main;
    }

    void Update() {
        transform.rotation = face.transform.rotation;
    }
}
