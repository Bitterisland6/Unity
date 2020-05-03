using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEventManager : MonoBehaviour
{
    private void Start()
    {
        instance = this;
    }
    static RandomEventManager instance;
    public static RandomEventManager Instance()
    {
        if (instance == null)
        {
            Debug.Log("Missing random event manager");
        }
        return instance;
    }
    public void InvokeRandomEvent(ActionType type)
    {
        //TODO OODOOO
    }
}
