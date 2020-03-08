using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDissapear : Action
{
    public GameObject wall;

    public override void Action_start()
    {
        wall.SetActive(false);
    }
}