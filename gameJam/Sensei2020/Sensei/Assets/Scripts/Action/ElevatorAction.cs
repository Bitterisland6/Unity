using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorAction : Action
{
    public bool up;
    public Vector3 pos1;
    public Vector3 pos2;
    public float speed;
    private float proc;
    private Vector3 originalPos;
    [SerializeField]
    AudioClip elevatorSound;

    public override void Action_start()
    {
        up = !up;
        Debug.Log("action");
        AudioSource.PlayClipAtPoint(elevatorSound, transform.position);
    }

    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(up)
        {
            proc += speed;
        }
        else
        {
            proc -= speed;
        }
        proc = Mathf.Clamp(proc, 0, 100);
        transform.position = Vector3.Lerp(pos1 + originalPos, pos2 + originalPos, proc/100);
    }
}
