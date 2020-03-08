using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Dissapear : Action
{
    public GameObject wall;
    public GameObject[] mann;
    public GameObject groundfloor;
    public override void Action_start()
    {
        wall.SetActive(false);
        groundfloor.SetActive(true);
        foreach(GameObject i in mann)
        {
            i.SetActive(true);
        }
    }
}
