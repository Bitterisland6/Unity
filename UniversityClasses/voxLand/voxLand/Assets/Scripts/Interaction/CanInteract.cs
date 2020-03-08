using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanInteract : MonoBehaviour
{
    public List<Interactable> interactables;
    // Start is called before the first frame update
    void Start()
    {
        interactables = new List<Interactable>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Interact()
    {
        try
        {
            interactables.ForEach(e => e.Action(gameObject));
        }
        catch(Exception e)
        {

        }
    }
    
}
