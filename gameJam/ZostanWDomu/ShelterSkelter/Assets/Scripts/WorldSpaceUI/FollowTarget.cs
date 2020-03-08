using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Vector3 target;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(target.x, transform.position.y, target.z);
    }
}
