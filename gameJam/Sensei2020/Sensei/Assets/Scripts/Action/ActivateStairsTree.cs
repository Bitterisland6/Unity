using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateStairsTree : Action
{
    public Animator anim;
    public AudioClip clip;
    bool oneTime;
    public override void Action_start()
    {
        if(!oneTime)
        {
            anim.SetTrigger("Start");
            AudioSource.PlayClipAtPoint(clip, transform.position);
        }
    }
}
