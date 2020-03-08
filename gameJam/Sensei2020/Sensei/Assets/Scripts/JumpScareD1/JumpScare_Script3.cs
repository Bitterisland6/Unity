using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScare_Script3 : MonoBehaviour
{
    public float speed;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
}
