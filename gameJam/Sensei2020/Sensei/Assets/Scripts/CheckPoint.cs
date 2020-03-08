using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private Vector3 checkPointPos;
    private Quaternion checkPointAngle;
    public AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SavePoint()
    {
        checkPointPos = transform.position;
        checkPointAngle = transform.rotation;
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }
    public void Kill()
    {
        transform.position = checkPointPos;
        transform.rotation = checkPointAngle;
    }
}
