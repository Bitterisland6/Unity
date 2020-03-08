using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    [SerializeField]
    AudioClip clip;
    Animator animator;
    public override bool Action(GameObject agent)
    {
        animator.SetBool("opened", !animator.GetBool("opened"));
        AudioSource.PlayClipAtPoint(clip, transform.position);
        return true;
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            CanInteract i = other.GetComponent<CanInteract>();
            if (!i.interactables.Contains(this))
            {
                i.interactables.Add(this);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<CanInteract>().interactables.Remove(this);
        }
    }
}
