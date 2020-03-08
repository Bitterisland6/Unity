using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScare_Script1 : MonoBehaviour
{
    public float speed;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
