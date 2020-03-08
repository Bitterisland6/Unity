using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    public Action action_object;
    public Animator anim;
    public void Interact()
    {
        if(anim)
        {
            anim.Play("Push");
        }
        
        action_object.Action_start();
    }
}

public abstract class Action : MonoBehaviour
{
    public abstract void Action_start();
}
