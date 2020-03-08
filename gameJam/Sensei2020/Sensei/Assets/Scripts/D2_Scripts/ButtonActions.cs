using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonActions : Action
{
    public GameObject bt;
    public GameObject riddle;
    public override void Action_start()
    {
        Light r = bt.GetComponent<Light>();
        if(r.gameObject.activeSelf)
        {
            
        }
    }
}
