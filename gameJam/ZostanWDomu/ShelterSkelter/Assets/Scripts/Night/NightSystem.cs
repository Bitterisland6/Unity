using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightSystem : GameEventListener
{
    // Start is called before the first frame update
    void Start()
    {
        action.AddListener(NightUpdate);
    }

    // Update is called once per frame
    void NightUpdate()
    {
        foreach (Player p in PlayerGroup.Instance().players)
        {
            if(p.inGame)
            {
                if (Bank.Instance().food <= 0)
                {
                    p.OnDamaged(1);
                }
                else
                {
                    ResourceAmount r = new ResourceAmount();
                    r.amount = 1;
                    r.type = ResourceType.food;
                    Bank.Instance().Sub(r);
                }
            }
        }
    }
}
